using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class EmployeeHistory_Entities
    {
        public decimal HISTORY_ID { get; set; }
        public decimal EMPLOYEE_ID { get; set; }
        public int DIVISION_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int SECTION_ID { get; set; }
        public int JOBTITLE_ID { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_VI { get; set; }
        public string SECTION_NAME_VI { get; set; }
        public string SECTION_NAME_EN { get; set; }
        public string TitleName_vi { get; set; }
        public string TitleName_en { get; set; }
        public DateTime CONGTACTU { get; set; }
        public DateTime CONGTACDEN { get; set; }
        public string QUIT_REASON { get; set; }
        public DateTime DATE_QUIT { get; set; }
        public bool? CONGTAC { get; set; }
        public bool? PROBATION { get; set; }

        public bool? QUIT { get; set; }

    }
}