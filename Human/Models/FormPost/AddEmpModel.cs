using Human.Controllers.PublicController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models
{
    public class AddEmpModel
    {

        public string EMP_CODE { get; set; }
        public DateTime HIRE_DAY { get; set; }
        public string LASTNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public DateTime DATE_OF_BIRTH { get; set; }
        public int SEX { get; set; }
        public string ID_CARDNUMBER { get; set; }
        public DateTime DATE_ISSUE { get; set; }
        public string EDUCATION { get; set; }
        public string DEGREE { get; set; }
        public string PERMANENT_ADDRESS { get; set; }
        public string TEMPORARY_ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string BANKACOUNT_NUMBER { get; set; }
        public string BANK_NAME { get; set; }
        public string TAX_NUMBER { get; set; }
        public string SOBHXH { get; set; }
        public string SOBHYT { get; set; }
        public string PLACE_ISSUE { get; set; }
        // quá trình công tác
        public int DIVISION_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int SECTION_ID { get; set; }
        public int JOBTITLE_ID { get; set; }
        public DateTime CONGTACTU { get; set; }
        public DateTime CONGTACDEN { get; set; }
        public  int CONGTAC { get; set; }
        public DateTime DATE_QUIT { get; set; }
        public string QUIT_REASON { get; set; }
        public int QUIT { get; set; }
        public int PROBATION { get; set; }
        // form lịch sử lương
        public int SALARY_CAT_ID { get; set; }
        public DateTime BEGIN_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public decimal BASIC_SALARY { get; set; }
        public decimal ALLOWANCE_JOB { get; set; }
        public decimal ALLOWANCE_HOUSE { get; set; }
        public decimal ALLOWANCE_PETROL { get; set; }
        public decimal ALLOWANCE_PHONE { get; set; }
        public decimal ALLOWANCE_SUPPORT { get; set; }
        public decimal ALLOWANCE_MEAL { get; set; }
        public decimal ALLOWANCE_CASHIER { get; set; }
        public string NOTE { get; set; }
      
    }
}