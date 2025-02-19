using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Human.Models.Entities;
using MimeKit.Cryptography;

namespace Human.Models
{
    public class ProfileEmployeeModel
    {
        public ProfileEmployee_Entities EMP_PROFILE { get; set; }
        public string AVATAR_EMP { get; set; }
        public List<Department_Entities> LST_DEPARTMENT { get; set; }
        public List<Section_Entities> LST_SECTION { get; set; }
        public List<Division_Entities> LST_DIVISION { get; set; }
        public List<Job_title_Entities> LST_JOBTITLE { get; set; }
        public Section_Entities SECTION_BY_ID { get; set; }
        public List<ProfileEmployee_Entities> LIST_EMP_CONDITION { get; set; }
        public List<TimeSheet_Entities> LST_TIMESHEETVIEW { get; set; }
        public EmployeeView_Entities EMP_INFO { get; set; }
        public List<Emp_Function_Entities> FUNCTION_EMP { get; set; }
        public List<Salary_Level_Entities> LST_SALARY_LEVEL { get; set; }
        public List<Salary_Ext_Entities> LST_PC { get; set; }
        public List<EmployeeHistory_Entities> LST_WORKING { get; set; }
        public List<SalaryHistory_Entities> LST_SALARY_HISTORY { get; set; }
        public List<ProfileEmployee_Entities> LIST_WORKING { get; set; }
        public List<EmployeeTab_Entities> LST_ALL_EMPLOYEE { get; set; }
        public List<ContractType_Entities> LST_CONTRACT_TYPE { get; set; }
        public List<Contract_Entities> LST_CONTRACT_EMPID { get; set; }
    }
}