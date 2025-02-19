using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio.TwiML.Voice;
using System.Resources;
using System.ComponentModel.DataAnnotations;

namespace Human.Models
{
    public class HomeModel
    {
      /*  public HomeModel()
        {
            KIND_LEAVE = new KindLeave_Entities();
            TOTAL_LEAVE = new Cal_Leave_Entities();
            EMP_VIEW = new EmployeeView_Entities();
            T_Leave = new Leave_Entities();
            LST_LEAVEVIEW = new List<LeaveView_Entities>();
        }*/
        public Leave_Entities T_Leave { get; set; }
        public KindLeave_Entities KIND_LEAVE { get; set; }
        public EmployeeView_Entities EMP_VIEW { get; set; }
        public List<KindLeave_Entities> LST_KINDLEAVE { get; set; }
        public List<Leave_Entities> LST_LEAVE { get; set; }
        public Cal_Leave_Entities TOTAL_LEAVE { get; set; }
        public List<LeaveView_Entities> LST_LEAVEVIEW { get; set; }
        public LeaveView_Entities COUNT_LEAVE {get;set;}
      //  public List<PowerView_Entities>LST_POWER_BYEMP { get; set; }
        public List<Emp_Function_Entities> LST_FUNCTION_EMP { get; set; }
     }
}