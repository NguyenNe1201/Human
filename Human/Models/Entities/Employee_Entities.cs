using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class Employee_Entities
    {
        public decimal EMPLOYEE_ID { get; set; }
        public string FULLNAME { get; set; }
        public string PHONE_NUMBER { get; set; }
        public DateTime HIRE_DAY { get; set; }
        public string EMP_CODE { get; set; }
        public string DIVISION_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string SECTION_NAME_EN { get; set; }
        public string TitleName_en { get; set; }
        public int DIVISION_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int SECTION_ID { get; set; }
        public int JOBTITLE_ID { get; set; }
        public string EMAIL { get; set; }
        public string OTP { get; set; }
        public int MyProperty { get; set; }
        public DateTime TIME_GET_OTP { get; set; }
        public DateTime TIME_LOGIN { get; set; }
        public string USERNAME { get; set; }
        public string PASS_WORD { get; set; }
    }
}