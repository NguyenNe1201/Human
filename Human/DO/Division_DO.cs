using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human.DO
{
    public class Division_DO
    {
        
      

        public IEnumerable<Division_Entities> GetAllDivision()
        {
            DbHelper db = new DbHelper();
            return db.Executereader<Division_Entities>("GetAllDivision", new string[] { "" }, new object[] { });
            
        }
    }
}
