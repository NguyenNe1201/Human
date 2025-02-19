using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Areas.Admin.Models
{
    public class EmployeeViewModel
    {
        public int LEAVE_ID { get; set; }
        public decimal EMPLOYEE_ID { get; set; }
        public int KINDLEAVE_ID { get; set; }
        public double HOURS { get; set; }
        public DateTime LEAVE_STARTDATE { get; set; }
        public DateTime LEAVE_ENDDATE { get; set; }
        public string REASON { get; set; }
        public double ANNUAL_LEAVE_USED { get; set; }
        public string COM_MONTH { get; set; }
        public string COM_YEAR { get; set; }
        public string COM_YEAR_MONTH { get; set; }
        public int STATUS_L { get; set; }
        public string FULLNAME { get; set; }
        public string EMP_CODE { get; set; }
        public int SUP_STATUS { get; set; }
        public DateTime SUP_DATETIME { get; set; }
        public int MAN_STATUS { get; set; }
        public DateTime MAN_DATETIME { get; set; }
        public int DIR_STATUS { get; set; }
        public DateTime DIR_DATETIME { get; set; }
        public string DIVISION_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string SECTION_NAME_EN { get; set; }
        public string TitleName_en { get; set; }
        public int DIVISION_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int SECTION_ID { get; set; }
        public int JOBTITLE_ID { get; set; }
        public string NAMELEAVE_VI { get; set; }
    }
}