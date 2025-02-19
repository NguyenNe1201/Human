using Human.DO;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models
{
    public class KindLeaveModel
    {
        public KindLeaveModel()
        {
            KIND_LEAVE = new KindLeave_Entities();
            T_Leave = new Leave_Entities();
            TOTAL_LEAVE = new Cal_Leave_Entities();
            EMPLOYEE = new EmployeeView_Entities();
        }
        public EmployeeView_Entities EMPLOYEE { get; set; }
        public List<KindLeave_Entities> LST_KINDLEAVE { get; set; }
        public List<Leave_Entities> LST_LEAVE { get;  set; }
        public KindLeave_Entities KIND_LEAVE { get; set; }
        public Leave_Entities T_Leave { get; set; }
        public Cal_Leave_Entities TOTAL_LEAVE { get; set; }
    }
}