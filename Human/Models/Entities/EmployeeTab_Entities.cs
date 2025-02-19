using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class EmployeeTab_Entities
    {
        public decimal EMPLOYEE_ID { get; set; }
        public string FIRSTNAME{ get; set; }
        public string EMP_CODE { get; set; }
        public string SEX { get; set; }
        public DateTime HIRE_DAY { get; set; }
        public DateTime DATE_OF_BIRTH { get; set; }
        public DateTime CONGTACTU { get; set; }
        public DateTime CONGTACDEN { get; set; }
        public string PHONE_NUMBER { get; set; }
        public int JOBTITLE_ID { get; set; }
        public string TitleName_en { get; set; }
        public string TitleName_vi { get; set; }
        public int SECTION_ID { get; set; }
        public string SECTION_NAME_VI { get; set; }
        public string SECTION_NAME_EN { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_VI { get; set; }
        public int DIVISION_ID { get; set; }
        public string DIVISION_NAME_EN { get; set; }
        public string DIVISION_NAME_VI { get; set; }
    }
}