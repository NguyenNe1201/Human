using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class Salary_Ext_Entities
    {
        public int ID { get; set; }
        public string SALARY_CODE { get; set; }
        public decimal SALARY_VALUE { get; set; }
        public string SALARY_LEVEL { get; set; }
        public decimal ALLOWANCE { get; set; }
        public DateTime DATEMODIFY { get; set; }

    }
}