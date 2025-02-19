using Human.DO;
using Human.Models.PublicModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Configuration;
using Org.BouncyCastle.Asn1.Crmf;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Human.Common;
using System.Globalization;
using System.Threading;
using System.Web.Routing;

namespace Human.Controllers.PublicController
{
    public class PublicController : BaseLoginController
    {
       

        PublicTimeKeepModel model = new PublicTimeKeepModel();
        TimeSheet_DO timesheet_DO;
        Employee_DO _empDO;
        TimeKeep_DO timekeep_DO;
       
        // GET: Public
        public ActionResult Index()
        {

            return View(model);
        }
        public ActionResult SearchLogList(string seach_code, string select_type)
        {
            try
            {
                _empDO = new Employee_DO();
                string con_str = "";
                con_str += " AND EMP_CODE='" + seach_code + "'";
                var emp = _empDO.GetInfoEmployee(con_str).FirstOrDefault();

                if (select_type == "1")
                {
                    timesheet_DO = new TimeSheet_DO();
                    var timesheetList = timesheet_DO.TimeLogListMonthEmpCode(seach_code);
                    if (timesheetList == null)
                    {
                        return Json(new { success = false, message = "Bảng chấm công ngày chưa hoàn thành!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var result = timesheetList.OrderByDescending(x => x.TIME_TEMP).ToList();
                        return Json(new { success = true, data = result, employeeInfo = emp }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (select_type == "2")
                {
                    timekeep_DO = new TimeKeep_DO();
                    var currentDate = DateTime.Now;
                    var current_year = DateTime.Now.Year.ToString();
                    var lastmonth = DateTime.Now.Month.ToString();
                  
                    if (currentDate.Month == 1)
                    {
                       
                        current_year = (currentDate.Year - 1).ToString();
                        lastmonth = (currentDate.Month - 1 + 12).ToString();
                    }
                    else
                    {

                        lastmonth = (currentDate.Month - 1).ToString();
                    }
                    var name_table = current_year + lastmonth;
                    var timeKeepList = timekeep_DO.TimekeepMothEmployeeId(seach_code, name_table);
                    if (timeKeepList == null)
                    {
                        return Json(new { success = false, message = "Bảng chấm công tháng " + lastmonth + " chưa hoàn thành!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var result = timeKeepList.OrderBy(s => s.DATEOFMONTH).ToList();
                        return Json(new { success = true, data = result, employeeInfo = emp, year=current_year,month=lastmonth }, JsonRequestBehavior.AllowGet);
                    }
                }
                // Nếu không có trường hợp nào trong if hoặc else if được thực hiện, trả về kết quả mặc định
                return Json(new { success = false, message = "Không có dữ liệu phù hợp" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra!" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
