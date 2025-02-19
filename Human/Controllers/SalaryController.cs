using Human.Models;
using Human.Models.Entities;
using Human.DO;
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
using Human.Common;

namespace Human.Controllers
{
    public class SalaryController : BaseController
    {
        // GET: Salary
        Salary_Entities model = new Salary_Entities();
        Salary_DO _salary_DO;
        public ActionResult Index()

        {

            return View(model);
        }
        [HttpGet]
        public ActionResult Index(string MONTH_SALARY, string YEAR)
        {

            try
            {

                if (MONTH_SALARY != null)
                {
                    if (Convert.ToInt32(MONTH_SALARY) < 10)
                        MONTH_SALARY = "0" + MONTH_SALARY;
                    string YearMonth = YEAR + MONTH_SALARY;
                    string Emp_Code = Session["EMP_CODE"].ToString();
                    _salary_DO = new Salary_DO();
                    model = _salary_DO.GetSalaryByEmpCode(Emp_Code, YearMonth).FirstOrDefault();

                    ViewBag.ThisMonth = MONTH_SALARY + "/" + YEAR;
                    ViewBag.PreMonth = (Convert.ToInt32(MONTH_SALARY) - 1).ToString() + "/" + YEAR;

                    if (model == null)
                    {
                        model = new Salary_Entities();
                     //   SetAlert("Bảng lương tháng " + MONTH_SALARY + " chưa hoàn thành!", "warning");
                        CommonCheck.SetAlert(this, "Bảng lương tháng " + MONTH_SALARY + " chưa hoàn thành!", "warning");

                    }
                    else
                    {
                        Session["PAY_DAY"] = YearMonth;

                    }

                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }

            //string emp_code = Session["EMP_CODE"].ToString();
            //string yearmonth = "";
            //Salary_BO _bo = new Salary_BO();

            //Salary model  = new Salary();
            //model = _bo.GetSalaryByEmpCode(emp_code, yearmonth);
            //return View(model);

        }

        public ActionResult Test()
        {
            SalaryModel m = new SalaryModel();
            m.Salary = new Salary_Entities();
            return View(m);
        }
        public ActionResult GeneratePdf(string name)
        {
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;

            var _model = new SalaryModel();
            _salary_DO = new Salary_DO();

            string _year = Session["PAY_DAY"].ToString().Substring(0, 4);
            string _month = Session["PAY_DAY"].ToString().Substring(4);
            _model.TEXT_TODATE = _month + "/" + _year;
            _model.TEXT_FROMDATE = (Convert.ToInt32(_month) - 1).ToString() + "/" + _year;

            _model.Salary = _salary_DO.GetSalaryByEmpCode(Session["EMP_CODE"].ToString(), Session["PAY_DAY"].ToString()).FirstOrDefault();

            var htmlPdf = base.RenderPartialToString("~/Views/Shared/Template/ViewSalary_PDF.cshtml", _model, ControllerContext);

            // create a new pdf document converting an html string
            PdfDocument doc = converter.ConvertHtmlString(htmlPdf);

            string filename = string.Format("{0}.pdf", DateTime.Now.Ticks);
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