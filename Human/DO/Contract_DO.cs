using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;

namespace Human.DO
{
    public class Contract_DO
    {
        public DbHelper db;
        public IEnumerable<ContractType_Entities> GetContractType()
        {
            db = new DbHelper();
            return db.Executereader<ContractType_Entities>("sp_SEL_ContractType", new string[] { "" }, new object[] {});
        }
        public IEnumerable<Contract_Entities> GetContractByEmpID(decimal emp_id)
        {
            db = new DbHelper();
            return db.Executereader<Contract_Entities>("GetContractByEmpID", new string[] { "EMPLOYEE_ID" }, new object[] {emp_id });
        }
        public int AddNewContract_Tab(string contract_no,decimal emp_id,int contract_type_id,string issuedate,string deadline,string remark,string baseon,string baseon_kyngay)
        {
            db = new DbHelper();
            int i = 0;
            i = db.execStoreProcedure("sp_INS_NewContract", new string[] { "CONTRACT_NO", "EMPLOYEE_ID", "CONTRACTTYPE_ID", "ISSUEDATE",
                "DEADLINE", "REMARK", "BASEON", "BASEON_KYNGAY", }
                    , new object[] { contract_no,emp_id,contract_type_id,issuedate,deadline,remark,baseon,baseon_kyngay });
            return i;

        }
        public int UpdateContract_Tab(int contract_id,string contract_no, decimal emp_id, int contract_type_id, string issuedate, string deadline, string remark, string baseon, string baseon_kyngay)
        {
            db = new DbHelper();
            int i = 0;
            i = db.execStoreProcedure("sp_UPD_NewContract", new string[] { "CONTRACT_ID","CONTRACT_NO", "EMPLOYEE_ID", "CONTRACTTYPE_ID", "ISSUEDATE",
                "DEADLINE", "REMARK", "BASEON", "BASEON_KYNGAY", }
                    , new object[] { contract_id,contract_no, emp_id, contract_type_id, issuedate, deadline, remark, baseon, baseon_kyngay });
            return i;

        }
    }
}