using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Human.Common;
using Human.DO;
using Human.Models;
using Human.Models.FormPost;

namespace Human.Controllers
{
    public class ContractController : BaseController
    {
        // GET: Contract
        public Contract_DO contract_do;
        public Employee_DO emp_do;
        public ProfileEmployee_DO profile_do;
        ContractModel model = new ContractModel();
        public ActionResult Index()
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;
            if (CommonCheck.Check_function_read(lst_fuction_emp, "contract"))
            {
                init();
                return View(model);
            }
            else
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Index", "Home");
            }
        }
       
        public void init()
        {
            contract_do = new Contract_DO();
            emp_do = new Employee_DO();
            model.LST_CONTRACT_TYPE = contract_do.GetContractType().ToList();
            model.LST_ALL_EMPLOYEE = emp_do.GetAllEmployee_Tab().ToList();
        }

        public ActionResult SearchContractByEmpID(string select_id)
        {
            try
            {
                contract_do = new Contract_DO();
                var list_ContractByEmp = contract_do.GetContractByEmpID(Convert.ToInt32(select_id)).OrderByDescending(w=>w.ISSUEDATE);
                if (list_ContractByEmp.Count() >0) {
                    profile_do = new ProfileEmployee_DO();
                    var emp_info = profile_do.ProfileEmployeeID(Convert.ToInt32(select_id)).FirstOrDefault();
                    var result = list_ContractByEmp.ToList();
                    return Json(new { success = true, data = result,emp = emp_info }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = Resource.Language.This_emp_not_contract+"!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Invalid employee ID!" }, JsonRequestBehavior.AllowGet);

            }
        }
        
        [ValidateAntiForgeryToken]
        public ActionResult AddContract_Tab(AddContractModel modelBody)
        {
            contract_do = new Contract_DO();
            int result = 0;

            string date_start = null;
            string date_end = null;

            if (modelBody.ISSUEDATE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_start = modelBody.ISSUEDATE.ToString("yyyy-MM-dd");

            }
            if (modelBody.DEADLINE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_end = modelBody.DEADLINE.ToString("yyyy-MM-dd");

            }
            result = contract_do.AddNewContract_Tab(modelBody.CONTRACT_NO, modelBody.EMPLOYEE_ID, modelBody.CONTRACTTYPE_ID,
                date_start, date_end, modelBody.REMARK, modelBody.BASEON, modelBody.BASEON_KYNGAY);
            if (result > 0)
            {
                var data_model = contract_do.GetContractByEmpID(modelBody.EMPLOYEE_ID).OrderByDescending(f=>f.CONTRACT_ID).FirstOrDefault();
                return Json(new { success = true, data = data_model, message = Human.Resource.Language.Add_contract_success });
            }
            else
            {
                return Json(new { success = false, message = Human.Resource.Language.Add_contract_fail });
            }
        }
        [ValidateAntiForgeryToken]
        public ActionResult UPD_ContractTab(AddContractModel modelBody)
        {
            contract_do = new Contract_DO();
            int result = 0;
            string date_start = null;
            string date_end = null;

            if (modelBody.ISSUEDATE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_start = modelBody.ISSUEDATE.ToString("yyyy-MM-dd");

            }
            if (modelBody.DEADLINE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_end = modelBody.DEADLINE.ToString("yyyy-MM-dd");

            }
            result = contract_do.UpdateContract_Tab(modelBody.CONTRACT_ID, modelBody.CONTRACT_NO, modelBody.EMPLOYEE_ID, modelBody.CONTRACTTYPE_ID,
                date_start, date_end, modelBody.REMARK, modelBody.BASEON, modelBody.BASEON_KYNGAY);
            if (result > 0)
            {
                return Json(new { success = true, data = modelBody, message = Human.Resource.Language.Update_contract_success });
            }
            else
            {
                return Json(new { success = false, message = Human.Resource.Language.Update_contract_fail });
            }
        }
    }
}