using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            LST_EMP = new List<EmployeeView_Entities>();
        }
        public List<EmployeeView_Entities> LST_EMP {  get; set; }
        public EmployeeView_Entities EMP_VIEW { get; set; }
    }
}