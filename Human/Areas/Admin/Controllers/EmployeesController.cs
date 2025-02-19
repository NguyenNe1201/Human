
using Human.DO;
using Human.Models.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Human.Areas.Admin.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Admin/Employees
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CheckEmpByCode(string emp_code)
        {
            Employee_DO _empDO = new Employee_DO();
            EmployeeView_Entities result = _empDO.GetEmployeeByCode(emp_code).FirstOrDefault();
            LEAVE_DO l_DO = new LEAVE_DO();
            float pncl = l_DO.SEL_ANUAL_LEAVE_REMAIN(result.EMPLOYEE_ID);
            return Json(new object[] {result,pncl},JsonRequestBehavior.AllowGet);
        }
    }
}