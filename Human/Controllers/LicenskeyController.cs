using Human.Common;
using Human.DO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Human.Controllers
{
    public class LicenskeyController : BaseLoginController
    {
        LicenseKey_DO licensekey_do;
        
        public ActionResult Index()
        {
            /*licensekey_do = new LicenseKey_DO();
              CommonAPI commonAPI = new CommonAPI();
              CommonAPI.ContentKeyInfo contentInfo = commonAPI.Get_content_key();
              DateTime date;
              DateTime.TryParseExact(contentInfo.DateEndText_Api, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
              if (date.Date < DateTime.Now.Date)
              {
                  // kiểm tra nếu ngày hết hạn lớn hơn ngày hiện tại thì trả về view nhập key
                  return View();
              }
              else
              {
                  // trả về trang login nếu còn hạn
                  return RedirectToAction("Index","Login");
              }*/
            return View();
        }
        [HttpPost]
        public ActionResult InsertKey(string key_licens)
        {
            CommonAPI commonAPI = new CommonAPI();
            CommonAPI.ContentKeyInfo contentInfo = commonAPI.Get_content_key();
            if (key_licens == contentInfo.KeyText_Api)
            {
                DateTime date;
                DateTime.TryParseExact(contentInfo.DateEndText_Api, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
                licensekey_do = new LicenseKey_DO();
                licensekey_do.Update_LicenseKey(contentInfo.KeySHA256_Api,date.Date.ToString("yyyy-MM-dd"));
                return RedirectToAction("Index","Login");
            }
            else
            {
                CommonCheck.SetAlert(this,"Key không đúng vui lòng nhập lại!!!","error");
                return RedirectToAction("Index");
            }      
        }
    }
}