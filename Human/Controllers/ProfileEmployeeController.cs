using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Human.Models;
using Human.DO;
using Human.Models.Entities;
using System.IO;
using ImageResizer;
using System.Drawing;
using Human.Common;

namespace Human.Controllers
{
    public class ProfileEmployeeController : BaseController
    {
        ProfileEmployeeModel model = new ProfileEmployeeModel();
        ProfileEmployee_DO profile_emp_DO;
        Employee_DO _empDO;
        // GET: ProfileEmployee
        public ActionResult Index()
        {
            init();
            return View(model);
        }
        public void init()
        {

            try
            {

                profile_emp_DO = new ProfileEmployee_DO();
                if (Session["EMP_ID"] != null)
                {
                    int emp_id = Convert.ToInt32(Session["EMP_ID"]);
                    var show_profile = profile_emp_DO.ProfileEmployeeID(emp_id);
                    model.EMP_PROFILE = show_profile.FirstOrDefault();
                    model.LST_WORKING = profile_emp_DO.GetEmployeeHistoryByID(Convert.ToInt32(emp_id)).ToList();
                }
            }
            catch (Exception ex)
            {

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAvt()
        {
            HttpPostedFileBase upload_file = Request.Files["upload_file"];
            string name_file = "";
            int emp_id = Convert.ToInt32(Session["EMP_ID"]);
            name_file = "emp_" + emp_id.ToString();

            string[] existingFiles = Directory.GetFiles(Server.MapPath("~/Assets/img/avatar/"), name_file + ".*");
            foreach (var file in existingFiles)
            {
                System.IO.File.Delete(file);
            }

            if (upload_file != null && upload_file.ContentLength > 0)
            {
                var fileExtension = Path.GetExtension(upload_file.FileName);
                var fileName = name_file + ".jpg";
                var path = Path.Combine(Server.MapPath("~/Assets/img/avatar/"), fileName);

                // Resize ảnh trước khi lưu
                using (var image = Image.FromStream(upload_file.InputStream))
                {
                    var settings = new ResizeSettings
                    {
                        MaxWidth = 1000,
                        MaxHeight = 1000,
                        Format = "jpg" // Định dạng đầu ra của ảnh (có thể là jpg, png, ...)
                    };
                    ImageBuilder.Current.Build(image, path, settings);
                }
                 
                profile_emp_DO = new ProfileEmployee_DO();
                profile_emp_DO.UpdateAvatarEmployeeID(fileName, emp_id);
                SetAlert("Cập nhật avatar thành công!", "success");
                CommonCheck.SetAlert(this, Human.Resource.Language.Update_avt_success, "success");
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult UploadImage()
        {
            // upload avatar = ajax
            HttpPostedFileBase file_avt = Request.Files["file_avt"];
            int emp_id = Convert.ToInt32(Session["EMP_ID"]);
            string name_file = "emp_" + emp_id.ToString();
            if (file_avt != null && file_avt.ContentLength > 0)
            {
                var fileExtension = Path.GetExtension(file_avt.FileName);
                var fileName = name_file + ".jpg";

                var path = Path.Combine(Server.MapPath("~/Assets/img/avatar/"), fileName);
                // Resize ảnh trước khi lưu
                using (var image = Image.FromStream(file_avt.InputStream))
                {
                    var settings = new ResizeSettings
                    {
                        MaxWidth = 1000,
                        MaxHeight = 1000,
                        Format = "jpg" // Định dạng đầu ra của ảnh (có thể là jpg, png, ...)
                    };
                    ImageBuilder.Current.Build(image, path, settings);
                }
                

                return Json(new { success = true, value_fileName = fileName, imagePath = "/Assets/img/avatar/" + fileName });
            }
            else
            {
                return Json(new { success = false });
            }

        }
 
        public ActionResult GeneratePdf(string name)
        {
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            var _model = new ProfileEmployeeModel();
            profile_emp_DO = new ProfileEmployee_DO();
            _empDO = new Employee_DO();
            var emp_code = Session["EMP_CODE"];
            string con_str = "";
            con_str += " AND EMP_CODE='" + emp_code + "'";
            var emp = _empDO.GetInfoEmployee(con_str).FirstOrDefault();
            var emp_id = emp.EMPLOYEE_ID;
            var emp_name = emp.FULLNAME;

            string imagePath = Server.MapPath("~/Assets/img/avatar-12.jpg");
            _model.AVATAR_EMP = imagePath;
            _model.EMP_PROFILE = profile_emp_DO.ProfileEmployeeID(emp_id).FirstOrDefault();
            var htmlPdf = base.RenderPartialToString("~/Views/Shared/Template/ViewProfile_PDF.cshtml", _model, ControllerContext);
           
            // create a new pdf document converting an html string
            PdfDocument doc = converter.ConvertHtmlString(htmlPdf);

            string filename = string.Format("Profile_{0}_{1}.pdf", emp_name, emp_id);
            string pathfile = Server.MapPath("~/Resource/Pdf/" + filename);

            doc.Save(pathfile);
            doc.Close();

            if (System.IO.File.Exists(pathfile))
            {
                byte[] filebytes = System.IO.File.ReadAllBytes(pathfile);
                string mimeType = MimeMapping.GetMimeMapping(filename);

                //xóa file trên server
                System.IO.File.Delete(pathfile);

                return File(filebytes, mimeType, filename);
            }
            else
            {
                return HttpNotFound();
            }

        }
    }
}


