using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class SalaryHistory_Entities
    {
        public decimal  SALARY_HISTORY_ID { get; set; }
        public decimal EMPLOYEE_ID { get; set; }
        public decimal SALARY_CAT_ID { get; set; }
        public decimal ALLOWANCE_FUNCTION { get; set; }
        public decimal BASIC_SALARY { get; set; }
        public decimal ALLOWANCE_JOB { get; set; }
        public decimal ALLOWANCE_PHONE { get; set; }
        public decimal ALLOWANCE_PETROL { get; set; }
        public decimal ALLOWANCE_CASHIER { get; set; }
        public decimal ALLOWANCE_HARM { get; set; }
        public DateTime BEGIN_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public string NOTE { get; set; }    
        public decimal ALLOWANCE_MEAL { get; set; }
        public decimal ALLOWANCE_HOUSE { get; set; }
        public decimal ALLOWANCE_SUPPORT { get; set; }

      
    }
}