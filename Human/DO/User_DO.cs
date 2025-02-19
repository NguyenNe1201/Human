using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using Human.Models.Entities;

namespace Human.DO
{

    public class User_DO
    {
        //   SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EMART;User ID=sa;Password=123");
        DbHelper db;
        public IEnumerable<User_Entities> GetUser(User_Entities u)
        {
            db = new DbHelper();
            return db.Executereader<User_Entities>("CheckUser", new string[] { "USER_NAME" }, new object[] { u.USERNAME });


        }
        public IEnumerable<User_Entities> GetListUser()
        {
            db = new DbHelper();
            return db.Executereader<User_Entities>("GetAllUser", new string[] { "" }, new object[] { });

        }
      
        public IEnumerable<Emp_Power_Entities> CheckFunctionEmp(string employee_code, int role_id)
        {
            db = new DbHelper();
            return db.Executereader<Emp_Power_Entities>("sp_CHECK_FUNTION_EMP", new string[] { "EMP_CODE", "ROLE_ID" }, new object[] { employee_code,role_id});
        }

        public IEnumerable<ProfileEmployee_Entities> GetAllEmpCondition(string condition)
        {
            db = new DbHelper();
            return db.Executereader<ProfileEmployee_Entities>("sp_SEL_EMP_CONDITION", new string[] { "CONDITION" }, new object[] { condition });
        }
        
    }
}
