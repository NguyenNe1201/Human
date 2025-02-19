using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class Job_title_Entities
    {
        public int JobTitle_ID { get; set; }
        public string TitleName_en { get; set; }
        public string TitleName_vi { get; set; }
        public bool? SUP_MANAGER { get; set; }
        public double SORT_JOB { get; set; }
    }
}