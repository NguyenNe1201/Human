using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human.DO
{
    public  class TimeSheet_DO
    {
        public DbHelper db;
        DataTable datatable;
        public IEnumerable<TimeSheet_Entities> TimeSheetMothEmployeeId(string employee_code, string tablename)
        {
            db = new DbHelper();
            return db.Executereader<TimeSheet_Entities>("sp_SEL_TimeSheetMothEmployeeID", new string[] { "EMPLOYEE_NUMBER", "TableName" }, new object[] { employee_code, tablename });
        }
        public IEnumerable<TimeSheetDay_Entities> GetTimeSheetDay(string tablename, string condition)
        {
            db = new DbHelper();
            return db.Executereader<TimeSheetDay_Entities>("sp_rp_TimeKeeper", new string[] { "TableName", "Condition" }, new object[] { tablename, condition });
        }
        public IEnumerable<TimeSheet_Entities> TimeLogListMonthEmpCode(string employee_code)
        {
            db = new DbHelper();
            return db.Executereader<TimeSheet_Entities>("sp_SEL_TimeLogListMonthEmpCode", new string[] { "Employee_Code" }, new object[] { employee_code});
        }
    }
}
