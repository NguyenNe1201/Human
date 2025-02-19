using Human.Models.FormPost;
using Human.Models;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Drawing.Printing;
using Microsoft.Ajax.Utilities;
using Human.DO;
using Human.Models.Entities;
using Human.Resource;
using static System.Collections.Specialized.BitVector32;
using Human.Common;

namespace Human.Controllers
{
    public class LeaveAdminController : BaseController
    {
        // GET: LeaveAdmin
        AdminLeaveModel model = new AdminLeaveModel();
        LEAVE_DO leave_DO;
        Employee_DO _empDO;
        public KindLeave_DO kindLeave_DO;
        public Section_DO section_DO;
        static int pagesize = 10;
        static int TotalPage = 1;
        // GET: Admin/Leave
        public ActionResult Index()
        {
            var lst_fuction_emp = Session["FUNCTION_BY_EMP"] as List<Human.Models.Entities.Emp_Function_Entities>;
            if (CommonCheck.Check_function_read(lst_fuction_emp, "leave"))
            {
                init();
                return View(model);
            }
            else
            {
                CommonCheck.SetAlert(this,Human.Resource.Language.You_not_have_this_right, "error");

                return RedirectToAction("Index","Home");
            }
        }
        public void init()
        {
            kindLeave_DO = new KindLeave_DO();
            leave_DO = new LEAVE_DO();
            _empDO = new Employee_DO();
            section_DO = new Section_DO();
            Section_Entities tempS = new Section_Entities();
            KindLeave_Entities tempK = new KindLeave_Entities();
            tempK.KINDLEAVE_ID = 0;
            /*     tempK.NAMELEAVE_VI = "Tất cả";*/
            /*tempK.NAMELEAVE_EN = "All leave";*/
            tempK.NAMELEAVE_EN = Resource.Language.All_leave;
            tempS.SECTION_ID = 0;
            /*     tempS.SECTION_NAME_VI = "Tất cả";*/
            /*tempS.SECTION_NAME_EN = "All section";*/
            tempS.SECTION_NAME_EN = Resource.Language.All_section;
            model.LST_SECTION = section_DO.GetAllSection().OrderBy(s => s.SECTION_ID).ToList();
            model.LST_SECTION.Insert(0, tempS);
            model.LST_KINDLEAVE = kindLeave_DO.GetAllKindLeave().ToList();
            model.LST_KINDLEAVE.Insert(0, tempK);
            model.POSITION = Session["EMP_POS"].ToString();
        }
        public ActionResult Search([FromBody] LeaveModel modelBody , string SEARCH_SECTION, string KINDLEAVE_ID, int? page)
        {
            if (page == null) page = 1;
            int pagenumber = (page ?? 1);
            init();
            string condition = "";
            if (modelBody.SEARCH_CODE != "" && modelBody.SEARCH_CODE != null)
            {
                condition += " AND (EMP.EMP_CODE='" + modelBody.SEARCH_CODE + "' OR EMP.FULLNAME LIKE '%" + modelBody.SEARCH_CODE + "%')";
                Session["SEARCH_CODE"] = modelBody.SEARCH_CODE;
            }
            else
            {
                Session["SEARCH_CODE"] = null;
            }
            if (modelBody.SEARCH_STARTDATE <= modelBody.SEARCH_ENDDATE && modelBody.SEARCH_STARTDATE.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                condition += " AND LEAVE_STARTDATE >= '" + modelBody.SEARCH_STARTDATE.ToString("yyyy-MM-dd") + "' AND LEAVE_STARTDATE <= '" + modelBody.SEARCH_ENDDATE.ToString("yyyy-MM-dd") + "'";
                Session["SEARCH_START"] = modelBody.SEARCH_STARTDATE;
                Session["SEARCH_END"] = modelBody.SEARCH_ENDDATE;
            }
            else
            {
                var start_date = Convert.ToDateTime(Session["SEARCH_START"]).ToString("yyyy-MM-dd");
                var end_date = Convert.ToDateTime(Session["SEARCH_END"]).ToString("yyyy-MM-dd");
                condition += " AND LEAVE_STARTDATE >= '" + start_date + "' AND LEAVE_STARTDATE <= '" + end_date + "'";
            }
            if (modelBody.SEARCH_STATUSLEAVE >0)
            {
                condition += " AND STATUS_L = " + (modelBody.SEARCH_STATUSLEAVE-1).ToString();
                Session["SEARCH_STATUS"] = modelBody.SEARCH_STATUSLEAVE;
            }
            else
            {
                Session["SEARCH_STATUS"] = null;
            }
            if (SEARCH_SECTION != "0" && SEARCH_SECTION != null)
            {
                condition += " AND EMP.SECTION_ID = " + SEARCH_SECTION + "";
                Session["SEARCH_SECTION_ID"] = SEARCH_SECTION;
            }
            else
            {
                Session["SEARCH_SECTION_ID"] = SEARCH_SECTION;
            }
            if (KINDLEAVE_ID != "0"&& KINDLEAVE_ID!=null)
            {
                condition += "AND LEAVE_TAB.KINDLEAVE_ID = " + KINDLEAVE_ID + "";
                Session["SEARCH_KL_ID"] = KINDLEAVE_ID;
            }
            else
            {
                Session["SEARCH_KL_ID"] = "0";
            }
            leave_DO = new LEAVE_DO();
            model.LST_LEAVEVIEW = leave_DO.SP_SEL_LEAVE_MODEL(condition).ToList();
            ViewData["COUNT_LST_LEAVEVIEW"] = model.LST_LEAVEVIEW.Count.ToString();
            int totalRow = model.LST_LEAVEVIEW.Count;
            model.LST_LEAVEVIEW = model.LST_LEAVEVIEW.ToPagedList(pagenumber, pagesize).ToList();
            TotalPage = (int)Math.Ceiling((decimal)(totalRow / pagesize));
            if (totalRow > pagesize)
            {
                if ((totalRow % pagesize) > 0)
                {
                    TotalPage += 1;
                }
            }
            else
            {
                TotalPage = 1;
            }
            model.PAGE_ACTIVE = 1;
            ViewBag.TotalPage = TotalPage;

            return View("Index", model);
        }

        public ActionResult PagingSite(string sort, int page, int size)
        {
            init();
            string condition = "";
            if (Session["SEARCH_CODE"] != null)
            {
                condition += " AND (EMP.EMP_CODE='" + Session["SEARCH_CODE"] + "' OR EMP.FULLNAME LIKE '%" + Session["SEARCH_CODE"] + "%')";
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
            if (Session["SEARCH_SECTION_ID"].ToString() != "0")
            {
                condition += "AND EMP.SECTION_ID = " + Session["SEARCH_SECTION_ID"] + "";
            }
            leave_DO = new LEAVE_DO();
            switch (sort)
            {
                case "code":
                    model.LST_LEAVEVIEW = leave_DO.SP_SEL_LEAVE_MODEL(condition).OrderBy(o => o.EMP_CODE).ToList();
                    break;
                case "name":
                    model.LST_LEAVEVIEW = leave_DO.SP_SEL_LEAVE_MODEL(condition).OrderBy(o => o.FULLNAME).ToList();
                    break;
                default:
                    model.LST_LEAVEVIEW = leave_DO.SP_SEL_LEAVE_MODEL(condition).OrderBy(o => o.LEAVE_STARTDATE).ToList();
                    break;
            }
            int totalRow = model.LST_LEAVEVIEW.Count;
            TotalPage = (int)Math.Ceiling((decimal)(totalRow / size));
            if (totalRow > size)
            {
                if ((totalRow % size) > 0)
                {
                    TotalPage += 1;
                }
            }
            else
            {
                TotalPage = 1;
            }
            if (page > TotalPage)
            {
                model.LST_LEAVEVIEW = model.LST_LEAVEVIEW.ToPagedList(TotalPage, size).ToList();
            }
            else
            {
                model.LST_LEAVEVIEW = model.LST_LEAVEVIEW.ToPagedList(page, size).ToList();
            }
            return PartialView("_PartialTbodyLeave", model);

        }
        public JsonResult GetTotalPage()
        {
            return Json(TotalPage, JsonRequestBehavior.AllowGet);
        }

        //public void GetLeaveCurrent()
        //{

        //    string condition = "";

        //    if (Session["SEARCH_CODE"] != null)
        //    {
        //        condition += " AND (EMP.EMP_CODE='" + Session["SEARCH_CODE"] + "' OR EMP.FULLNAME LIKE '%" + Session["SEARCH_CODE"] + "%')";


        //    }

        //    condition += " AND LEAVE_STARTDATE BETWEEN '" + Convert.ToDateTime(Session["SEARCH_START"]).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(Session["SEARCH_END"]).ToString("yyyy-MM-dd") + "'";
        //    if (Session["SEARCH_STATUS"] != null)
        //    {
        //        condition += " AND STATUS_L= " + Session["SEARCH_STATUS"].ToString();
        //    }
        //    if (Session["SEARCH_KL_ID"].ToString() != "0")
        //    {
        //        condition += "AND LEAVE_TAB.KINDLEAVE_ID = " + Session["SEARCH_KL_ID"] + "";
        //    }
        //    leave_bo = new Leave_BO();
        //    model.LST_LEAVEVIEW = leave_bo.SP_SEL_LEAVE_MODEL(condition).OrderBy(o => o.LEAVE_STARTDATE).ToList();
        //}



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
                leave_DO = new LEAVE_DO();
                var emp = _empDO.GetEmployeeByCode(sp.EMP_CODE).FirstOrDefault();
                if (emp != null && sp.EMPLOYEE_ID == emp.EMPLOYEE_ID)
                {
                    string pos = Session["EMP_POS"].ToString();
                    sp.COM_YEAR_MONTH = sp.COM_YEAR + sp.COM_MONTH;
                    int i = leave_DO.INS_LEAVE_ADMIN(sp, pos);
                    if (i > 0)
                    {
                       // SetAlert("Thêm mới phép thành công", "success");
                        CommonCheck.SetAlert(this,Human.Resource.Language.Add_new_leave_successful,"success");
                    }
                    else
                    {
                        CommonCheck.SetAlert(this, Human.Resource.Language.Add_new_leave_failed, "error");
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
                int emp_id = Convert.ToInt32(Session["EMP_ID"]);
                string pos = Session["EMP_POS"].ToString();
                leave_DO = new LEAVE_DO();
                leave_DO.UPD_LEAVE_STATUS(pos, LEAVE_ID, STATUS, emp_id);
            }

        }
        public void UpdateStatusByList(List<int> LST_LeaveID, int STATUS)
        {
            if (LST_LeaveID.Count > 0)
            {
                if (Session["EMP_ID"] != null)
                {
                    string pos = Session["EMP_POS"].ToString();
                    leave_DO = new LEAVE_DO();
                    int emp_id = Convert.ToInt32(Session["EMP_ID"]);
                    foreach (int item in LST_LeaveID)
                    {
                        if (Condition_Status(item, pos))
                        {

                            leave_DO.UPD_LEAVE_STATUS(pos, item, STATUS, emp_id);
                        }
                    }
                }
            }
        }
        public bool Condition_Status(int LEAVE_ID, string pos)
        {
            Leave_Entities L_temp;
            L_temp = leave_DO.GetLeaveById(LEAVE_ID).FirstOrDefault();
            bool check_condition = true;
            //can change if manage and director status = 0
            if (pos == "SUP")
            {
                if (L_temp.MAN_STATUS > 0 || L_temp.DIR_STATUS > 0)
                {
                    if (L_temp.SUP_STATUS > 0)
                    {
                        check_condition = false;
                    }
                }
            }
            //can change if director status = 0
            else if (pos == "MAN")
            {
                if (L_temp.DIR_STATUS > 0)
                {
                    if (L_temp.MAN_STATUS > 0)
                    {
                        check_condition = false;
                    }
                }
            }
            return check_condition;
        }
        public void RefundStatus(int LEAVE_ID)
        {
            leave_DO = new LEAVE_DO();
            int i = leave_DO.Cancel(LEAVE_ID);
        }
        public void CancelByList(List<int> LST_LEAVEID)
        {
            leave_DO = new LEAVE_DO();
            if (LST_LEAVEID.Count > 0)
            {
                Leave_Entities L_temp;
                DateTime datenow = DateTime.Now;
                foreach (int i in LST_LEAVEID)
                {
                    L_temp = new Leave_Entities();
                    L_temp = leave_DO.GetLeaveById(i).FirstOrDefault();
                    if (L_temp != null)
                    {
                        if (L_temp.LEAVE_STARTDATE >= datenow || L_temp.STATUS_L == 0)
                        {
                            leave_DO.Delete(i);
                        }
                        else
                        {
                            leave_DO.Cancel(i);
                        }
                    }
                }
            }
        }
        public bool UpdateKindLeave(int LEAVE_ID, int KINDLEAVE_ID, int EMPLOYEE_ID)
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
        //public JsonResult LoadLeaveUpdate()
        //{
        //    init();
        //    GetLeaveCurrent();
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

    }

}