using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human.DO
{
    public class Salary_DO
    {
        public DbHelper db;
        public IEnumerable<Salary_Entities> GetSalaryByEmpCode(string empCode, string yearmonth)
        {
            DbHelper db = new DbHelper();
            return db.Executereader<Salary_Entities>("GetSalaryEmp_ByCode", new string[] { "EMPCODE", "LuongThang" }, new object[] { empCode, yearmonth });
   
        }
        public IEnumerable<Salary_Level_Entities> GetSalary_level()
        {
            DbHelper db = new DbHelper();
            return db.Executereader<Salary_Level_Entities>("GetAllSalary_level", new string[] { "" }, new object[] { });

        }
        public IEnumerable<SalaryHistory_Entities> GetAllSalary_History()
        {
            DbHelper db = new DbHelper();
            return db.Executereader<SalaryHistory_Entities>("GetAllSalary_History", new string[] { "" }, new object[] { });

        }
        public IEnumerable<Salary_Ext_Entities> GetSalary_ext()
        {
            DbHelper db = new DbHelper();
            return db.Executereader<Salary_Ext_Entities>("GetAllSalary_Ext", new string[] { "" }, new object[] { });

        }
        public int AddNewSalary_Level(string salary_code, decimal salary_value)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("AddNewSalary_Level", new string[] { "SALARY_CODE", "SALARY_VALUE" }, new object[] { salary_code,salary_value });

            return i;
        }
        public int UpdateSalary_Level(int id,string salary_code, decimal salary_value)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("UpdateSalary_Level", new string[] { "ID","SALARY_CODE", "SALARY_VALUE" }, new object[] { id,salary_code, salary_value });

            return i;
        }
        public int DeleteSalary_Level(int id)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("DeleteSalary_Level", new string[] { "ID" }, new object[] { id });

            return i;
        }
    }
}
