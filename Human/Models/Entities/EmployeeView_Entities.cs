using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class EmployeeView_Entities
    {
        public string IMG_WEB { get; set; }
        public decimal EMPLOYEE_ID { get; set; }
        public string FULLNAME { get; set; }
        public DateTime HIRE_DAY { get; set; }
        public string EMP_CODE { get; set; }
        public string DIVISION_NAME_EN { get; set; }
        public string DIVISION_NAME_VI { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_VI { get; set; }
        public string SECTION_NAME_EN { get; set; }
        public string SECTION_NAME_VI { get; set; }
        public string TitleName_en { get; set; }
        public string TitleName_vi { get; set; }
        public int DIVISION_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int SECTION_ID { get; set; }
        public int JOBTITLE_ID { get; set; }
        public string EMAIL { get; set; }
        public string OTP { get; set; }
        public int MyProperty { get; set; }
        public DateTime TIME_GET_OTP { get; set; }
        public DateTime TIME_LOGIN { get; set; }
    }
}