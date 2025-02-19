using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Human.DO
{
    public class Role_DO
    {
        public DbHelper db;
        public IEnumerable<UserRole_Entities> GetUserRole()
        {
            db = new DbHelper();
            return db.Executereader<UserRole_Entities>("sp_SEL_USER_ROLE", new string[] { "" }, new object[] { });
        }
        public IEnumerable<Emp_Power_Entities> GetEmpRole()
        {
            db = new DbHelper();
            return db.Executereader<Emp_Power_Entities>("sp_SEL_EMP_ROLE", new string[] { "" }, new object[] { });
        }
        public IEnumerable<PowerView_Entities> GetPowerByEmpCode(string emp_code)
        {
            db = new DbHelper();
            return db.Executereader<PowerView_Entities>("GetPower_User", new string[] { "EMPLOYEE_CODE" }, new object[] { emp_code });
        }
        public int AddEmpPower( string emp_code, int id_division,int id_department,int id_section)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("Add_EMP_POWER", new string[] { "EMPLOYEE_CODE", "DEPARTMENT_ID", "DIVISION_ID", "SECTION_ID" }, new object[] { emp_code,id_department,id_division,id_section });

            return i;
        }
        public int AddEmpFunction(string emp_code, string name_function, string btn_function)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("Add_EMP_FUNCTION", new string[] { "EMPLOYEE_CODE", "NAME_FUNCTION", "BTN_FUNCTION" }, new object[] { emp_code, name_function, btn_function });

            return i;
        }
        public IEnumerable<Emp_Function_Entities> GetFunctionEmp(string employee_code)
        {
            db = new DbHelper();
            return db.Executereader<Emp_Function_Entities>("GetFunction_User", new string[] { "EMP_CODE" }, new object[] { employee_code });
        }
        public int DeleteEmpFunction(int func_id)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("sp_DEL_EMP_FUNCTION", new string[] { "EMP_FUNCTION_ID" }, new object[] { func_id });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
        public int DeleteEmpFunction_ByEmpCode(string emp_code)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("sp_DEL_EMP_FUNCTION_EMPCODE", new string[] { "EMP_CODE" }, new object[] { emp_code });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
        public int DeleteEmpPower_ByEmpCode(string emp_code)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("sp_DEL_EMP_POWER_EMPCODE", new string[] { "EMP_CODE" }, new object[] {emp_code });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
        public int DeleteEmpPower(int power_id)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("sp_DEL_EMP_POWER", new string[] { "EMP_POWER_ID" }, new object[] { power_id });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
    }
}