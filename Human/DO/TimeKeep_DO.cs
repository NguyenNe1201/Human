using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Human.Models.Entities;
namespace Human.DO
{
    public class TimeKeep_DO
    {
        public DbHelper db;
        DataTable datatable;
        public IEnumerable<TimeKeep_Entities> TimekeepMothEmployeeId(string employee_code, string tablename)
        {
            db = new DbHelper();
            return db.Executereader<TimeKeep_Entities>("sp_SEL_TimeKeepMothEmployeeID", new string[] { "EMP_CODE", "TableName" }, new object[] { employee_code, tablename });
        }
        
    }
}