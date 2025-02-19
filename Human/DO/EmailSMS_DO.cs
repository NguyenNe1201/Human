using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.DO
{
    public class EmailSMS_DO
    {
        DbHelper db;
        public IEnumerable<EmailSMS_Entities> UPD_EmailSMS(string email, string password)
        {

            db = new DbHelper();
            return db.Executereader<EmailSMS_Entities>("UPD_EmailSMS", new string[] { "EMAIL", "PASSWORD" }, new object[] {email,password });

        }
        public IEnumerable<EmailSMS_Entities> GetEmailSMS()
        {

            db = new DbHelper();
            return db.Executereader<EmailSMS_Entities>("GetEmailSMS", new string[] { "" }, new object[] {});

        }
    }
}