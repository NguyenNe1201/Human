using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.DO
{
    public class JobTitle_DO
    {
        public DbHelper db;
        public IEnumerable<Job_title_Entities> GetAllJobTitle()
        {
            DbHelper db = new DbHelper();
            return db.Executereader<Job_title_Entities>("GetAllJobTitle", new string[] { "" }, new object[] { });

        }
        public IEnumerable<Job_title_Entities> GetJobTitleByID(int id)
        {
            DbHelper db = new DbHelper();
            return db.Executereader<Job_title_Entities>("GetJobTitleByID", new string[] { "JobTitle_ID" }, new object[] {  id });

        }
        public int AddJobTitle(string TitleName_vi, string TitleName_en)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("AddNewJobtitle", new string[] { "TitleName_vi", "TitleName_en" }, new object[] { TitleName_vi, TitleName_en });

            return i;
        }
        public int UpdateJobTitle(int JobTitle_ID, string TitleName_vi, string TitleName_en)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("UpdateJobtitle", new string[] { "JobTitle_ID", "TitleName_vi", "TitleName_en" }, new object[] { JobTitle_ID, TitleName_vi, TitleName_en });

            return i;
        }
        public int DeleteJob_Title(int Job_id)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("DeleteJobtitle", new string[] { "JobTitle_ID" }, new object[] { Job_id });

            return i;
        }
    }
}