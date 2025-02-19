using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human.DO
{
    public class Department_DO
    {
        public IEnumerable<Department_Entities> GetAllDepartment()
        {
            DbHelper db = new DbHelper();
            return db.Executereader<Department_Entities>("GetAllDepartment", new string[] { "" }, new object[] { });
            
        }
    }
}
