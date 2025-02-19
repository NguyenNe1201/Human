using Human.Models;

using Human.DO;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Human.Controllers
{
    public class EmployeeListController : Controller
    {
        // GET: Employeelist
        public ActionResult Index()
        {
            if (Session["EMAIL"] != null && Session["EMAIL"].ToString() != "")
            {
                Division_DO div_BO = new Division_DO();
                Department_DO dep_DO = new Department_DO();

                var ListDiv = div_BO.GetAllDivision().ToList();
                var listDep = dep_DO.GetAllDepartment().ToList();
                var model = new CommonModel();
                model.LSTDIVISION = new List<Division_Entities>();
                model.LSTDEPART = new List<Department_Entities>();
                model.LSTDIVISION.Add(new Division_Entities { DIVISION_ID = 0, DIVISION_NAME_EN = "ALL" });
                model.LSTDEPART.Add(new Department_Entities { DEPARTMENT_ID = 0, DEPARTMENT_NAME_EN = "ALL", DIVISION_ID = 0 });
                foreach (var div in ListDiv)
                {
                    model.LSTDIVISION.Add(div);
                }
                foreach (var dep in listDep)
                {
                    model.LSTDEPART.Add(dep);
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        
        }

    }
}