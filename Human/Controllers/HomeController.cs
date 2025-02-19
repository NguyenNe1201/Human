using Human.DO;
using Human.Models;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Human.Common;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Net;
using Infobip.Api.SDK.RCS.Models;
using System.Globalization;

namespace Human.Controllers
{
    public class HomeController : BaseController
    {
        public Employee_DO _empDO;
        public LEAVE_DO leave_DO;
        public EmailSMS_DO _smsDO;
        public Role_DO role_do;
        HomeModel model = new HomeModel();
        KindLeave_DO kindleave_Do;

        
        public int Count_notifi { get; private set; }

        public ActionResult Index()
        {
            // Lấy đối tượng HttpContext từ Controller
            HttpContext context = System.Web.HttpContext.Current;

            // Lấy địa chỉ IP của người dùng từ đối tượng HttpContext
            string ipAddress = context.Request.UserHostAddress;
            init();
            Session["INFO_EMP"] = model.EMP_VIEW;
            Session["COUNT_NOTIFI"] = Count_notifi.ToString();
           
            return View(model);

        }
       
        [HttpPost]
        public ActionResult ProcessEmailSMS(string email_address, string password_app)
        {
            //upload email sms login
            try
            {
                _smsDO = new EmailSMS_DO();
                _smsDO.UPD_EmailSMS(email_address, password_app);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public void init()
        {

            role_do = new Role_DO();
            _empDO = new Employee_DO();
            leave_DO = new LEAVE_DO();
            kindleave_Do = new KindLeave_DO();
            var emp_code = Session["EMP_CODE"];
            string con_str = "";
            model.LST_KINDLEAVE = kindleave_Do.GetAllKindLeave().ToList();
            if (emp_code != null)
            {
                con_str += " AND EMP_CODE='" + emp_code + "'";
                var emp = _empDO.GetInfoEmployee(con_str).FirstOrDefault();
                if (emp != null)
                {
                    var emp_id = emp.EMPLOYEE_ID;
                    string condition = "AND EMPLOYEE_ID =" + emp_id;
                    string condition_leave = " AND STATUS_L = 4 ";
                    model.EMP_VIEW = _empDO.GetEmployeeByCode(emp_code.ToString()).FirstOrDefault();
                    Session["POWER_BY_EMP"] = role_do.GetPowerByEmpCode(emp_code.ToString()).ToList();
                    Session["FUNCTION_BY_EMP"] = role_do.GetFunctionEmp(emp_code.ToString()).ToList();
                    model.LST_LEAVE = leave_DO.GetLeaveByEmployeeId(emp_id).Where(w => w.LEAVE_STARTDATE.Year == DateTime.Now.Year).OrderByDescending(o => o.LEAVE_STARTDATE).ToList();
                    model.TOTAL_LEAVE = leave_DO.SEL_CAL_LEAVE(condition).FirstOrDefault();
                   /* model.LST_LEAVEVIEW = leave_DO.SP_SEL_LEAVE_MODEL(condition_leave).ToList();*/
                    Count_notifi = leave_DO.SP_SEL_LEAVE_MODEL(condition_leave).Count();
                }
            }

        }
        
        public ActionResult Cancel(string LEAVE_ID)
        {
            leave_DO = new LEAVE_DO();
            int i = leave_DO.Cancel(Convert.ToInt32(LEAVE_ID));
            if (i > 0)
            {
              //  SetAlert("Hủy phép thành công", "success");
                CommonCheck.SetAlert(this, Human.Resource.Language.Cancel_leave_successfully, "success");
            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Cancel_leave_failed, "error");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Request(string LEAVE_ID)
        {
            leave_DO = new LEAVE_DO();
            int i = leave_DO.Request(Convert.ToInt32(LEAVE_ID));
            if (i > 0)
            {
              //  SetAlert("Gửi yêu cầu thành công", "success");
                CommonCheck.SetAlert(this,Resource.Language.Request_sent_successfully,"success");
            }
            else
            {
                CommonCheck.SetAlert(this, Resource.Language.Request_sent_failed, "error");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string LEAVE_ID)
        {
            leave_DO = new LEAVE_DO();
            int i = leave_DO.Delete(Convert.ToInt32(LEAVE_ID));
            if (i > 0)
            {
             //   SetAlert("Xóa phép thành công!", "success");
                CommonCheck.SetAlert(this,Human.Resource.Language.Delete_leave_successfully,"success");
            }
            else
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.Delete_leave_failed,"error");
            }
            return RedirectToAction("Index");
        }
       /* private void SetAlert(string message, string alertType)
        {
            TempData["AlertMessage"] = message;
            TempData["AlertType"] = alertType;
        }*/
    }
}
