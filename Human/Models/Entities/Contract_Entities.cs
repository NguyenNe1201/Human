using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class Contract_Entities
    {
        public int CONTRACT_ID { get; set; }
        public string CONTRACT_NO { get; set; }
        public decimal EMPLOYEE_ID { get; set; }
        public int CONTRACTTYPE_ID { get; set; }
        public DateTime ISSUEDATE { get; set; }
        public DateTime DEADLINE { get; set; }
        public string REMARK { get; set; }
        public string BASEON { get; set; }
        public string BASEON_KYNGAY { get; set; }
        public string CONTRACTNAME_VI { get; set; }
        public string CONTRACTNAME_EN { get; set; }
        /*  public bool? SIGNED { get; set; }
          public int STR1 { get; set; }
          public int STR2 { get; set; }
          public int SALARY_HISTORY_ID  { get; set; }
          public bool? INPUT_MAIN { get; set; }
     
        public int DIV { get; set; }
        public bool? BASIC_SALARY_85 { get; set; }*/

    }
}