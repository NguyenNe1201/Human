using Human.Common;
using Human.DO;
using Human.Models;
using Human.Models.Entities;
using ImageResizer;
using ImageResizer.Caching;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Twilio.TwiML.Messaging;
using static System.Collections.Specialized.BitVector32;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Human.Controllers
{
    public class SetupController : BaseController
    {
        SetupModel model = new SetupModel();
        User_DO user_do;
        Role_DO role_do;
        Section_DO section_do;
        Division_DO division_do;
        Department_DO department_do;
        Salary_DO salary_do;
        JobTitle_DO jobtitle_do;
        ProfileEmployee_DO profile_do;
        
        //--------------------------------------- View decentralize -------------------------------------
        public ActionResult Decentralize()
        {
            if (Session["EMP_POS"].ToString() == "DIR")
            {
                init_index();
                return View(model);
            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Index", "Home");
            }
        }
        public void init_index()
        {
            department_do = new Department_DO();
            section_do = new Section_DO();
            division_do = new Division_DO();

            model.LST_SECTION = section_do.GetAllSection().OrderBy(s => s.SECTION_ID).ToList();
            model.List_DEPARTMENT = department_do.GetAllDepartment().OrderBy(s => s.DEPARTMENT_ID).ToList();
            model.LST_DIVISION = division_do.GetAllDivision().OrderBy(s => s.DIVISION_ID).ToList();
        }
        // ---------------------------------- View category -----------------------------------------
        public ActionResult Category()
        {
            jobtitle_do = new JobTitle_DO();
            salary_do = new Salary_DO();
            model.LST_JOBTITLE = jobtitle_do.GetAllJobTitle().ToList();
            model.LST_SALARY_LEVEL = salary_do.GetSalary_level().ToList();
            init_index();
            return View(model);
        }
        //------------------------------------
        public ActionResult AddNewSection(int DEPARTMENT_ID, string SECTION_NAME_VI, string SECTION_NAME_EN)
        {
            int result = 0;
            section_do = new Section_DO();
            result = section_do.AddNewSection(SECTION_NAME_VI,SECTION_NAME_EN,DEPARTMENT_ID);
            if (result > 0)
            {
                 var section_list = section_do.GetAllSection().Where(w => w.SECTION_NAME_EN == SECTION_NAME_EN && w.SECTION_NAME_VI == SECTION_NAME_VI).FirstOrDefault();
               // var section_list = section_do.GetAllSection().ToList();
                return Json(new { success = true, data = section_list,message ="Thêm bộ phận thành công!"});
            }
            else
            {
                return Json(new { success = false, message = "Thêm bộ phận thất bại" });
            }
        }
        public ActionResult UpdateSection(int section_id_radio, int DEPARTMENT_ID, string SECTION_NAME_VI, string SECTION_NAME_EN)
        {
            int result = 0;
            section_do = new Section_DO();
            result = section_do.UpdateSection(section_id_radio, SECTION_NAME_VI,SECTION_NAME_EN,DEPARTMENT_ID);
           
            if (result > 0)
                {
                    var sectionByID = section_do.GetSectionByID(section_id_radio).FirstOrDefault();
                return Json(new { success = true, data = sectionByID, message = "Cập nhật bộ phận thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Cập nhật bộ phận thất bại!" });
            }
        }
        public ActionResult DeleteSection(int id)
        {
            section_do = new Section_DO();
            int result = 0;
            result = section_do.DeleteSection(id);
            if (result > 0)
            {
               
                return Json(new { success = true, message = "Xóa bộ phận thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Xóa bộ phận thấ bại!" });
            }
        }
        // ------------------------------------
        public ActionResult UpdateJob_Tab(int job_id_select, string TitleName_en, string TitleName_vi)
        {
            int result = 0;
            jobtitle_do = new JobTitle_DO();
            result = jobtitle_do.UpdateJobTitle(job_id_select, TitleName_vi,TitleName_en);
            if (result > 0)
            {
                var jobTitle_info = jobtitle_do.GetJobTitleByID(job_id_select).FirstOrDefault(); 
                return Json(new { success = true,data=jobTitle_info ,message = "Chập nhật chức vụ thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Cập nhật chức vụ thất bại!" });
            }
        }
        public ActionResult AddNewJob_Tab(string TitleName_vi, string TitleName_en)
        {
            int result = 0;
            jobtitle_do = new JobTitle_DO();
            result = jobtitle_do.AddJobTitle(TitleName_vi,TitleName_en);
            if (result > 0)
            {
                var jobtitle_info = jobtitle_do.GetAllJobTitle().Where(m => m.TitleName_en == TitleName_en && m.TitleName_vi == TitleName_vi).FirstOrDefault();
                return Json(new { success = true,data = jobtitle_info, message = "thêm chức vụ thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "thêm chức vụ thất bại!" });
            }
        }
        public ActionResult DeleteJob_Title(int id)
        {
            jobtitle_do = new JobTitle_DO();
            int result = 0;
            result = jobtitle_do.DeleteJob_Title(id);
            if (result > 0)
            {
                return Json(new { success = true, message = "Xóa chức vụ thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Xóa chức vụ thấ bại!" });
            }
        } 
        // ------------------------------ ------------------------------
        public ActionResult AddSalary_Level(string SALARY_CODE, decimal SALARY_VALUE)
        {
            salary_do = new Salary_DO();
            int result = 0;
            result = salary_do.AddNewSalary_Level(SALARY_CODE,SALARY_VALUE);
            if (result > 0)
            {
                var SalaryLv_info = salary_do.GetSalary_level().Where(i => i.SALARY_CODE == SALARY_CODE && i.SALARY_VALUE == SALARY_VALUE).FirstOrDefault();
                return Json(new { success = true,data = SalaryLv_info ,message = "Thêm bậc lương thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Thêm bậc lương thấ bại!" });
            }
        }
        public ActionResult UpdateSalary_Level(int ID,string SALARY_CODE, decimal SALARY_VALUE)
        {
            salary_do = new Salary_DO();
            int result = 0;
            result = salary_do.UpdateSalary_Level(ID,SALARY_CODE,SALARY_VALUE);
            if (result > 0)
            {
                return Json(new { success = true,data = new {ID,SALARY_CODE,SALARY_VALUE},message = "Cập nhật bậc lương thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Cập nhật bậc lương thấ bại!" });
            }
        }
        public ActionResult DeleteSalary_Level(int id) {
            salary_do = new Salary_DO();
            int result = 0;
            result = salary_do.DeleteSalary_Level(id);
            if (result > 0)
            {
                return Json(new { success = true, message = "Xóa bậc lương thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Xóa bậc lương thấ bại!" });
            }
        }
        // ---------------------------------- VIEW COLOR & LOGO --------------------------------------------
        public ActionResult Color()
        {
            return View();
        }
        public ActionResult UploadLogo(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string fileName = "logo.png";
                    string path = Path.Combine(Server.MapPath("~/Assets/img/logo/"), fileName);
                    using (var image = Image.FromStream(file.InputStream))
                    {
                        var settings = new ResizeSettings
                        {
                            MaxWidth = 1000,
                            MaxHeight = 1000,
                            Format = "png" // Định dạng đầu ra của ảnh (có thể là jpg, png, ...)
                        };
                        ImageBuilder.Current.Build(image, path, settings);
                    }
                    return Json(new { success = true, message = Human.Resource.Language.Logo_update_success, imagePath = "/Assets/img/logo/" + fileName });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = Human.Resource.Language.error_while_upload + ": " + ex.Message });
                }
            }
            else
            {
                // Xử lý khi không có tập tin được chọn
                return Json(new { success = false, message = "Không có tập tin được chọn" });
            }
        }
        //------------------------------------ thêm quyền hạn cho user -----------------------------------------------
        [ValidateAntiForgeryToken]
        public ActionResult Insert_power(string code_emp, int selectedDivision, List<int> selectedDepartments, List<string> selectedSections, List<string> check_input)
        {
            role_do = new Role_DO();
            int i = 0;
            if (code_emp != null)
            {
                role_do.DeleteEmpFunction_ByEmpCode(code_emp);
                role_do.DeleteEmpPower_ByEmpCode(code_emp);
            }
            if (selectedDepartments != null)
            {
                foreach (var section in selectedSections)
                {
                    // Tách chuỗi sectionId_departmentId thành sectionId và departmentId
                    string[] parts = section.Split('_');
                    if (parts.Length == 2)
                    {
                        int sectionId;
                        int departmentId;
                        // Ép kiểu từ chuỗi sang số nguyên
                        if (int.TryParse(parts[0], out sectionId) && int.TryParse(parts[1], out departmentId))
                        {
                            i = role_do.AddEmpPower(code_emp, selectedDivision, departmentId, sectionId);
                        }
                    }
                }
                //role_do.AddEmpFunction(code_emp, "staffBySection");
            }
            if (check_input != null)
            {
                foreach (var function in check_input)
                {
                    // Tách chuỗi  thành name_function và btn_function
                    string[] part = function.Split('_');
                    if (part.Length == 2)
                    {
                        string name_function = part[0];
                        string btn_function = part[1];
                        i = i + role_do.AddEmpFunction(code_emp, name_function, btn_function);
                    }
                }
            }
            if (i > 0)
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Granted_permissions_success, "success");
                Session["POWER_BY_EMP"] = role_do.GetPowerByEmpCode(code_emp).ToList();
                Session["FUNCTION_BY_EMP"] = role_do.GetFunctionEmp(code_emp).ToList();
            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Granted_permissions_failed, "error");
            }
            return RedirectToAction("Decentralize");
        }
    }
}