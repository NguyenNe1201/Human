using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class PowerView_Entities
    {
        public decimal POWER_ID { get; set; }
        public string EMP_CODE { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME_VI { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public int SECTION_ID { get; set; }
        public string SECTION_NAME_EN { get; set; }

        public string SECTION_NAME_VI { get; set; }

    }
}