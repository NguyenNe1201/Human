using Human.Areas.Admin.Models;
using Human.Controllers;
using Human.Models;
using Human.DO;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Human.Areas.Admin.Controllers
{
    public class LeaveController : BaseController
    {
        LeaveModel model = new LeaveModel();
        LEAVE_DO leave_DO;
        Employee_DO _empDO;
        public KindLeave_DO kindLeave_DO;
        // GET: Admin/Leave
        public ActionResult Index()
        {
            init();

            return View(model);
        }
        public void init()
        {
            kindLeave_DO = new KindLeave_DO();
            leave_DO = new LEAVE_DO();
            _empDO = new Employee_DO();

            KindLeave_Entities tempK = new KindLeave_Entities();
            tempK.KINDLEAVE_ID = 0;
            tempK.NAMELEAVE_VI = "Tất cả";
            tempK.NAMELEAVE_EN = "ALL";
            model.LST_KINDLEAVE = kindLeave_DO.GetAllKindLeave().ToList();
            model.LST_KINDLEAVE.Add(tempK);

            model.POSITION = ClsGlobal_Entities.POS; 
        }
       

        public ActionResult Search([FromBody] AdminLeaveModel modelBody, string KINDLEAVE_ID)
        {
            init();
            string condition = "";
            if (modelBody.SEARCH_CODE != "" && modelBody.SEARCH_CODE != null)
            {
                condition += " AND EMP.EMP_CODE='" + modelBody.SEARCH_CODE + "'";
                Session["SEARCH_CODE"] = modelBody.SEARCH_CODE;

            }
            else
            {
                Session["SEARCH_CODE"] = null;
            }
            if (modelBody.SEARCH_STARTDATE <= modelBody.SEARCH_ENDDATE)
            {
                condition += " AND LEAVE_STARTDATE BETWEEN '" + modelBody.SEARCH_STARTDATE.ToString("yyyy-MM-dd") + "' AND '" + modelBody.SEARCH_ENDDATE.ToString("yyyy-MM-dd") + "'";
                Session["SEARCH_START"] = modelBody.SEARCH_STARTDATE;
                Session["SEARCH_END"] = modelBody.SEARCH_ENDDATE;
            }
            if(modelBody.SEARCH_STATUSLEAVE >= 0)
            {
                condition += " AND STATUS_L = " + modelBody.SEARCH_STATUSLEAVE.ToString();
                Session["SEARCH_STATUS"] = modelBody.SEARCH_STATUSLEAVE;
            }
            else
            {
                Session["SEARCH_STATUS"] = null;

            }
            if (KINDLEAVE_ID != "0")
            {
                condition += "AND LEAVE_TAB.KINDLEAVE_ID = " + KINDLEAVE_ID + "";
                Session["SEARCH_KL_ID"] = KINDLEAVE_ID;
            }
            else
            {
                Session["SEARCH_KL_ID"] = "0";
            }
            leave_DO = new LEAVE_DO();
            model.LST_LEAVEVIEW = leave_DO.SP_SEL_LEAVE_MODEL(condition).OrderBy(o => o.LEAVE_STARTDATE).ToList();
            return View("Index", model);


        }
       
        
       
       
        public void GetLeaveCurrent()
        {

            string condition = "";
            
            if (Session["SEARCH_CODE"] != null)
            {
                condition += " AND EMP.EMP_CODE='" + Session["SEARCH_CODE"] + "'";

            }

            condition += " AND LEAVE_STARTDATE BETWEEN '" + Convert.ToDateTime(Session["SEARCH_START"]).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(Session["SEARCH_END"]).ToString("yyyy-MM-dd") + "'";
            if (Session["SEARCH_STATUS"] != null)
            {
                condition += " AND STATUS_L= " + Session["SEARCH_STATUS"].ToString();
            }
            if (Session["SEARCH_KL_ID"].ToString() != "0")
            {
                condition += "AND LEAVE_TAB.KINDLEAVE_ID = " + Session["SEARCH_KL_ID"] + "";
            }
            leave_DO = new LEAVE_DO();
            model.LST_LEAVEVIEW = leave_DO.SP_SEL_LEAVE_MODEL(condition).OrderBy(o => o.LEAVE_STARTDATE).ToList();
        }

       

        // GET: Admin/Leave/Create
        public ActionResult Create()
        {
            init();
            return View(model);
        }

        // POST: Admin/Leave/Create


        // GET: Admin/Leave/Edit/5
        public ActionResult Edit(int LEAVE_ID)
        {
            init();
            leave_DO = new LEAVE_DO();
            model.T_Leave = leave_DO.GetLeaveById(LEAVE_ID).FirstOrDefault();
            return View(model);
        }
        public ActionResult InsertLeave([FromBody] Leave_Entities sp)
        {
            try
            {
                _empDO = new Employee_DO();
                leave_DO = new  LEAVE_DO();
                var emp = _empDO.GetEmployeeByCode(sp.EMP_CODE).FirstOrDefault();
                if (emp != null && sp.EMPLOYEE_ID == emp.EMPLOYEE_ID)
                {
                    string pos = ClsGlobal_Entities.POS;
                    sp.COM_YEAR_MONTH = sp.COM_YEAR + sp.COM_MONTH;
                    int i = leave_DO.INS_LEAVE_ADMIN(sp,pos);
                    if (i > 0)
                    {
                        SetAlert("Thêm mới phép thành công", "success");
                    }
                    else
                    {
                        SetAlert("Thêm mới Thất bại", "alert-error");
                        return RedirectToAction("Create");
                    }
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }


        public void UpdateStatusLeave(int LEAVE_ID, int STATUS)
        {
            if (Session["EMP_ID"] != null)
            {
                string pos = ClsGlobal_Entities.POS;
                int emp_id = Convert.ToInt32(Session["EMP_ID"]);
                leave_DO = new LEAVE_DO();
                leave_DO.UPD_LEAVE_STATUS(pos, LEAVE_ID, STATUS, emp_id);
            }
        }
        public bool UpdateKindLeave(int LEAVE_ID,int KINDLEAVE_ID,int EMPLOYEE_ID)
        {
            Leave_Entities l = new Leave_Entities();
            Cal_Leave_Entities cal_Leave = new Cal_Leave_Entities();
            l.LEAVE_ID = LEAVE_ID;
            l.KINDLEAVE_ID = KINDLEAVE_ID;
            leave_DO = new LEAVE_DO();
            if (KINDLEAVE_ID == 1)
            {
                cal_Leave = leave_DO.SEL_CAL_LEAVE("AND EMPLOYEE_ID=" + EMPLOYEE_ID.ToString()).FirstOrDefault();
                if (cal_Leave != null && cal_Leave.REMAIN <= 0)
                {
                    return false;
                }
              
            }
                leave_DO.UPD_LEAVE_KINDLEAVE(l);
                return true;
           

        }
        public ActionResult ReloadTotalLeave()
        {
            leave_DO = new LEAVE_DO();
            string year_now = DateTime.Now.Year.ToString();
            int i = leave_DO.SP_TOTAL_LEAVE("%", "%", "%", year_now);
            Session.Clear();
            return RedirectToAction("Index");
        }
        public JsonResult LoadLeaveUpdate()
        {
            init();
            GetLeaveCurrent();
            return Json(model,JsonRequestBehavior.AllowGet);
        }

    }
}
