using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Human.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            switch (type)
            {
                case "success":
                    TempData["AlertType"] = "alert-success"; break;
                case "warring":
                    TempData["AlertType"] = "warning"; break;
                case "error":
                    TempData["AlertType"] = "alert-error"; break;
                default: TempData["AlertType"] = ""; break;
            }
        }
        protected bool IsEmail(string input)
        {
            // Biểu thức chính quy để kiểm tra địa chỉ email
            if (input != null)
            {
                string emailRegex = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b";
                return System.Text.RegularExpressions.Regex.IsMatch(input, emailRegex);
            }
            else
                return false;
        }

        protected bool IsUsername(string input)
        {
            // Biểu thức chính quy để kiểm tra tên người dùng
            string usernameRegex = @"^[A-Za-z0-9_]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(input, usernameRegex);
        }
        protected override void OnActionExecuted(ActionExecutedContext context)
        {
            var session = Session["EMP_CODE"];
            if (session == null)
            {
                // context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Admin", controller = "Login", action = "Index" }));
                context.Result = RedirectToAction("Index","Login", new { area = "Admin" });
            }
            base.OnActionExecuted(context);
        }
    }
}