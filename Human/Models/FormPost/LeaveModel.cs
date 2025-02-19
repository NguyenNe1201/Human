using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models
{
    public class LeaveModel
    {
        public LeaveModel()
        {
            KIND_LEAVE = new KindLeave_Entities();
            T_Leave = new Leave_Entities();
            LST_LEAVEVIEW = new List<LeaveView_Entities>();
        }
        public List<LeaveView_Entities> LST_LEAVEVIEW { get; set; }
        public List<KindLeave_Entities> LST_KINDLEAVE { get; set; }
        public KindLeave_Entities KIND_LEAVE { get; set; }
        public Leave_Entities T_Leave { get; set; }
        public Section_Entities SECTION_TAB { get; set; }
        public string SEARCH_CODE { get; set; }
        public DateTime SEARCH_STARTDATE { get; set; }
        public DateTime SEARCH_ENDDATE { get; set; }
        public int SEARCH_STATUSLEAVE { get; set; }
        public string EMP_NAME { get; set; }

        public string POSITION { get; set; }
    }
  

}