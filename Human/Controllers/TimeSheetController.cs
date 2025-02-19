using Human.Models;
using Human.Models;
using Human.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using OfficeOpenXml;
using System.IO;
using System.Globalization;
using OfficeOpenXml.Style;
using Human.Common;

namespace Human.Controllers
{
    public class TimeSheetController : BaseController
    {
        TimeSheetModel model = new TimeSheetModel();
        TimeSheet_DO timesheet_DO;
        Department_DO department_do;
        Section_DO section_do;
        Employee_DO _empDO;
        JobTitle_DO job_do;
        
        // loglist theo từng nhân viên 
        public ActionResult ListTimeSheet()
        {
            init();
            return View(model);
        }
        public void init()
        {
            try
            {
                timesheet_DO = new TimeSheet_DO();
                var emp_code = Session["EMP_CODE"];
                if (emp_code != null)
                {
                    // Lấy năm và tháng hiện tại
                    int currentYear = DateTime.Now.Year;
                    int currentMonth = DateTime.Now.Month;
                    // Tạo chuỗi name_table từ năm và tháng hiện tại
                    string name_table = currentYear.ToString() + currentMonth.ToString("D2");
                    // Lấy dữ liệu từ BO
                    var timesheetList = timesheet_DO.TimeSheetMothEmployeeId(emp_code.ToString(), name_table);
                    // Tạo đối tượng TimesheetModel và gán danh sách dữ liệu cho nó
                    model.LIST_TIMESHEETVIEW = timesheetList.ToList();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public ActionResult SearchTimeSheet(string SEARCH_MONTH, string SEARCH_YEAR)
        {
            try
            {
                timesheet_DO = new TimeSheet_DO();
                var emp_code = Session["EMP_CODE"];
                if (SEARCH_MONTH == "" || SEARCH_YEAR == "")
                {
                  //  SetAlert(Human.Resource.Language.Please_select_complete_info, "warning");
                    CommonCheck.SetAlert(this, Human.Resource.Language.Please_select_complete_info, "warning");
                }
                else
                {
                    if (emp_code != null)
                    {
                        string name_table = SEARCH_YEAR + SEARCH_MONTH;
                        var timesheetList = timesheet_DO.TimeSheetMothEmployeeId(emp_code.ToString(), name_table);
                        ViewData["SEARCH_DATE"] = SEARCH_MONTH + "/" + SEARCH_YEAR;
                        if (timesheetList == null)
                        {
                           // SetAlert("Bảng chấm công tháng " + SEARCH_MONTH + "/" + SEARCH_YEAR + " chưa hoàn thành!", "warning");
                            CommonCheck.SetAlert(this, "Bảng chấm công tháng " + SEARCH_MONTH + "/" + SEARCH_YEAR + " chưa hoàn thành!", "warning");
                        }
                        else
                        {
                            // Tạo đối tượng TimeKeepModel và gán danh sách dữ liệu cho nó
                            model.LIST_TIMESHEETVIEW = timesheetList.ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View("ListTimeSheet", model);
        }
        // hàm lấy danh sách department, section
        public void getInfo()
        {
            department_do = new Department_DO();
            section_do = new Section_DO();
            job_do = new JobTitle_DO();
            model.LST_SECTION = section_do.GetAllSection().ToList();
            model.LST_DEPARTMENT = department_do.GetAllDepartment().ToList();
            model.LST_JOB = job_do.GetAllJobTitle().ToList();
        }
        // chấm công ngày
        public ActionResult TimeSheetDay()
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;
            if (CommonCheck.Check_function_read(lst_fuction_emp, "timekeep"))
            {
                getInfo();
                return View(model);
            }
            else
            {
              //  SetAlert(Human.Resource.Language.You_not_have_this_right, "error");
                CommonCheck.SetAlert(this, Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Index", "Home");
            }
            
        }
        public ActionResult SearchTimeKeepDay(DateTime start_date, DateTime end_date, string select_department, string select_section,string select_job)
        {
            getInfo();
            timesheet_DO = new TimeSheet_DO();
            string table_name = "";
            //table_name = "TIMESHEET_MONTH_" + start_date.ToString("yyyy") + start_date.ToString("MM");
            table_name = "TIMESHEET_MONTH_202311";
            string condition = "";
            if (start_date.ToString("dd/MM/yyyy") != "01/01/0001" && end_date.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                condition += "WHERE TIMESHEET_MONTH.DATEOFMONTH BETWEEN '" + start_date.ToString("yyyy-MM-dd") + "' AND '" + end_date.ToString("yyyy-MM-dd") + "'";
                Session["DATE_END"] = end_date;
                Session["DATE_START"] = start_date;
            }
            if (select_department != "0")
            {
                condition += "AND view_EMPLOYEE_INFO.DEPARTMENT_ID=" + select_department;
                 Session["DEPARTMENT_ID_SEARCH"] = select_department;
            }
            if (select_section != "0")
            {
                condition += "AND view_EMPLOYEE_INFO.SECTION_ID=" + select_section;
                Session["SECTION_ID_SEARCH"] = select_section;
            }
            if (select_job != "0")
            {
                condition += "AND view_EMPLOYEE_INFO.JOBTITLE_ID=" + select_job;
                Session["JOB_ID_SEARCH"] = select_job;
            }
            var lst_timekeep = timesheet_DO.GetTimeSheetDay(table_name, condition);
            if (lst_timekeep.Count() > 0)
            {
                model.LST_TIMEKEEPDAY = lst_timekeep.ToList();
                return View("TimeSheetDay", model);
            }
            else{
             //   SetAlert("Dữ liệu chấm công tháng này chưa được cập nhật!", "warning");
                CommonCheck.SetAlert(this, "Dữ liệu chấm công tháng này chưa được cập nhật!", "warning");
                return View("TimeSheetDay", model);
            }
        }
        public ActionResult GenerateExcel(string SEARCH_DATE)
        {
            // Đường dẫn đến template Excel của bạn
            string templatePath = Server.MapPath("~/Resource/Excel_exp/Template_Exp_TimesheetMoth.xlsx");
            // Load template Excel
            FileInfo templateFile = new FileInfo(templatePath);
            // get thông tin nhân viên
            var _model = new TimeSheetModel();
            timesheet_DO = new TimeSheet_DO();
            _empDO = new Employee_DO();
            var emp_code = Session["EMP_CODE"];
            string con_str = " AND EMP_CODE='" + emp_code + "'";
            var emp = _empDO.GetInfoEmployee(con_str).FirstOrDefault();
            // lấy công tháng từ search_date
            DateTime date = DateTime.ParseExact(SEARCH_DATE, "MM/yyyy", CultureInfo.InvariantCulture);
            string name_table = date.ToString("yyyyMM");
            // lấy dữ liệu từ model
            _model.LIST_TIMESHEETVIEW = timesheet_DO.TimeSheetMothEmployeeId(emp_code.ToString(), name_table).ToList();
            // Tạo một gói Excel
            using (ExcelPackage package = new ExcelPackage(templateFile))
            {

                /* ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("data");*/
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault(); // Lấy sheet đầu tiên từ template
                // in thêm dữ liệu vào title
                worksheet.Cells[8, 2].Value = "Ngày in: " + DateTime.Now.ToString("dd/MM/yyyy");
                worksheet.Cells[5, 7].Value = "Họ và tên (Fullname): " + emp.FULLNAME.ToString();
                worksheet.Cells[6, 7].Value = "Phòng ban (Department): " + emp.DEPARTMENT_NAME_VI.ToString();
                worksheet.Cells[7, 7].Value = "Bộ phận (Section): " + emp.SECTION_NAME_VI.ToString();
                worksheet.Cells[8, 7].Value = "Chấm công tháng (Month): " + SEARCH_DATE.ToString();
                // Đổ dữ liệu từ model vào Excel
                int rowIndex = 12;
                int i = 1;
                foreach (var item in _model.LIST_TIMESHEETVIEW)
                {
                    worksheet.Cells[rowIndex, 4].Value = i;
                    worksheet.Cells[rowIndex, 5].Value = item.EMPLOYEE_NUMBER;
                    worksheet.Cells[rowIndex, 6].Value = item.DATECHECK.ToString("dd/MM/yyyy");
                    worksheet.Cells[rowIndex, 7].Value = item.TIME_TEMP.ToString("HH:mm:ss");
                    worksheet.Cells[rowIndex, 8].Value = item.MACHINE_NUMBER;
                    // Định dạng các ô dữ liệu
                    using (var range = worksheet.Cells[rowIndex, 4, rowIndex, 8])
                    {
                        range.Style.Font.Size = 12; // Đặt font size
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa ngang
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center; // Căn giữa dọc
                        range.Style.WrapText = true; // Xuống dòng tự động nếu cần
                        range.Style.Fill.PatternType = ExcelFillStyle.None; // đặt lại viền của ô
                    }
                    // Đặt chiều cao hàng
                    worksheet.Row(rowIndex).Height = 30;
                    rowIndex++;
                    i++;
                }
                // Tạo tên tệp Excel dưới dạng "timekeep_emp_code_date.xlsx"
                string fileName = "timesheet_" + emp_code + "_" + name_table + ".xlsx";
                // Ghi dữ liệu vào Response để tải xuống
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();

            }
            // Trả về một đối tượng null trống để thoả mãn yêu cầu trả về một giá trị
            return null;
        }
    }
}
