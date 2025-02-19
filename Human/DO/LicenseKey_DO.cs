using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.DO
{
    public class LicenseKey_DO
    {
        public DbHelper db;
        public IEnumerable<LicenseKey_Entities> Get_LicensKey()
        {
            db = new DbHelper();
            return db.Executereader<LicenseKey_Entities>("GetLicense_Key", new string[] { "" }, new object[] { });
        }
        public int Update_LicenseKey(string key,string end_date)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("UPD_LisenseKey", new string[] { "KEY_VALUE", "END_DATE" }, new object[] { key, end_date });
                return i;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}