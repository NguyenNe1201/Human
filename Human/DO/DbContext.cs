using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human.DO
{
    public class DbContext
    {
        public SqlConnection _SqlConnect;
        public SqlCommand _SqlCommand;
        public DbContext()
        {

        }
        public void Dispose()
        {

        }
    }
}
