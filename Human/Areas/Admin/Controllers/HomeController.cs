using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Human.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Hime
        public ActionResult Index()
        {
            return View();
        }
    }
}