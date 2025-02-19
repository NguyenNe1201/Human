using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human.DO
{
    public class KindLeave_DO 
    {

        public IEnumerable<KindLeave_Entities> GetAllKindLeave()
        {
            DbHelper db = new DbHelper();
            return db.Executereader<KindLeave_Entities>("sp_SEL_KindLeave", new string[] { "" }, new object[] { });
        
        }
    }
}
