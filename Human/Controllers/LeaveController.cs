using Human.Models;

using Human.DO;
using Human.Models.Entities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Human.Common;

namespace Human.Controllers
{
    public class LeaveController : BaseController
    {
        // GET: Leave
        KindLeaveModel model = new KindLeaveModel();
        LEAVE_DO leave_DO;
        Employee_DO _empDO;
        public KindLeave_DO kindLeave_DO;
        public ActionResult Index()
        {

            init();
          
            return View(model);
        }
        public void init()
        {
            try
            {
                kindLeave_DO = new KindLeave_DO();
                leave_DO = new LEAVE_DO();
                _empDO = new Employee_DO();


                string year = DateTime.Now.ToString("yyyy");
                int i = leave_DO.SP_TOTAL_LEAVE("%", "%", "%", year);

                model.LST_KINDLEAVE = kindLeave_DO.GetAllKindLeave().ToList();
                var emp_code = Session["EMP_CODE"];
                string con_str = "";
                if (emp_code != null)
                {
                    con_str += " AND EMP_CODE='" + emp_code + "'";
                    var emp = _empDO.GetInfoEmployee(con_str).FirstOrDefault();
                    if (emp != null)
                    {

                        var emp_id = emp.EMPLOYEE_ID;
                        string condition = "AND EMPLOYEE_ID =" + emp_id;

                        ViewBag.EmployeeID = emp_id;
                        model.EMPLOYEE = emp;
                        model.LST_LEAVE = leave_DO.GetLeaveByEmployeeId(emp_id).Where(w => w.LEAVE_STARTDATE.Year == DateTime.Now.Year).OrderByDescending(o => o.LEAVE_STARTDATE).ToList();
                        model.TOTAL_LEAVE = leave_DO.SEL_CAL_LEAVE(condition).FirstOrDefault();
                        
                    }

                }
                else
                {

                }




            }
            catch (Exception ex)
            {

            }
        }

        public ActionResult Add()
        {

            init();
            return View(model);
        }
        public ActionResult Edit(int LEAVE_ID)
        {

            init();
            model.T_Leave = model.LST_LEAVE.Where(w => w.LEAVE_ID == LEAVE_ID).FirstOrDefault();

            return View(model);

        }
        [ValidateAntiForgeryToken]
        public ActionResult InsertLeave([FromBody] Leave_Entities sp)
        {
            try
            {
                if (sp.CHECKED_TODATE == false)
                {
                    sp.LEAVE_ENDDATE = sp.LEAVE_STARTDATE;
                }

                _empDO = new Employee_DO();
                leave_DO = new LEAVE_DO();
                string con_email = " AND EMP_CODE ='" + Session["EMP_CODE"].ToString() + "'";
                var emp = _empDO.GetEmployeeByCondition(con_email).FirstOrDefault();
                if (emp != null && sp.EMPLOYEE_ID == emp.EMPLOYEE_ID)
                {

                    sp.COM_YEAR_MONTH = sp.COM_YEAR + sp.COM_MONTH;
                    float PNCL = leave_DO.SEL_ANUAL_LEAVE_REMAIN(sp.EMPLOYEE_ID);
                    if (sp.KINDLEAVE_ID == 1 && TotalLeaveInsert(sp.LEAVE_STARTDATE, sp.LEAVE_ENDDATE, (float)sp.HOURS, PNCL) < 0)
                    {
                        CommonCheck.SetAlert(this,"Phép năm của bạn không đủ. Lưu không thành công!", "warning");


                    }
                    else if (Max3_DateLeave(sp.LEAVE_STARTDATE, sp.LEAVE_ENDDATE))
                    {
                        CommonCheck.SetAlert(this,"Tối đa là 3 ngày phép. Lưu không thành công!", "warning");
                    }
                    else
                    {
                        string condition = " AND LEAVE_TAB.LEAVE_STARTDATE between'" + sp.LEAVE_STARTDATE.ToString("yyyy-MM-dd") + "' and '" + sp.LEAVE_ENDDATE.ToString("yyyy-MM-dd") + "'";

                        if (sp.EMPLOYEE_ID > 0)
                            condition += " AND LEAVE_TAB.EMPLOYEE_ID = " + sp.EMPLOYEE_ID;
                        if (sp.LEAVE_ID > 0)
                            condition += " AND LEAVE_ID =" + sp.LEAVE_ID;

                        List<Leave_Entities> leave_CHECK = leave_DO.SEL_LEAVE(condition).ToList();
                        foreach (Leave_Entities leave in leave_CHECK)
                        {
                            if (leave.STATUS_L != 0)
                            {
                                CommonCheck.SetAlert(this,Human.Resource.Language.This_leave_date_has_been_confirmed, "error");
                                
                                return RedirectToAction("Index");
                            }
                        }

                        int i = 0;
                        if (sp.LEAVE_ID > 0)
                        {
                            i = leave_DO.UPD_LEAVE(sp);
                            if (i > 0)
                            {
                                CommonCheck.SetAlert(this,Human.Resource.Language.Update_leave_successful, "success");
                            }
                            else
                            {
                                CommonCheck.SetAlert(this,Human.Resource.Language.Update_leave_failed, "error");

                            }
                        }
                        else
                        {
                            i = leave_DO.INS_Leave(sp);
                            if (i > 0)
                            {
                                CommonCheck.SetAlert(this,Human.Resource.Language.Add_new_leave_successful, "success");
                            }
                            else
                            {
                                CommonCheck.SetAlert(this,Human.Resource.Language.Add_new_leave_failed, "error");

                            }
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                CommonCheck.SetAlert(this,"Error!!!!", "error");
            }
            finally
            {

            }


            return RedirectToAction("Index");
        }

        public float TotalLeaveInsert(DateTime StartDate, DateTime EndDate, float Hours, float PNCL)
        {
            float total_Phep;
            int total_day = 0;
            for (DateTime dt_temp = StartDate; dt_temp <= EndDate; dt_temp = dt_temp.AddDays(1))
            {
                if (dt_temp.DayOfWeek != DayOfWeek.Sunday)
                {
                    total_day++;
                }
            }
            total_Phep = (total_day * (Hours / 8));
            return PNCL - total_Phep;
        }
        public bool Max3_DateLeave(DateTime start, DateTime end)
        {
            //tính cả ngày bắt đầu nên tổng ngày chỉ là 2. vd: 1>3 -> 2
            TimeSpan timediff = end - start;
            if (timediff.TotalDays > 2)
                return true;
            else
                return false;
        }
        public ActionResult Delete(string LEAVE_ID)
        {
            leave_DO = new LEAVE_DO();
            int i = leave_DO.Delete(Convert.ToInt32(LEAVE_ID));
            if (i > 0)
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.Delete_successfully, "success");
            }
            else
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.Delete_failed, "error");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Request(string LEAVE_ID)
        {
            leave_DO = new LEAVE_DO();
            int i = leave_DO.Request(Convert.ToInt32(LEAVE_ID));
            if (i > 0)
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.Request_sent_successfully, "success");
            }
            else
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.Request_sent_failed, "error");
            }
            return RedirectToAction("Index");
        }
      
        public ActionResult Cancel(string LEAVE_ID)
        {
            leave_DO = new LEAVE_DO();
            int i = leave_DO.Cancel(Convert.ToInt32(LEAVE_ID));
            if (i > 0)
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Cancel_leave_successfully, "success");

            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Cancel_leave_failed, "error");

            }
            return RedirectToAction("Index");
        }

        public int DeleteById(string LEAVE_ID)
        {
            leave_DO = new LEAVE_DO();
            int i = leave_DO.Delete(Convert.ToInt32(LEAVE_ID));
            if (i > 0)
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Delete_successfully, "success");

            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Delete_failed, "error");

            }
            return i;
        }
        public JsonResult PNCL(string EMPLOYEE_ID)
        {
            leave_DO = new LEAVE_DO();
            float pncl = leave_DO.SEL_ANUAL_LEAVE_REMAIN(Convert.ToDecimal(EMPLOYEE_ID));
            return Json(pncl, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListLeaveByMonth(int month)
        {
            init();
            if (month != 0)
            {
                model.LST_LEAVE = model.LST_LEAVE.Where(w => w.LEAVE_STARTDATE.Month == month && w.STATUS_L==1 && w.KINDLEAVE_ID==1).OrderByDescending(o => o.LEAVE_STARTDATE).ToList();

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadLeave()
        {
            init();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckleaveDate(DateTime date_create, decimal employee_id)
        {
            List<Leave_Entities> l = new List<Leave_Entities>();
            leave_DO = new LEAVE_DO();
            l = leave_DO.GetLeaveByEmployeeId(employee_id).ToList();
            bool isDateExit = l.Any(w => w.LEAVE_STARTDATE == date_create);
            return Json(isDateExit);
        }
    }
}