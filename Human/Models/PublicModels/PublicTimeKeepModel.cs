using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.PublicModels
{
    public class PublicTimeKeepModel
    {
        public List<TimeSheet_Entities> LST_TIMESHEETVIEW { get; set; }
        public List<TimeKeep_Entities> LST_TIMEKEEEPVIEW { get; set; }
    }
  
}