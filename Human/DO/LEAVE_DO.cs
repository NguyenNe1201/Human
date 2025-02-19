using Human.Models.Entities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human.DO
{
    public class LEAVE_DO
    {
        public DbHelper db;
        DataTable datatable;
        public IEnumerable<Leave_Entities> GetLeaveByEmployeeId(decimal employeeId)
        {
            db = new DbHelper();
            return db.Executereader<Leave_Entities>("sp_SEL_LeaveByEmployeeID", new string[] { "EMPLOYEE_ID" }, new object[] { employeeId });

        }
        public IEnumerable<Leave_Entities> GetAllLeave(string Condition)
        {
            db = new DbHelper();
            return db.Executereader<Leave_Entities>("sp_SEL_Leave", new string[] { "CONDITION" }, new object[] { Condition });
        }
        public IEnumerable<Leave_Entities> SEL_LEAVE(string condition)
        {
            db = new DbHelper();
            return db.Executereader<Leave_Entities>("sp_SEL_LEAVE", new string[] { "@CONDITION" }, new object[] { condition });
        }
        public IEnumerable<Leave_Entities> GetLeaveById(int LeaveId)
        {
            db = new DbHelper();
            return db.Executereader<Leave_Entities>("sp_SEL_LeaveByID", new string[] { "LEAVE_ID" }, new object[] { LeaveId });
        }
        public int INS_Leave(Leave_Entities leave)
        {
            int i = 0;
            db = new DbHelper();

            i = db.execStoreProcedure("sp_INS_Leave", new string[] { "EMPLOYEE_ID", "KINDLEAVE_ID", "HOURS", "LEAVE_STARTDATE", "LEAVE_ENDDATE", 
                "REASON", "ANNUAL_LEAVE_USED", "COM_MONTH", "COM_YEAR", "COM_YEAR_MONTH" },new object[] { leave.EMPLOYEE_ID, leave.KINDLEAVE_ID, leave.HOURS, leave.LEAVE_STARTDATE, leave.LEAVE_ENDDATE, leave.REASON,
                    leave.ANNUAL_LEAVE_USED, leave.COM_MONTH, leave.COM_YEAR, leave.COM_YEAR_MONTH });
            return i;
        }
        
        public int INS_CAL_LEAVE()
        {
            int i = 0;
            db = new DbHelper();
            i = db.execStoreProcedure("INS_CAL_LEAVE", new string[] { }, new object[] { });
            return i;
        }
        public int INS_LEAVE_ADMIN(Leave_Entities leave,string pos)
        {
            int i = 0;
            db = new DbHelper();
            i = db.execStoreProcedure("sp_INS_Leave_ADMIN", new string[] { "EMPLOYEE_ID", "KINDLEAVE_ID", "HOURS", "LEAVE_STARTDATE", "LEAVE_ENDDATE", "REASON", "ANNUAL_LEAVE_USED", "COM_MONTH", 
                "COM_YEAR", "COM_YEAR_MONTH","POSITION" },new object[] { leave.EMPLOYEE_ID, leave.KINDLEAVE_ID, leave.HOURS, leave.LEAVE_STARTDATE, 
                    leave.LEAVE_ENDDATE, leave.REASON, leave.ANNUAL_LEAVE_USED, leave.COM_MONTH, leave.COM_YEAR, leave.COM_YEAR_MONTH,pos });
            return i;
        }
        public float SEL_ANUAL_LEAVE_REMAIN(decimal EMP_ID)
        {
            float i = 0;
            db = new DbHelper();
            datatable = new DataTable();
            datatable = db.return_DataTable("SEL_CURRENT_ANUAL_LEAVE", new string[] { "@EMPLOYEE_ID" }, new object[] { EMP_ID });
            if (datatable.Rows.Count > 0)
            {
                i = float.Parse(datatable.Rows[0][2].ToString());
            }
            return i;
        }
        public IEnumerable<LeaveView_Entities> SP_SEL_LEAVE_MODEL(string condition)
        {
            db = new DbHelper();
           return db.Executereader<LeaveView_Entities>("sp_SEL_Leave", new string[] { "@CONDITION" }, new object[] { condition });
        }
        public IEnumerable<Cal_Leave_Entities> SEL_CAL_LEAVE(string condition)
        {
            db = new DbHelper();
            return db.Executereader<Cal_Leave_Entities>("SEL_CAL_LEAVE_TAB", new string[] { "@CONDITION" }, new object[] { condition });
        }
       
        public int UpdateStatus(int LEAVE_ID)
        {
            int i = 0;
            try
            {
                 db = new DbHelper();
                i = db.execStoreProcedure("UPD_LeaveStatus", new string[] { "LEAVE_ID" }, new object[] { LEAVE_ID });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
        public int SP_TOTAL_LEAVE(string departmentId,string divisionId,string sectionId,string year)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("sp_Total_Leave_ADMIN", new string[] { "Department_ID", "Division_ID", "Section_ID", "YEAR" }, new object[] { departmentId, divisionId, sectionId, year });
                return i;
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        public int Delete(int LEAVE_ID)
        {
            int i = 0;
            try
            {
                 db = new DbHelper();
                i = db.execStoreProcedure("sp_DEL_Leave", new string[] { "LEAVE_ID" }, new object[] { LEAVE_ID });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
        public int Cancel(int LEAVE_ID)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("sp_Cancel_Leave", new string[] { "LEAVE_ID" }, new object[] { LEAVE_ID });
                return i;
            }
            catch(Exception e)
            {
                return i;
            }
        }
        public int Request(int LEAVE_ID)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("sp_Request_Leave", new string[] { "LEAVE_ID" }, new object[] { LEAVE_ID });
                return i;
            }
            catch (Exception e)
            {
                return i;
            }
        }
        public int UPD_LEAVE(Leave_Entities l)
        {
            int i = 0;
            try
            {
                db = new DbHelper();
                i = db.execStoreProcedure("sp_UPD_LEAVE", new string[] { "EMPLOYEE_ID", "LEAVE_ID", "KINDLEAVE_ID", "HOURS","LEAVE_STARTDATE", "REASON" }, new object[] { l.EMPLOYEE_ID,l.LEAVE_ID, l.KINDLEAVE_ID, l.HOURS,l.LEAVE_STARTDATE, l.REASON });
                return i;
            }catch(Exception e)
            {
                return 0;
            }
        }
        //ADMIN
        public int UPD_LEAVE_STATUS(string POSITION,int LEAVE_ID,int STATUS,int EMP_ID_APP)
        {
            int i = 0;
            try
            {
                 db = new DbHelper();
                i = db.execStoreProcedure("UPD_Leave_Status", new string[] { "POSITION", "LEAVE_ID", "STATUS", "EMP_ID_APP" }, new object[] { POSITION, LEAVE_ID, STATUS, EMP_ID_APP });
                return i;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        
        public int UPD_LEAVE_KINDLEAVE(Leave_Entities l)
        {
            int i = 0;
            try
            {
                 db = new DbHelper();
                i = db.execStoreProcedure("UPD_LEAVE_KINDLEAVE", new string[] { "KINDLEAVE_ID", "LEAVE_ID" }, new object[] { l.KINDLEAVE_ID, l.LEAVE_ID });
                return i;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
    }
}
