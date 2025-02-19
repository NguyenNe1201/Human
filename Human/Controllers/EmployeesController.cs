using Human.Models;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using System.Web.Mvc;
using Human.DO;
using System.Web.Routing;
using Human.Areas.Admin.Models;
using System.Web.Http.Results;
using Microsoft.Ajax.Utilities;
using SelectPdf;
using static System.Collections.Specialized.BitVector32;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Human.Models.FormPost;
using Human.Common;
namespace Human.Controllers
{
    public class EmployeesController : BaseController
    {
        public Employee_DO _empDO;
        public ProfileEmployee_DO profile_do;
        public Section_DO section_do;
        public Department_DO department_do;
        public TimeSheet_DO timesheet_DO;
        public JobTitle_DO jobtitle_do;
        public Salary_DO salary_do;
        public User_DO user_do;
        public Role_DO role_do;
        public Contract_DO contract_do;
        LoginController _login = new LoginController();
        ProfileEmployeeModel model = new ProfileEmployeeModel();
        // GET api/values
        public ActionResult Index()
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;
            if (CommonCheck.Check_function_read(lst_fuction_emp, "allEmployee"))
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
            role_do = new Role_DO();
            user_do = new User_DO();
            department_do = new Department_DO();
            section_do = new Section_DO();
            jobtitle_do = new JobTitle_DO();
            salary_do = new Salary_DO();
            _empDO = new Employee_DO();
            string condition = "";
            /* model.LIST_EMP_CONDITION = user_do.GetAllEmpCondition(condition).ToList();*/
            model.LST_ALL_EMPLOYEE = _empDO.GetAllEmployee_Tab().ToList();
            model.LST_DEPARTMENT = department_do.GetAllDepartment().ToList();
            model.LST_SECTION = section_do.GetAllSection().ToList();
            model.LST_JOBTITLE = jobtitle_do.GetAllJobTitle().ToList();
            model.LST_SALARY_LEVEL = salary_do.GetSalary_level().ToList();
            model.LST_PC = salary_do.GetSalary_ext().ToList();
        }
        
        //---------------------------------------- View info employee by ID ---------------------------------------------
        public ActionResult InfoEmp(int? employee)
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;

            if (CommonCheck.Check_function_read(lst_fuction_emp, "allEmployee"))
            {   
                // show profile nhân viên theo quyền tất cả nhân viên
                profile_do = new ProfileEmployee_DO();
                model.EMP_PROFILE = profile_do.ProfileEmployeeID(Convert.ToInt32(employee)).FirstOrDefault();
                model.LST_WORKING = profile_do.GetEmployeeHistoryByID(Convert.ToInt32(employee)).ToList();
                return View(model);
            }
          
            else
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Index", "Home");
            }
        }

        // Contract in employeeController
        public ActionResult Contract(int? employee)
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;
            if (CommonCheck.Check_function_read(lst_fuction_emp, "contract"))
            {
                profile_do = new ProfileEmployee_DO();            
                contract_do = new Contract_DO();
                var show_profile = profile_do.ProfileEmployeeID(Convert.ToInt32(employee));
                model.EMP_PROFILE = show_profile.OrderByDescending(o => o.HISTORY_ID).FirstOrDefault();
                model.LST_CONTRACT_TYPE = contract_do.GetContractType().ToList();
                model.LST_CONTRACT_EMPID = contract_do.GetContractByEmpID(Convert.ToInt32(employee)).OrderByDescending(w => w.ISSUEDATE).ToList();
                return View(model);
            }
            else
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Index", "Home");
            }
        }
        //---------------------------------------- View Update Employee ---------------------------------------------------
        public ActionResult EditEmp(int? employee)
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;

            if (CommonCheck.Check_function_edit(lst_fuction_emp, "allEmployee"))
            {
                init_editEmp(employee);
                return View(model);
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
            model.LST_DEPARTMENT = department_do.GetAllDepartment().ToList();
            model.LST_SALARY_LEVEL = salary_do.GetSalary_level().ToList();
            model.LST_PC = salary_do.GetSalary_ext().ToList();
            model.LST_JOBTITLE = jobtitle_do.GetAllJobTitle().ToList();
            // show table working, salary history
            model.LST_SALARY_HISTORY = salary_do.GetAllSalary_History().Where(o => o.EMPLOYEE_ID == emp).OrderByDescending(w => w.BEGIN_DATE).ToList();
            model.LST_WORKING = profile_do.GetEmployeeHistoryByID(Convert.ToInt32(emp)).OrderByDescending(w => w.CONGTACTU).ToList();
        }
        //------------------------------------------- Export pdf profile employee --------------------------------------------------
        public ActionResult GeneratePdf(string name, string emp_code)
        {
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();
            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            var _model = new ProfileEmployeeModel();
            profile_do = new ProfileEmployee_DO();
            _empDO = new Employee_DO();
            string con_str = "";
            con_str += " AND EMP_CODE='" + emp_code + "'";
            var emp = _empDO.GetInfoEmployee(con_str).FirstOrDefault();
            var emp_id = emp.EMPLOYEE_ID;
            var emp_name = emp.FULLNAME;
            string imagePath = Server.MapPath("~/Assets/img/avatar-12.jpg");
            _model.AVATAR_EMP = imagePath;
            _model.EMP_PROFILE = profile_do.ProfileEmployeeID(emp_id).FirstOrDefault();
            var htmlPdf = base.RenderPartialToString("~/Views/Shared/Template/ViewProfile_PDF.cshtml", _model, ControllerContext);
            // create a new pdf document converting an html string
            PdfDocument doc = converter.ConvertHtmlString(htmlPdf);
            string filename = string.Format("Profile_{0}_{1}.pdf", emp_name, emp_id);
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
        //------------------------------------------  Add employee = method post -----------------------------------------------
        [ValidateAntiForgeryToken]
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
                CommonCheck.SetAlert(this,Human.Resource.Language.Add_emp_info_success, "success");
            }
            else
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.Add_emp_info_fail, "error");
            }
            return RedirectToAction("Index");
        }
        //----------------------------------------- update employee_tab = method post --------------------------------
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmp_Tab(EditEmpModel modelBody)
        {
            profile_do = new ProfileEmployee_DO();
            int result = 0;
            result = profile_do.UpdateEmp_tab(modelBody.ID_EMP, modelBody.HIRE_DAY, modelBody.FIRSTNAME, modelBody.DATE_OF_BIRTH, modelBody.SEX,
                modelBody.ID_CARDNUMBER, modelBody.PHONE_NUMBER, modelBody.MOBILE_NUMBER, modelBody.DATE_ISSUE, modelBody.PLACE_ISSUE, modelBody.EMAIL, modelBody.BANKACOUNT_NUMBER, modelBody.BANK_NAME,
                modelBody.TAX_NUMBER, modelBody.SOBHXH, modelBody.SOBHYT, modelBody.PERMANENT_ADDRESS, modelBody.TEMPORARY_ADDRESS, modelBody.EDUCATION, modelBody.DEGREE);
            // Trả về kết quả cập nhật, có thể là JSON hoặc thông báo thành công/lỗi
            if (result > 0)
            {
                return Json(new { success = true, message = Human.Resource.Language.Update_emp_info_success });
            }
            else
            {
                return Json(new { success = false, message = Human.Resource.Language.Update_emp_info_fail });
            }
        }
        //------------------------------------  add, update employee_history = method post / ajax -----------------------------
        [ValidateAntiForgeryToken]    
        public ActionResult UpdateEmp_History(EditEmpModel modelBody)
        {
            profile_do = new ProfileEmployee_DO();
            int result = 0;
            string date_quit = null;
            string date_start = null;
            string date_end = null;
            if (modelBody.DATE_QUIT.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_quit = modelBody.DATE_QUIT.ToString("yyyy-MM-dd");
            }
            if (modelBody.CONGTACTU.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_start = modelBody.CONGTACTU.ToString("yyyy-MM-dd");
            }
            if (modelBody.CONGTACDEN.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_end = modelBody.CONGTACDEN.ToString("yyyy-MM-dd");
            }
            result = profile_do.UpdateEmp_History(modelBody.HISTORY_ID, modelBody.ID_EMP, modelBody.DIVISION_ID, modelBody.DEPARTMENT_ID, modelBody.SECTION_ID, modelBody.JOBTITLE_ID,
                modelBody.PROBATION, modelBody.QUIT, date_quit, modelBody.QUIT_REASON, date_start, date_end);
            if (result > 0)
            {
                return Json(new { success = true, data = modelBody, message = Human.Resource.Language.Edit_work_process_success });
            }
            else
            {
                return Json(new { success = false, message = Human.Resource.Language.Edit_work_process_fail });
            }
        }
        [ValidateAntiForgeryToken]
        public ActionResult AddEmp_History(EditEmpModel modelBody)
        {
            profile_do = new ProfileEmployee_DO();
            int result = 0;
            string date_quit = null;
            string date_start = null;
            string date_end = null;
            if (modelBody.DATE_QUIT.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_quit = modelBody.DATE_QUIT.ToString("yyyy-MM-dd");
            }
            if (modelBody.CONGTACTU.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_start = modelBody.CONGTACTU.ToString("yyyy-MM-dd");
            }
            if (modelBody.CONGTACDEN.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_end = modelBody.CONGTACDEN.ToString("yyyy-MM-dd");
            }
            result = profile_do.AddEmp_History(modelBody.ID_EMP, modelBody.DIVISION_ID, modelBody.DEPARTMENT_ID, modelBody.SECTION_ID, modelBody.JOBTITLE_ID,
                     modelBody.PROBATION, modelBody.QUIT, date_quit, modelBody.QUIT_REASON, modelBody.CONGTAC, date_start, date_end);
            if (result > 0)
            {
                var model_data = profile_do.GetEmployeeHistoryByID(Convert.ToInt32(modelBody.ID_EMP)).OrderByDescending(w => w.CONGTACTU).ToList();
                return Json(new { success = true, data = model_data, message = Human.Resource.Language.Add_work_process_success });
            }
            else
            {
                return Json(new { success = false, message = Human.Resource.Language.Add_work_process_fail });
            }
        }
        //-------------------------------- Update, add salary_history_tab = ajax -----------------------------------
        [ValidateAntiForgeryToken]
        public ActionResult AddSalary_History(EditEmpModel modelBody)
        {
            profile_do = new ProfileEmployee_DO();
            int result = 0;
            string date_start = null;
            string date_end = null;

            if (modelBody.BEGIN_DATE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_start = modelBody.BEGIN_DATE.ToString("yyyy-MM-dd");
            }
            if (modelBody.END_DATE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_end = modelBody.END_DATE.ToString("yyyy-MM-dd");
            }
            result = profile_do.AddSalary_History(modelBody.ID_EMP, modelBody.SALARY_CAT_ID, modelBody.BASIC_SALARY, modelBody.ALLOWANCE_JOB, modelBody.ALLOWANCE_PHONE,
                    modelBody.ALLOWANCE_PETROL, modelBody.ALLOWANCE_CASHIER, date_start, date_end, modelBody.NOTE, modelBody.ALLOWANCE_MEAL,
                    modelBody.ALLOWANCE_HOUSE, modelBody.ALLOWANCE_SUPPORT);
            if (result > 0)
            {
                salary_do = new Salary_DO();
                var data_model = salary_do.GetAllSalary_History().Where(o => o.EMPLOYEE_ID == modelBody.ID_EMP).OrderByDescending(w => w.BEGIN_DATE).ToList();
                return Json(new { success = true, data = data_model, message = Human.Resource.Language.Add_salary_history_success });
            }
            else
            {
                return Json(new { success = false, message = Human.Resource.Language.Add_salary_history_fail });
            }
        }
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSalary_History(EditEmpModel modelBody)
        {
            profile_do = new ProfileEmployee_DO();
            int result = 0;
            string date_start = null;
            string date_end = null;
            if (modelBody.BEGIN_DATE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_start = modelBody.BEGIN_DATE.ToString("yyyy-MM-dd");
            }
            if (modelBody.END_DATE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                date_end = modelBody.END_DATE.ToString("yyyy-MM-dd");
            }
            result = profile_do.UpdateSalary_His(modelBody.SALARY_HISTORY_ID, modelBody.ID_EMP, modelBody.SALARY_CAT_ID, modelBody.BASIC_SALARY, modelBody.ALLOWANCE_JOB, modelBody.ALLOWANCE_PHONE,
                    modelBody.ALLOWANCE_PETROL, modelBody.ALLOWANCE_CASHIER, date_start, date_end, modelBody.NOTE, modelBody.ALLOWANCE_MEAL,
                    modelBody.ALLOWANCE_HOUSE, modelBody.ALLOWANCE_SUPPORT);
            if (result > 0)
            {
                return Json(new { success = true, data = modelBody, message = Human.Resource.Language.Update_salary_history_success });
            }
            else
            {
                return Json(new { success = false, message = Human.Resource.Language.Update_salary_history_fail });
            }
        }
        // --------------------------------------- delete salary_history_tab, employee_history = ajax ----------------------------------
        public ActionResult DeleteSalary(int id)
        {
            profile_do = new ProfileEmployee_DO();
            int result = 0;
            result = profile_do.DelSalary_History(id);
            if (result > 0)
            {
                return Json(new { success = true, message = Human.Resource.Language.Del_salary_history_success });
            }
            else
            {
                return Json(new { success = false, message = Human.Resource.Language.Del_salary_history_fail });
            }
        }
        public ActionResult DeleteEmp_His(int id)
        {
            profile_do = new ProfileEmployee_DO();
            int result = 0;
            result = profile_do.DeleteEmp_History(id);
            if (result > 0)
            {
                return Json(new { success = true, message = Human.Resource.Language.Del_work_process_success });
            }
            else
            {
                return Json(new { success = false, message = Human.Resource.Language.Del_work_process_fail });
            }
        }
        // ------------------------------------ Check employee = ajax -----------------------------------------
        public ActionResult CheckEmployeeCode(string empCode)
        {
            // Kiểm tra empCode trong danh sách nhân viên
            _empDO = new Employee_DO();
            string condition = "AND EMP_CODE =" + empCode;
            var result = _empDO.GetEmployeeByCondition(condition).Count();
            return Json(new { isExists = result });
        }
        public IEnumerable<Employee_Entities> GetEmployees([FromBody] CommonModel model)
        {
            _empDO = new Employee_DO();
            List<Employee_Entities> empList = _empDO.GetEmployees().ToList();
            var table = empList.AsQueryable();
            if (model.SEARCH_DIVID != 0)
            {
                table = table.Where(w => w.DIVISION_ID == model.SEARCH_DIVID);
            }
            if (model.SEARCH_DEPID != 0)
            {
                table = table.Where(w => w.DEPARTMENT_ID == model.SEARCH_DEPID);
            }
            return table;
        }
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        public JsonResult CheckEmpByCode(string emp_code)
        {
            EmployeeView_Entities result = _empDO.GetEmployeeByCode(emp_code).FirstOrDefault();
            LEAVE_DO leave_DO = new LEAVE_DO();
            float pncl = leave_DO.SEL_ANUAL_LEAVE_REMAIN(result.EMPLOYEE_ID);
            return Json(pncl, JsonRequestBehavior.AllowGet);
        }
    
        // POST api/values
        public void Post([FromBody] string value)
        {
        }
        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
