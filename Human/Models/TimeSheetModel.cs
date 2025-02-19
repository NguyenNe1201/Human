using Human.Controllers.PublicController;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Human.Models
{
    public class TimeSheetModel
    {
        public List<TimeSheetDay_Entities> LST_TIMEKEEPDAY { get; set; }
        public List<Section_Entities> LST_SECTION { get; set; }
        public List<Department_Entities> LST_DEPARTMENT { get; set; }
        public List<Job_title_Entities> LST_JOB { get; set; }
        public List<TimeSheet_Entities> LIST_TIMESHEETVIEW { get; set; }
    }
}