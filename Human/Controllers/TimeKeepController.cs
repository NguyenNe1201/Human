
using Human.Models;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Drawing.Printing;
using Microsoft.Ajax.Utilities;
using System.Globalization;
using WebGrease.Activities;
using SelectPdf;
using System.Web.Configuration;
using Human.DO;
using Human.Common;
namespace Human.Controllers
{
    public class TimeKeepController : BaseController
    {
        // GET: TimeKeep
        TimeKeepModel model = new TimeKeepModel();
        TimeKeep_DO timekeep_DO;
        Employee_DO _empDO;

        public ActionResult ListTimeKeep()
        {
            init();

            return View(model);

        }
        public void init()
        {
            try
            {
                timekeep_DO = new TimeKeep_DO();
                // Lấy năm và tháng hiện tại
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;
                string name_table = currentYear.ToString() + currentMonth.ToString("D2");
                var emp_code = Session["EMP_CODE"];
                if (emp_code != null)
                {
                    
                    var timeKeepList = timekeep_DO.TimekeepMothEmployeeId(emp_code.ToString(), name_table);
                    model.LIST_TIMEKEEEPVIEW = timeKeepList.ToList();
                }
            }
            catch (Exception ex)
            {

            }
        }
        // Search TimKeep
        public ActionResult SearchTimeKeep([FromBody] TimeKeepModel modelBody, string SEARCH_MONTH, string SEARCH_YEAR)
        {
            try
            {
                timekeep_DO = new TimeKeep_DO();
                if (SEARCH_MONTH == "" || SEARCH_YEAR == "")
                {
                 //   SetAlert("Vui lòng chọn đầy đủ!", "warning");
                    CommonCheck.SetAlert(this,Human.Resource.Language.Please_select_complete_info, "warning");
                }
                else
                {
                    var emp_code = Session["EMP_CODE"];
                    if (emp_code != null)
                    {
                        int emp_id = Convert.ToInt32(Session["EMP_ID"]);

                        // Set the name_table based on the selected date (SEARCH_DATE)
                        string name_table = SEARCH_YEAR + SEARCH_MONTH;
                        // Cập nhật giá trị SEARCH_DATE trong ViewData
                        ViewData["SEARCH_DATE"] = SEARCH_MONTH + "/" + SEARCH_YEAR;
                        // Lấy dữ liệu từ BO
                        var timeKeepList = timekeep_DO.TimekeepMothEmployeeId(emp_code.ToString(), name_table);
                        if (timeKeepList == null)
                        {

                           // SetAlert("Bảng chấm công tháng " + SEARCH_MONTH+"/"+SEARCH_YEAR+ " chưa hoàn thành!", "warning");
                            CommonCheck.SetAlert(this, "Bảng chấm công tháng " + SEARCH_MONTH + "/" + SEARCH_YEAR + " chưa hoàn thành!", "warning");
                        }
                        else
                        {

                            // Tạo đối tượng TimeKeepModel và gán danh sách dữ liệu cho nó
                            model.LIST_TIMEKEEEPVIEW = timeKeepList.OrderBy(o => o.DATEOFMONTH).ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return View("ListTimeKeep", model);
        }
        // Export PDF
        public ActionResult GeneratePdf(string SEARCH_DATE)
        {
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 10;
            converter.Options.MarginBottom = 10;

            var _model = new TimeKeepModel();
            timekeep_DO = new TimeKeep_DO();
            _empDO = new Employee_DO();
            var emp_code = Session["EMP_CODE"];
            string con_str =" AND EMP_CODE='" + emp_code + "'";
            var emp = _empDO.GetInfoEmployee(con_str).FirstOrDefault();
            var emp_id = emp.EMPLOYEE_ID;
            var emp_name = emp.FULLNAME;
            DateTime date = DateTime.ParseExact(SEARCH_DATE, "MM/yyyy", CultureInfo.InvariantCulture);
            string name_table = date.ToString("yyyyMM");
            _model.MONTH_TIMEKEEP = SEARCH_DATE;
            _model.EMP_VIEW = emp;
            _model.LIST_TIMEKEEEPVIEW = timekeep_DO.TimekeepMothEmployeeId(emp_code.ToString(), name_table).OrderBy(o => o.DATEOFMONTH).ToList();
            var htmlPdf = base.RenderPartialToString("~/Views/Shared/Template/ViewTimeKeep_PDF.cshtml", _model, ControllerContext);
            // create a new pdf document converting an html string
            PdfDocument doc = converter.ConvertHtmlString(htmlPdf);
            string filename = string.Format("Timkeep_{0}_{1}.pdf", emp_name, name_table);
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