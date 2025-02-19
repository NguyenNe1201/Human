
using Human.DO;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebGrease;
using Human.Models.Entities;
using Human.Common;
namespace Human.Areas.Admin.Controllers
{
    public class LoginController : BaseController
    {
        public User_DO _user;
        public Employee_DO _empDO;
        public static int LogIn_DivID;

        string Code_OTP;
        public string Gmail;
        public string OTP;

        // GET: Admin/Login
        public ActionResult Index()
        {
            Session.Clear();
            Session.Abandon();
            ClsGlobal_Entities.POS = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(User_Entities u)
        {
            _empDO = new Employee_DO();
            string condition = "";
            if (IsEmail(u.USERNAME))
            {
                condition += " AND EMAIL='" + u.USERNAME + "'";
            }
            else
            {
                condition += " AND USERNAME='" + u.USERNAME + "'";
            }
            var emp = _empDO.GetEmployeeByCondition(condition).FirstOrDefault();
            if(emp != null)
            {
                if(emp.PASS_WORD == u.PASSWORD)
                {
                    ClsGlobal_Entities.m_Division_ID = emp.DIVISION_ID.ToString();
                    if (emp.JOBTITLE_ID == 1)
                    {
                        ClsGlobal_Entities.POS = "DIR";
                    }
                    else if (emp.JOBTITLE_ID == 2 || emp.JOBTITLE_ID == 3)
                    {
                        ClsGlobal_Entities.POS = "MAN";
                    }
                    else if (emp.JOBTITLE_ID == 5)
                    {
                        ClsGlobal_Entities.POS = "SUP";
                    }
                    else
                        ClsGlobal_Entities.POS = "";
                    return RedirectToAction("Index", "Home");
                }
                SetAlert("your account or password is not correct", "warning");
                
            }
            return View("Index");

        }

    }
}