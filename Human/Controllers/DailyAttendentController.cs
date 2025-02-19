using Human.Models;
using Human.DO;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Human.Controllers
{
    public class DailyAttendentController : BaseController
    {
        // GET: DailyA
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTimeTempByMonth(string MONTH, string YEAR)
        {
            try
            {
                DbHelper db = new DbHelper();
                List<TimeTempModel> result = new List<TimeTempModel>();
                if (Convert.ToInt32(MONTH) < 10)
                    MONTH = "0" + MONTH;
                string YearMonth = YEAR + MONTH;
                var empCode = Session["EMP_CODE"];
                string condition = "AND T2.EMP_CODE ='" + empCode + "'";

                result = db.Executereader<TimeTempModel>("GetAllTimesheetTemp", new string[] { "@YEARMONTH", "@CONDITION" }, new object[] { YearMonth, condition }).ToList();
                if (result.Count == 0)
                {

                    SetAlert("không có giờ công tháng này.", "warning");
                }
                return View("Index", result);

            }
            catch (Exception ex)
            {
                SetAlert("Lỗi! chưa tồn tại thời gian này.", "error");
                return View("Index");
            }

        }
    }
}