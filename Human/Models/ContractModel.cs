using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.Models
{
    public class ContractModel
    {
        public List<ContractType_Entities> LST_CONTRACT_TYPE { get; set; }
        public List<EmployeeTab_Entities> LST_ALL_EMPLOYEE { get; set; }
    }
}