using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models
{
    public class CommonModel
    {
      public List<Division_Entities> LSTDIVISION { get; set; }
        public List<Department_Entities> LSTDEPART { get; set; }
        public int SEARCH_DIVID { get; set; }
        public int SEARCH_DEPID { get; set; }
    }
}