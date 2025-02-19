using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Human.Models.Entities;
namespace Human.Models
{
    public class TimeKeepModel
    {
        public List<TimeKeep_Entities> LIST_TIMEKEEEPVIEW { get; set; }
        public string MONTH_TIMEKEEP { get; set; }
        public EmployeeView_Entities EMP_VIEW { get; set; }
    }
}