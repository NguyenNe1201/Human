using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models
{
    public class SalaryModel
    {
      public Salary_Entities Salary { get; set; }

        public string TEXT_FROMDATE { get; set; }
        public string TEXT_TODATE { get; set; }
        public string MONTH { get; set; }
    }
}