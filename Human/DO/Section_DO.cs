using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Human.DO
{
    public class Section_DO
    {
        public DbHelper db;
        public IEnumerable<Section_Entities> GetAllSection()
        {
            DbHelper db = new DbHelper();
            return db.Executereader<Section_Entities>("GetAllSection", new string[] { "" }, new object[] { });

        }
        public IEnumerable<Section_Entities> GetSectionByID(int section_id)
        {
            DbHelper db = new DbHelper();
            return db.Executereader<Section_Entities>("GetSectionByID", new string[] { "SECTION_ID" }, new object[] { section_id });
        }
        public int AddNewSection(string SectionName_vi, string SectionName_en, int department_id)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("AddNewSection", new string[] { "SECTION_NAME_VI", "SECTION_NAME_EN", "DEPARTMENT_ID" }, new object[] { SectionName_vi, SectionName_en, department_id });

            return i;
        }
        public int UpdateSection(int Section_id, string sectionName_vi, string sectionName_en, int department_id)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("UpdateSection", new string[] { "SECTION_ID", "SECTION_NAME_VI", "SECTION_NAME_EN", "DEPARTMENT_ID" }, new object[] { Section_id, sectionName_vi, sectionName_en, department_id });

            return i;
        }
        public int DeleteSection(int Section_id)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("DeleteSection", new string[] { "SECTION_ID" }, new object[] { Section_id});

            return i;
        }
    }
}