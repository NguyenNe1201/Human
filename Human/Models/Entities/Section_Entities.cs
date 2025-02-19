using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class Section_Entities
    {
        public int SECTION_ID { get; set; }
        public string SECTION_NAME_EN { get; set; }
        public string SECTION_NAME_VI { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int SHIFT_ID { get; set; }
        public string COST_CENTER { get; set; }
        public string CATEGORY { get; set; }
        public double SORT { get; set; }
        public string DEPARTMENT_NAME_VI { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
    }
}