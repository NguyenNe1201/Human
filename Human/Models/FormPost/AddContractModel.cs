using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.FormPost
{
    public class AddContractModel
    {
        public string CONTRACT_NO { get; set; }
        public decimal EMPLOYEE_ID { get; set; }
        public int CONTRACTTYPE_ID { get; set; }
        public DateTime ISSUEDATE { get; set; }
        public DateTime DEADLINE { get; set; }
        public string REMARK { get; set; }
        public string BASEON { get; set; }
        public string BASEON_KYNGAY { get; set; }
        public int CONTRACT_ID { get; set; }
    }
}