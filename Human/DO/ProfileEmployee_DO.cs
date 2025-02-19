
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Human.Models;
using Human.Models.Entities;

namespace Human.DO
{
    public class ProfileEmployee_DO
    {
        public DbHelper db;
        public IEnumerable<ProfileEmployee_Entities> ProfileEmployeeID(decimal employeeId)
        {
            db = new DbHelper();
            return db.Executereader<ProfileEmployee_Entities>("sp_SEL_ProfileEmployeeID", new string[] { "EMPLOYEE_ID" }, new object[] { employeeId });
        }
        public IEnumerable<EmployeeHistory_Entities> GetEmployeeHistoryByID(decimal EMPLOYEE_ID)
        {
            db = new DbHelper();
            return db.Executereader<EmployeeHistory_Entities>("GetAllHistoryByEmpID", new string[] { "EMPLOYEE_ID" }, new object[] { EMPLOYEE_ID });
        }
    
        public IEnumerable<ProfileEmployee_Entities> UpdateAvatarEmployeeID(string img_web, decimal employeeId)
        {
            db = new DbHelper();
            return db.Executereader<ProfileEmployee_Entities>("UPD_AVATAR_EMP", new string[] { "IMG_WEB", "EMPLOYEE_ID" }, new object[] { img_web, employeeId });
        }
        public int AddEmployee_Tab(string Emp_CODE, DateTime hire_day, string firstname, DateTime birthday, int sex, string id_card, DateTime date_issue, string place_issue,
              string education, string degree, string permanent_address, string temporary_address, string phone_number, string mobile_number, string email, string bankacount, string bank_name, string tax_number,
              string bhxh, string bhyt, int department_id, int section_id, int job_id, int probation, int congtac_status, string congtactu, string congtacden, string quit_reason, int quit_status, string date_quit,int sa_cat_id,decimal basic_salary, decimal allowance_job, decimal allowance_house, decimal allowance_petrol,
              decimal allowance_phone, decimal allowance_cashier, string begin_date, string end_date, decimal allowance_meal, decimal allowance_sup, string note)
        {
            db = new DbHelper();
            int i = 0;
            i = db.execStoreProcedure("AddEmployee_Tab", new string[] { "EMP_CODE", "HIRE_DAY", "FIRSTNAME", "DATE_OF_BIRTH", "SEX", "ID_CARDNUMBER", "DATE_ISSUE", "PLACE_ISSUE", "EDUCATION", "DEGREE","PERMANENT_ADDRESS", "TEMPORARY_ADDRESS", "PHONE_NUMBER","MOBILE_NUMBER", "EMAIL",
                  "BANKACOUNT_NUMBER", "BANK_NAME", "TAX_NUMBER", "SOBHXH", "SOBHYT", "DEPARTMENT_ID", "SECTION_ID", "JOBTITLE_ID","PROBATION","CONGTAC","CONGTACTU","CONGTACDEN","QUIT_REASON","QUIT","DATE_QUIT","SALARY_CAT_ID","BASIC_SALARY","ALLOWANCE_JOB","ALLOWANCE_HOUSE",
                  "ALLOWANCE_PETROL","ALLOWANCE_PHONE","ALLOWANCE_CASHIER","BEGIN_DATE","END_DATE","ALLOWANCE_MEAL","ALLOWANCE_SUPPORT","NOTE"}
                    , new object[] { Emp_CODE,hire_day,firstname,birthday,sex,id_card,date_issue,place_issue,education,degree,permanent_address,temporary_address,phone_number,mobile_number,email,bankacount,bank_name,tax_number,bhxh,bhyt,department_id,section_id,job_id,
                      probation,congtac_status,congtactu,congtacden,quit_reason,quit_status,date_quit,sa_cat_id,basic_salary,allowance_job,allowance_house,allowance_petrol,allowance_phone,allowance_cashier,begin_date,end_date,allowance_meal,allowance_sup,note});
            return i;

        }
        public int UpdateEmp_History(decimal history_id,decimal emp_id,int division_id,int department_id,int section_id,int job_id, int probation, int quit,string date_quit,
            string quit_reason, string congtactu, string congtacden)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("UPD_EMPLOYEE_HISTORY", new string[] { "HISTORY_ID","EMPLOYEE_ID","DIVISION_ID", "DEPARTMENT_ID", "SECTION_ID", "JOBTITLE_ID", 
                    "PROBATION", "QUIT", "DATE_QUIT", "QUIT_REASON", "CONGTACTU", "CONGTACDEN" }, 
                    new object[] {history_id,emp_id,division_id, department_id, section_id, job_id, probation, quit, date_quit, quit_reason, congtactu, congtacden });
                return i;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int AddEmp_History( decimal emp_id,int division_id,int department_id,int section_id,int job_id,int probation,int quit,string date_quit,
            string quit_reason,int congtac,string congtactu,string congtacden)
        {
            db = new DbHelper();
            int i = 0;
            i = db.execStoreProcedure("AddNewEmp_History", new string[] {"EMPLOYEE_ID","DIVISION_ID","DEPARTMENT_ID","SECTION_ID","JOBTITLE_ID", "PROBATION", "QUIT",
               "DATE_QUIT", "QUIT_REASON","CONGTAC", "CONGTACTU", "CONGTACDEN" }
                    , new object[] {emp_id,division_id,department_id,section_id,job_id,probation,quit,date_quit,quit_reason,congtac,congtactu,congtacden });
            return i;

        }
        public int UpdateEmp_tab(decimal employeeId,DateTime hire_day, string firstname, DateTime birthday, int sex, string id_carnumber, string phone,string mobile_phone, DateTime date_issue,string place_issue, string email, string bank_account, string bank_name, string tax_number, string bhxh, string bhyt, string permanent_address, string temporary_address, string education,string degree)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("UPD_EMPLOYEE_TAB", new string[] { "EMPLOYEE_ID", "HIRE_DAY", "FIRSTNAME", "DATE_OF_BIRTH", "SEX", "ID_CARDNUMBER","DATE_ISSUE", "PLACE_ISSUE", "PHONE_NUMBER", "MOBILE_NUMBER", "EMAIL", "BANKACOUNT_NUMBER", "BANK_NAME", "TAX_NUMBER", "SOBHXH", "SOBHYT", "PERMANENT_ADDRESS", "TEMPORARY_ADDRESS", "EDUCATION", "DEGREE" }, new object[] { employeeId, hire_day, firstname, birthday, sex, id_carnumber, date_issue,place_issue, phone, mobile_phone, email, bank_account, bank_name, tax_number, bhxh, bhyt, permanent_address, temporary_address, education,degree });
                return i;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int AddSalary_History(decimal emp_id,int salary_cat_id,decimal basic_salary, decimal allwance_job,
            decimal allowance_phone, decimal allowance_petrol, decimal allowance_cashier,string begin_date,string end_date,string note,decimal allowance_meal,decimal allowance_house,decimal allowance_sup )
        {
            db = new DbHelper();
            int i = 0;
            i = db.execStoreProcedure("AddSalary_History", new string[] {"EMPLOYEE_ID","SALARY_CAT_ID","BASIC_SALARY",
                "ALLOWANCE_JOB","ALLOWANCE_PHONE","ALLOWANCE_PETROL","ALLOWANCE_CASHIER","BEGIN_DATE","END_DATE","NOTE","ALLOWANCE_MEAL","ALLOWANCE_HOUSE","ALLOWANCE_SUPPORT"}
                    , new object[] { emp_id,salary_cat_id,basic_salary,allwance_job,allowance_phone,allowance_petrol,allowance_cashier,begin_date,end_date,note,allowance_meal,allowance_house,allowance_sup });
            return i;

        }
        public int UpdateSalary_His(decimal salary_his_id,decimal employeeId,int salary_cat_id, decimal basic_salary, decimal allwance_job,
            decimal allowance_phone, decimal allowance_petrol, decimal allowance_cashier, string begin_date, string end_date, string note, decimal allowance_meal, decimal allowance_house, decimal allowance_sup)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("UPD_SALARY_HISTORY_TAB", new string[] { "SALARY_HISTORY_ID","EMPLOYEE_ID","SALARY_CAT_ID" , "BASIC_SALARY",
                "ALLOWANCE_JOB","ALLOWANCE_PHONE","ALLOWANCE_PETROL","ALLOWANCE_CASHIER","BEGIN_DATE","END_DATE","NOTE","ALLOWANCE_MEAL","ALLOWANCE_HOUSE","ALLOWANCE_SUPPORT"}, new object[] { salary_his_id,employeeId,salary_cat_id,
                    basic_salary, allwance_job, allowance_phone, allowance_petrol, allowance_cashier, begin_date, end_date, note, allowance_meal, allowance_house, allowance_sup });
                return i;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int DeleteEmployee(decimal EMPLOYEE_ID)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("DeleteEmployee", new string[] { "EMPLOYEE_ID" }, new object[] { EMPLOYEE_ID });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
        public int DeleteEmp_History(decimal HISTORY_ID)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("DeleteEmp_History", new string[] { "HISTORY_ID" }, new object[] { HISTORY_ID });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
        public int DelSalary_History(decimal SALARY_HISTORY_ID)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("DeleteSalary_History", new string[] { "SALARY_HISTORY_ID" }, new object[] { SALARY_HISTORY_ID });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
    }
}