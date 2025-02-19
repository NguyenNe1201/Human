using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class LicenseKey_Entities
    {
        public int ID { get; set; }
        public string KEY_VALUE { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime EXPIRY_DATE { get; set; }
        public int IS_KEYPROVIDED { get; set; }
    }
}
