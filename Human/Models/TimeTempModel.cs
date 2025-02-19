using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models
{
    public class TimeTempModel
    {

        public decimal ID { get; set; }
        public string CARD_NUMBER { get; set; }
        public string EMPLOYEE_NUMBER { get; set; }
        public DateTime DATECHECK { get; set; }
        public TimeSpan TIMECHECK { get; set; }
        public string MACHINE_NUMBER { get; set; }
        public DateTime TIME_TEMP { get; set; }
        public string FULLNAME { get; set; }
        public string ENGLISH_NAME { get; set; }
     
    }
}