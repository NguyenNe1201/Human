using Human.Common;
using Human.DO;
using Human.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace Human.Controllers
{
    public class ManageBySectionController : BaseController
    {

        User_DO user_do;
        Department_DO department_do;
        Section_DO section_do;
        JobTitle_DO jobtitle_do;
        Salary_DO salary_do;
        Employee_DO _emp_DO;
        ProfileEmployee_DO profile_do;
        Contract_DO contract_do;
        ManageBySectionModel model = new ManageBySectionModel();

        //--------------------------------- List employee by section_id ------------------------------------------
        public ActionResult ListEmp(int? sectionId)
        {
            var lstPowerByEmp = Session["POWER_BY_EMP"] as List<Human.Models.Entities.PowerView_Entities>;
            int i = CommonCheck.GetSectionIdFromList(lstPowerByEmp, sectionId); // Sử dụng hàm mới để lấy giá trị i

            if (lstPowerByEmp != null && sectionId.HasValue && sectionId == i)
            {
                ViewBag.activecontrol = Convert.ToInt32(sectionId + 1000);
                init(sectionId);
                return View(model);
            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Index", "Home");
            }

        }
        public void init(int? sectionId)
        {

            user_do = new User_DO();
            department_do = new Department_DO();
            section_do = new Section_DO();
            jobtitle_do = new JobTitle_DO();
            salary_do = new Salary_DO();
            _emp_DO = new Employee_DO();
            string condition = "";
            /* model.LIST_EMP_CONDITION = user_do.GetAllEmpCondition(condition).ToList();*/
            model.LST_EMP_BY_SECTION = _emp_DO.GetEmpViewBySectionID(Convert.ToInt32(sectionId)).ToList();
            model.SECTION_BY_ID = section_do.GetSectionByID(Convert.ToInt32(sectionId)).FirstOrDefault();
            model.List_DEPARTMENT = department_do.GetAllDepartment().ToList();
            model.LST_SECTION = section_do.GetAllSection().ToList();
            model.LST_JOBTITLE = jobtitle_do.GetAllJobTitle().ToList();
            model.LST_SALARY_LEVEL = salary_do.GetSalary_level().ToList();
            model.LST_PC = salary_do.GetSalary_ext().ToList();
            /*  var emp_code = Session["EMP_CODE"];
              if (emp_code != null)
              {
                  Session["FUNCTION_BY_EMP"] = role_do.GetFunctionEmp(emp_code.ToString()).ToList();
              }*/
        }
        //------------------------------------ add employee tab ----------------------------------------
        public ActionResult AddEmpPost([FromBody] AddEmpModel modelBody)
        {
            profile_do = new ProfileEmployee_DO();
            int result = 0;
            string date_quit = null;
            string begin_date = null;
            string end_date = null;
            string date_start = null;
            string date_end = null;
            if (modelBody.DATE_QUIT.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_quit = modelBody.DATE_QUIT.ToString("yyyy-MM-dd");
            }
            if (modelBody.BEGIN_DATE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                begin_date = modelBody.BEGIN_DATE.ToString("yyyy-MM-dd");
            }
            if (modelBody.END_DATE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                end_date = modelBody.END_DATE.ToString("yyyy-MM-dd");
            }
            if (modelBody.CONGTACTU.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_start = modelBody.CONGTACTU.ToString("yyyy-MM-dd");
            }
            if (modelBody.CONGTACDEN.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_end = modelBody.CONGTACDEN.ToString("yyyy-MM-dd");
            }
            result = profile_do.AddEmployee_Tab(modelBody.EMP_CODE, modelBody.HIRE_DAY, modelBody.FIRSTNAME, modelBody.DATE_OF_BIRTH, modelBody.SEX,
                    modelBody.ID_CARDNUMBER, modelBody.DATE_ISSUE, modelBody.PLACE_ISSUE, modelBody.EDUCATION, modelBody.DEGREE, modelBody.PERMANENT_ADDRESS, modelBody.TEMPORARY_ADDRESS,
                    modelBody.PHONE_NUMBER, modelBody.MOBILE_NUMBER, modelBody.EMAIL, modelBody.BANKACOUNT_NUMBER, modelBody.BANK_NAME, modelBody.TAX_NUMBER, modelBody.SOBHXH, modelBody.SOBHYT,
                    modelBody.DEPARTMENT_ID, modelBody.SECTION_ID, modelBody.JOBTITLE_ID, modelBody.PROBATION, modelBody.CONGTAC, date_start, date_end, modelBody.QUIT_REASON, modelBody.QUIT,
                    date_quit, modelBody.SALARY_CAT_ID, modelBody.BASIC_SALARY, modelBody.ALLOWANCE_JOB, modelBody.ALLOWANCE_HOUSE, modelBody.ALLOWANCE_PETROL, modelBody.ALLOWANCE_PHONE,
                    modelBody.ALLOWANCE_CASHIER, begin_date, end_date, modelBody.ALLOWANCE_MEAL, modelBody.ALLOWANCE_SUPPORT, modelBody.NOTE);
            if (result > 0)
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Add_emp_info_success, "success");
            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Add_emp_info_fail, "error");
            }
            return RedirectToAction("ListEmp", new { sectionId = modelBody.SECTION_ID });
        }
        //---------------------------------------- View Form EDIT Employee --------------------------------------
        public ActionResult EditEmp(int? employee, int? sectionId)
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;
            // check quyền hạn chỉnh sửa nhân viên của user đang login
            if (CommonCheck.Check_function_read(lst_fuction_emp, "staffBySection"))
            {
                // kiểm tra user này có quản lý section của nhân viên muốn sửa hay không
                var lst_power_emp = Session["POWER_BY_EMP"] as List<Human.Models.Entities.PowerView_Entities>;
                int i = CommonCheck.GetSectionIdFromList(lst_power_emp, sectionId);
                _emp_DO = new Employee_DO();
                var emp_view = _emp_DO.GetEmpViewBySectionID(i).Where(w => w.EMPLOYEE_ID == employee).FirstOrDefault();
                if (emp_view != null)
                {
                    ViewBag.activecontrol = Convert.ToInt32(sectionId + 1000);
                    init_editEmp(employee);
                    return View(model);
                }
                else
                {
                    CommonCheck.SetAlert(this, "Bạn không thể chỉnh sửa người này!", "error");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Index", "Home");
            }
        }
        public void init_editEmp(int? emp)
        {
            profile_do = new ProfileEmployee_DO();
            department_do = new Department_DO();
            section_do = new Section_DO();
            jobtitle_do = new JobTitle_DO();
            salary_do = new Salary_DO();
            var show_profile = profile_do.ProfileEmployeeID(Convert.ToInt32(emp));
            model.EMP_PROFILE = show_profile.OrderByDescending(o => o.HISTORY_ID).FirstOrDefault();
            model.LST_SECTION = section_do.GetAllSection().ToList();
            model.List_DEPARTMENT = department_do.GetAllDepartment().ToList();
            model.LST_SALARY_LEVEL = salary_do.GetSalary_level().ToList();
            model.LST_PC = salary_do.GetSalary_ext().ToList();
            model.LST_JOBTITLE = jobtitle_do.GetAllJobTitle().ToList();
            // show table working, salary history
            model.LST_SALARY_HISTORY = salary_do.GetAllSalary_History().Where(o => o.EMPLOYEE_ID == emp).OrderByDescending(w => w.BEGIN_DATE).ToList();
            model.LST_WORKING = profile_do.GetEmployeeHistoryByID(Convert.ToInt32(emp)).OrderByDescending(w => w.CONGTACTU).ToList();
        }
        // ----------------------------------------- View Info Employee ----------------------------------------
        public ActionResult InfoEmp(int? employee,int? sectionId)
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;
            if (CommonCheck.Check_function_read(lst_fuction_emp, "staffBySection"))
            {
                // kiểm tra user này có quản lý section của nhân viên này hay không
                var lst_power_emp = Session["POWER_BY_EMP"] as List<Human.Models.Entities.PowerView_Entities>;
                int i = CommonCheck.GetSectionIdFromList(lst_power_emp, sectionId);
                _emp_DO = new Employee_DO();
                var emp_view = _emp_DO.GetEmpViewBySectionID(i).Where(w => w.EMPLOYEE_ID == employee).FirstOrDefault();
                if (emp_view != null)
                {
                    // show profile nhân viên theo quyền hạn quản lý bộ phận
                    profile_do = new ProfileEmployee_DO();
                    model.EMP_PROFILE = profile_do.ProfileEmployeeID(Convert.ToInt32(employee)).FirstOrDefault();
                    model.LST_WORKING = profile_do.GetEmployeeHistoryByID(Convert.ToInt32(employee)).ToList();
                    ViewBag.activecontrol = Convert.ToInt32(sectionId + 1000);
                    return View(model);
                }
                else
                {
                    CommonCheck.SetAlert(this,"Bạn không có quyền xem thông tin nhân viên này!", "error");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Index", "Home");
            }
        }
        // ----------------------------------------- View Contract index -----------------------------------
        public ActionResult Contract(int? employee,int? sectionId)
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;
            if (CommonCheck.Check_function_read(lst_fuction_emp, "contract"))
            { 
                // kiểm tra user này có quản lý section của nhân viên này hay không
                var lst_power_emp = Session["POWER_BY_EMP"] as List<Human.Models.Entities.PowerView_Entities>;
                int i = CommonCheck.GetSectionIdFromList(lst_power_emp, sectionId);
                _emp_DO = new Employee_DO();
                var emp_view = _emp_DO.GetEmpViewBySectionID(i).Where(w => w.EMPLOYEE_ID == employee).FirstOrDefault();
                if (emp_view != null)
                {
                    profile_do = new ProfileEmployee_DO();
                   
                    contract_do = new Contract_DO();
                    var show_profile = profile_do.ProfileEmployeeID(Convert.ToInt32(employee));
                    model.EMP_PROFILE = show_profile.OrderByDescending(o => o.HISTORY_ID).FirstOrDefault();
                    model.LST_CONTRACT_TYPE = contract_do.GetContractType().ToList();
                    model.LST_CONTRACT_EMPID = contract_do.GetContractByEmpID(Convert.ToInt32(employee)).OrderByDescending(w => w.ISSUEDATE).ToList();
                    ViewBag.activecontrol = Convert.ToInt32(sectionId + 1000);
                    return View(model);
                }
                else
                {
                    CommonCheck.SetAlert(this,"Bạn không được xem hợp đồng của nhân viên này!", "error");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}