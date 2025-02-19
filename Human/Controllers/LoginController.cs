
using Human.DO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using Human.Models.Entities;
using Infobip.Api.Client.Api;
using Infobip.Api.Client.Model;
using Org.BouncyCastle.Cms;
using System.Net;
using Infobip.Api.Client;
using Infobip.Api.Client.Api;
using Infobip.Api.Client.Model;
using InfobipConfig = Infobip.Api.Client.Configuration;
using Org.BouncyCastle.Asn1.Crmf;
using Human.Common;
using System.Globalization;
using System.Threading;
using System.Web.Routing;
using static Human.Common.CommonAPI;

namespace Human.Controllers
{
    public class LoginController : BaseLoginController
    {
        private static readonly string BASE_URL = "https://e1eyx1.api.infobip.com";
        private static readonly string API_KEY = "f93d2541265d9a04e8a2c130b231fab9-b0e1e4a6-6d03-4dab-87c6-9652b204e6de";
        private static readonly string SENDER = "InfoSMS";
        private static readonly string RECIPIENT = "84363786687";

        public User_DO _user;
        public Employee_DO _empDO;
        public LicenseKey_DO licenseKey_do;
        public static int LogIn_DivID;
        string Code_OTP;
        public string Gmail;
        public string OTP;

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            Session.Remove("EMAIL");
            Session.Remove("EMP_CODE");
            Session.Remove("EMP_ID");
            Session.Remove("EMP_POS");
            Session.Remove("COUNT_NOTIFI");
            Session.Remove("POWER_BY_EMP");
            Session.Remove("FUNCTION_BY_EMP");
            /*ClsGlobal_Entities.POS = "";*/
            Session["EMP_POS"] = "";
            // ------------------------  Key bản quyền (sử dụng gg drive)  --------------------------------
            //CommonAPI commonAPI = new CommonAPI();
            //CommonAPI.ContentKeyInfo contentInfo = commonAPI.Get_content_key();
            //DateTime date;
            //DateTime.TryParseExact(contentInfo.DateEndText_Api, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            //licenseKey_do = new LicenseKey_DO();
            //var key_database = licenseKey_do.Get_LicensKey().FirstOrDefault();
            // So sánh với DateTime.Now
            //if (key_database.KEY_VALUE == contentInfo.KeySHA256_Api && key_database.EXPIRY_DATE.ToString("dd/MM/yyyy") == date.Date.ToString("dd/MM/yyyy"))
            //{
            //    if (date.Date > DateTime.Now.Date)
            //    {
            //        return View();
            //    }
            //    else if (date.Date < DateTime.Now.Date)
            //    {
            //        // Ngày trong tệp văn bản nhỏ hơn ngày hiện tại
            //        return RedirectToAction("Index", "LicensKey");
            //    }
            //    else
            //    {
            //        // Ngày trong tệp văn bản = ngày hiện tại
            //        return View();
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Index", "LicensKey");
            //}
            //----------------------- END key bản quyền -----------------------------------
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User_Entities u)
        {
            _empDO = new Employee_DO();
            if (IsEmail(u.EMAIL))
            {
                // login = email
                string condition = " AND EMAIL ='" + u.EMAIL + "' ";

                EMP_Login_Entities emp = _empDO.GetEmpLoginByEmail(u.EMAIL).FirstOrDefault();
                var emp_pwd = _empDO.GetEmployeeByCondition(condition).FirstOrDefault();

                DateTime date_login = DateTime.Now;
                if (u.EMAIL == null || (emp == null && emp_pwd == null))
                {
                    CommonCheck.SetAlert(this, Human.Resource.Language.Wrong_account_or_password, "error");
                    return View();
                }
                else
                {
                    //login with password

                    DateTime date_getotp = DateTime.Now;
                    if (u.PASSWORD != null && u.PASSWORD == emp_pwd.PASS_WORD)
                    {
                        _empDO.UpdateOTPEmp(emp_pwd.EMAIL, emp_pwd.EMP_CODE, emp_pwd.PASS_WORD, date_getotp);
                        _empDO.UpdateTimeLogin(emp_pwd.EMP_CODE, date_getotp, date_getotp);

                        Session["USERNAME"] = u.EMAIL;
                        Session["EMP_CODE"] = emp_pwd.EMP_CODE;
                        Session["EMP_ID"] = emp_pwd.EMPLOYEE_ID;
                        SetPosition(emp_pwd.JOBTITLE_ID);

                    }
                    else if ((u.PASSWORD != "" && u.PASSWORD != emp_pwd.PASS_WORD) || (u.OTP != u.PASSWORD && emp.TIME_GET_OTP.ToString("dd/MM/yyyy HH:mm") != date_getotp.ToString("d/MM/yyyy HH:mm")))
                    {

                        CommonCheck.SetAlert(this, Human.Resource.Language.Wrong_pass_please_re_enter, "error");
                        return View();
                    }
                    else
                    {
                        //login with otp
                        TimeSpan time = date_login.Subtract(emp.TIME_GET_OTP);
                        int minute = time.Minutes;

                        if (u.OTP == emp.OTP && minute <= 1)
                        {

                            _empDO.UpdateTimeLogin(emp.EMPLOYEE_CODE, emp.TIME_GET_OTP, date_login);

                            Session["EMAIL"] = u.EMAIL;
                            Session["EMP_CODE"] = emp.EMPLOYEE_CODE;
                            Session["EMP_ID"] = emp_pwd.EMPLOYEE_ID;
                            SetPosition(emp_pwd.JOBTITLE_ID);
                        }
                        else if (minute > 1)
                        {
                            CommonCheck.SetAlert(this, Human.Resource.Language.OTP_code_expired_please_get_code, "warning");

                            return View();
                        }
                        else
                        {
                            TempData["WAITING_OTP"] = minute;
                            CommonCheck.SetAlert(this, Human.Resource.Language.Wrong_OTP_password_please_re_enter, "error");
                            return View();
                        }
                    }

                }
            }
            else if (IsPhoneNumber(u.EMAIL))
            {
                // login = phonenumber (u.email là 1 tham số truyền từ view  qua)
                string condition = " AND PHONE_NUMBER ='" + u.EMAIL + "' ";

                EMP_Login_Entities emp = _empDO.GetEmpLoginByPhone(u.EMAIL).FirstOrDefault();
                var emp_pwp = _empDO.GetEmployeeByCondition(condition).FirstOrDefault();

                DateTime date_login = DateTime.Now;
                if (u.EMAIL == null || (emp == null && emp_pwp == null))
                {
                    CommonCheck.SetAlert(this, Human.Resource.Language.Wrong_account_or_password, "error");

                    return View();
                }
                else
                {
                    //login with password
                    DateTime date_getotp = DateTime.Now;
                    if (u.PASSWORD != null && u.PASSWORD == emp_pwp.PASS_WORD)
                    {
                        _empDO.UpdateOTPEmp_phone(emp_pwp.PHONE_NUMBER, emp_pwp.EMPLOYEE_ID, emp_pwp.EMP_CODE, emp_pwp.PASS_WORD, date_getotp);
                        _empDO.UpdateTimeLogin(emp_pwp.EMP_CODE, date_getotp, date_getotp);

                        Session["USERNAME"] = u.EMAIL;
                        Session["EMP_CODE"] = emp_pwp.EMP_CODE;
                        Session["EMP_ID"] = emp_pwp.EMPLOYEE_ID;
                        SetPosition(emp_pwp.JOBTITLE_ID);

                    }
                    else if ((u.PASSWORD != "" && u.PASSWORD != emp_pwp.PASS_WORD) || (u.OTP != u.PASSWORD && emp.TIME_GET_OTP.ToString("dd/MM/yyyy HH:mm") != date_getotp.ToString("d/MM/yyyy HH:mm")))
                    {
                        CommonCheck.SetAlert(this, Human.Resource.Language.Wrong_pass_please_re_enter, "error");
                        return View();
                    }
                    else
                    {
                        //login with otp of phone number
                        TimeSpan time = date_login.Subtract(emp.TIME_GET_OTP);
                        int minute = time.Minutes;

                        if (u.OTP == emp.OTP && minute <= 1)
                        {
                            _empDO.UpdateTimeLogin(emp.EMPLOYEE_CODE, emp.TIME_GET_OTP, date_login);

                            Session["EMAIL"] = u.EMAIL;
                            Session["EMP_CODE"] = emp.EMPLOYEE_CODE;

                            SetPosition(emp_pwp.JOBTITLE_ID);
                        }
                        else if (minute > 1)
                        {

                            CommonCheck.SetAlert(this, Human.Resource.Language.OTP_code_expired_please_get_code, "warning");
                            /*ViewBag.LoginFail = "Mã OTP hết hạn, vui lòng lấy mã mới.";*/
                            return View();
                        }
                        else
                        {
                            TempData["WAITING_OTP"] = minute;

                            CommonCheck.SetAlert(this, Human.Resource.Language.Wrong_OTP_password_please_re_enter, "error");
                            return View();
                        }
                    }
                }
            }
            else
            {
                //login with username
                string condition = " AND USERNAME='" + u.EMAIL + "'";
                var emp = _empDO.GetEmployeeByCondition(condition).FirstOrDefault();
                if (emp != null)
                {
                    if (emp.PASS_WORD == u.PASSWORD)
                    {
                        string sha = CommonCheck.ComputeSHA256Hash(u.PASSWORD);
                        DateTime date_getotp = DateTime.Now;
                        //  _empDO.UpdateOTPEmp_phone(emp.EMAIL, emp.EMPLOYEE_ID, emp.EMP_CODE, emp.PASS_WORD, date_getotp);
                        //  _empDO.UpdateTimeLogin(emp.EMP_CODE, date_getotp, date_getotp);

                        Session["USERNAME"] = u.EMAIL;
                        Session["EMP_CODE"] = emp.EMP_CODE;
                        Session["EMP_ID"] = emp.EMPLOYEE_ID;
                        //
                        SetPosition(emp.JOBTITLE_ID);
                        //
                    }
                    else
                    {
                        TempData["WAITING_PWD"] = 1;

                        CommonCheck.SetAlert(this, Human.Resource.Language.Wrong_account_or_password, "error");

                        return View();
                    }
                }

                else
                    return View();
            }

            return RedirectToAction("Index", "Home");

            //Session["EMAIL"] = u.EMAIL;
            //Session["EMP_CODE"] = "2100052";
            //ViewBag.EMP_ID = 1;
            //return RedirectToAction("Index", "Home");

        }

        public void SetPosition(int position)
        {
            if (position == 1 || position == 11)
            { // position = 1,11 là Senior Director,General Director
                /*ClsGlobal_Entities.POS = "DIR";*/
                Session["EMP_POS"] = "DIR";
            }
            else if (position == 2 || position == 3)
            { // position = 2,3 là Senior Manager,Manager
                /*ClsGlobal_Entities.POS = "MAN";*/
                Session["EMP_POS"] = "MAN";
            }
            else if (position == 5)
            { // position = 5 là Supervisor
                /* ClsGlobal_Entities.POS = "SUP";*/
                Session["EMP_POS"] = "SUP";
            }
        }
        public int CheckEmail(string USER_GMAIL)
        {
            int i = 0;
            if (USER_GMAIL == "")
            {
                return -1;
            }
            else
            {
                _empDO = new Employee_DO();
                string condition = "";
                if (IsEmail(USER_GMAIL))
                {
                    condition += " AND EMAIL='" + USER_GMAIL + "'";
                }
                else if (IsPhoneNumber(USER_GMAIL))
                {
                    condition += " AND PHONE_NUMBER='" + USER_GMAIL + "'";
                }
                else
                {
                    condition += " AND USERNAME='" + USER_GMAIL + "'";

                }
                var emp = _empDO.GetEmployeeByCondition(condition);
                if (emp.Count() > 0)
                {
                    i = 1;
                    return i;
                }
                return i;
            }
        }
        public int SendEmail(string GMAIL)
        {
            int i = 0;

            _empDO = new Employee_DO();
            if (IsEmail(GMAIL))
            {
                string condition = "";
                condition += " AND EMAIL='" + GMAIL + "'";
                var emp = _empDO.GetEmployeeByCondition(condition).FirstOrDefault();
                if (emp != null)
                {
                    i = 1;
                    DateTime date_getotp = DateTime.Now;
                    _empDO = new Employee_DO();
                    CommonOTP comm = new CommonOTP();
                    Code_OTP = comm.MakeRandomOTP(4);
                    string imagePath = Server.MapPath("~/Assets/img/logo.png");
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Shared/Template/SendOTP.cshtml"));
                    /*content = content.Replace("{{ImageSrc}}", imagePath);*/
                    content = content.Replace("{{OTP}}", Code_OTP);
                    content = content.Replace("{{USERNAME}}", emp.FULLNAME);
                    content = content.Replace("{{GMAIL}}", emp.EMAIL);
                    content = content.Replace("{{HELLO}}", Resource.Language.Hello);
                    content = content.Replace("{{SECURITY_ACCOUNT}}", Resource.Language.security_for_account);
                    content = content.Replace("{{SECURITY_CODE}}", Resource.Language.Security_code);
                    content = content.Replace("{{ABSOLUTELY_OTP}}", Resource.Language.Absolutely_otp);
                    content = content.Replace("{{THANKS}}", Resource.Language.Thanks);
                    content = content.Replace("{{ACCOUNT_TEAM}}", Resource.Language.Account_team);
                    comm.SendMail(GMAIL, "Mã OTP Đăng Nhập Website.", content);
                    _empDO.UpdateOTPEmp(GMAIL, emp.EMP_CODE, Code_OTP, date_getotp);
                }
            }
            else if (IsPhoneNumber(GMAIL))
            {
                // GMAIL là tên biến chứa tham số truyền từ input vào (có thể là gmail hoặc phone number)
                string condition = "";
                condition += " AND PHONE_NUMBER='" + GMAIL + "'";
                var emp = _empDO.GetEmployeeByCondition(condition).FirstOrDefault();
                if (emp != null)
                {
                    i = 1;
                    DateTime date_getotp = DateTime.Now;
                    _empDO = new Employee_DO();
                    // Sinh mã OTP ngẫu nhiên (ví dụ sử dụng Random)
                    Random random = new Random();
                    string Code_OTP = random.Next(1000, 9999).ToString();
                    string content = "Mã xác nhận của bạn là: " + Code_OTP;
                    sendsms(content);
                    _empDO.UpdateOTPEmp_phone(GMAIL, emp.EMPLOYEE_ID, emp.EMP_CODE, Code_OTP, date_getotp);
                }

            }
            return i;
        }
        private void sendsms(string content)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var configuration = new InfobipConfig()
            {
                BasePath = BASE_URL,
                ApiKeyPrefix = "App",
                ApiKey = API_KEY
            };

            var sendSmsApi = new SendSmsApi(configuration);

            var smsMessage = new SmsTextualMessage()
            {
                From = SENDER,
                Destinations = new List<SmsDestination>()
                {
                    new SmsDestination(to: RECIPIENT)
                },
                Text = content
            };

            var smsRequest = new SmsAdvancedTextualRequest()
            {
                Messages = new List<SmsTextualMessage>() { smsMessage }
            };

            try
            {
                var smsResponse = sendSmsApi.SendSmsMessage(smsRequest);
                ViewBag.ResponseMessage = "SMS gửi thành công.";
            }
            catch (Infobip.Api.Client.ApiException apiException)
            {
                ViewBag.ResponseMessage = $"Đã xảy ra lỗi! Thông điệp: {apiException.ErrorContent}";
            }
        }
        protected bool IsEmail(string input)
        {
            // Biểu thức chính quy để kiểm tra địa chỉ email
            if (input != null)
            {
                //string emailRegex = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b";
                string emailRegex = @".*@.*";
                return System.Text.RegularExpressions.Regex.IsMatch(input, emailRegex);
            }
            else
                return false;
        }
        protected bool IsPhoneNumber(string input)
        {
            // Biểu thức chính quy để kiểm tra số điện thoại
            if (input != null)
            {
                string phoneRegex = @"\b\d{10}\b";
                return System.Text.RegularExpressions.Regex.IsMatch(input, phoneRegex);
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


    }
}