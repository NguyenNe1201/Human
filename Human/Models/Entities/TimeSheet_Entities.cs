using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class TimeSheet_Entities
    {
        public string EMPLOYEE_NUMBER { get; set; }
        public string FULLNAME { get; set; }
        public DateTime DATECHECK { get; set; }
        public decimal MACHINE_NUMBER { get; set; }
        public DateTime TIME_TEMP { get; set; }
        public string NM { get; set; }
        public string EMP_CODE { get; set; }

        public string EVENT_TYPE { get; set; }
        public double TNAKEY { get; set; }
    }
}