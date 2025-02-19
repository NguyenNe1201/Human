using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static System.Net.WebRequestMethods;
using Human.Models.Entities;
namespace Human.DO
{
    public class Employee_DO
    {

        DbHelper db;
        public IEnumerable<Employee_Entities> GetEmployees()
        {

            db = new DbHelper();
            return db.Executereader<Employee_Entities>("GetAllEmployee", new string[] { "" }, new object[] { });

        }
        public IEnumerable<Employee_Entities> GetEmployeeByCondition(string condition)
        {
            db = new DbHelper();
            return db.Executereader<Employee_Entities>("GetEmployeeByCondition", new string[] { "CONDITION" }, new object[] { condition });

        }
        public IEnumerable<EmployeeView_Entities> GetInfoEmployee(string condition)
        {
            db = new DbHelper();
            return db.Executereader<EmployeeView_Entities>("GetEmployeeByCondition", new string[] { "CONDITION" }, new object[] { condition });

        }
        public IEnumerable<ProfileEmployee_Entities> GetEmployeeByID(decimal emp_id)
        {
            db = new DbHelper();
            return db.Executereader<ProfileEmployee_Entities>("GetEmployeeByID", new string[] { "EMPLOYEEID" }, new object[] { emp_id });
        }
        public IEnumerable<EmployeeTab_Entities> GetAllEmployee_Tab()
        {
            db = new DbHelper();
            return db.Executereader<EmployeeTab_Entities>("GetAllEmployeeByHis", new string[] { "" }, new object[] { });
        }
        public IEnumerable<EmployeeTab_Entities> GetEmpViewBySectionID(int section_id)
        {
            db = new DbHelper();
            return db.Executereader<EmployeeTab_Entities>("GetEmpViewBySectionID", new string[] { "SECTION_ID" }, new object[] {section_id });
        }
        public IEnumerable<EmployeeView_Entities> GetEmployeeByCode(string EMP_CODE)
        {
            DbHelper db = new DbHelper();
            return db.Executereader<EmployeeView_Entities>("GetEmployeeByCode", new string[] { "EMP_CODE" }, new object[] { EMP_CODE });
        }

        public IEnumerable<EMP_Login_Entities> GetEmpLoginByEmail(string email)
        {
            db = new DbHelper();
            return db.Executereader<EMP_Login_Entities>("GetEmpLoginByEmail", new string[] { "EMAIL" }, new object[] { email });

        }
        public IEnumerable<EMP_Login_Entities> GetEmpLoginByPhone(string phone)
        {
            db = new DbHelper();
            return db.Executereader<EMP_Login_Entities>("GetEmpLoginByPhone", new string[] { "PHONE_NUMBER" }, new object[] { phone });

        }
        public int UpdateOTPEmp(string Email, string Emp_CODE, string OTP, DateTime TimeGetOTP)
        {
            db = new DbHelper();
            int i = 0;
            i = db.execStoreProcedure("Add_OtpByEmail", new string[] { "EMAIL", "EMPLOYEE_CODE", "OTP", "TIME_GET_OTP" }, new object[] { Email, Emp_CODE, OTP, TimeGetOTP });
            return i;

        }
        public int UpdateOTPEmp_phone(string Phone, decimal Emp_ID, string Emp_CODE, string OTP, DateTime TimeGetOTP)
        {
            db = new DbHelper();
            int i = 0;
            i = db.execStoreProcedure("Add_OtpByPhone", new string[] { "PHONE_NUMBER", "EMPLOYEE_ID", "EMPLOYEE_CODE", "OTP", "TIME_GET_OTP" }, new object[] { Phone, Emp_ID, Emp_CODE, OTP, TimeGetOTP });
            return i;

        }
         
        public int UpdateTimeLogin(string emp_code, DateTime TimeGetOTP, DateTime time_login)
        {
            db = new DbHelper();
            int i = 0;
            i = db.execStoreProcedure("UPD_EmpLogin", new string[] { "EMP_CODE", "TIME_GET_OTP", "TIME_LOGIN" }, new object[] { emp_code, TimeGetOTP, time_login });
            return i;
        }

    }
}
