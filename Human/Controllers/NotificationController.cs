using Human.Areas.Admin.Models;
using Human.Common;
using Human.DO;
using Human.Models;
using Human.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Human.Controllers
{
    public class NotificationController : BaseController
    {
        AdminLeaveModel model = new AdminLeaveModel();
        LEAVE_DO leave_DO;
        Employee_DO _empDO;
        User_DO user_Do;
        public KindLeave_DO kindLeave_DO;
        public Section_DO section_DO;
      
        // GET: Notification
        public ActionResult Index()
        {
            if (Session["EMP_POS"] != null && !string.IsNullOrEmpty(Session["EMP_POS"].ToString()))
            {
                init();
                Session["COUNT_NOTIFI"] = model.LST_LEAVEVIEW.Count;
                return View(model);
            }
            else
            {
          
                CommonCheck.SetAlert(this, Human.Resource.Language.You_not_have_this_right, "error");
                return RedirectToAction("Home", "Index");
            }
        }
        public void init()
        {
           
            string condition = " AND STATUS_L = 4 ";
            leave_DO = new LEAVE_DO();
            model.LST_LEAVEVIEW = leave_DO.SP_SEL_LEAVE_MODEL(condition).OrderByDescending(o => o.LEAVE_STARTDATE).ToList();

        }

        public ActionResult Cancel(string LEAVE_ID)
        {
            leave_DO = new LEAVE_DO();
            int i = leave_DO.Cancel(Convert.ToInt32(LEAVE_ID));
            if (i > 0)
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Request_approved_success, "success");
            }
            else
            {
                CommonCheck.SetAlert(this, Human.Resource.Language.Request_approval_fail, "success");
            }
            return RedirectToAction("Index");
        }
    }
}