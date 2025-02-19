using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models
{
    public class SetupModel
    {
        public List<Department_Entities> List_DEPARTMENT { get; set; }
        public List<Section_Entities> LST_SECTION { get; set; }
        public List<Division_Entities> LST_DIVISION { get; set; }
        public List<Job_title_Entities> LST_JOBTITLE { get; set; }
        public List<Salary_Level_Entities> LST_SALARY_LEVEL { get; set; }
    }
}