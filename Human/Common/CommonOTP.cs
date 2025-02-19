using Human.DO;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Human.Common
{
    public class CommonOTP
    {
        EmailSMS_DO _smsDO;
        public string MakeRandomOTP(int length)
        {
            string UpperCase = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string LowerCase = "qwertyuiopasdfghjklzxcvbnm";
            string Digits = "1234567890";
            string allcharacter = UpperCase + LowerCase + Digits;
            Random R = new Random();
            string password = "";
            for (int i = 0; i < length; i++)
            {
                double rand = R.NextDouble();
                password += Math.Floor(rand * Digits.Length);

            }
            return password;
        }
        public void SendMail(string toEmail, string subject, string body)
        {
            //var FromEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            //var EmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();

            // lấy dữ liệu infomaiton email sms từ csdl
            _smsDO = new EmailSMS_DO();
            var info_sms = _smsDO.GetEmailSMS().FirstOrDefault();
            // truyền email, pass để tiến hành gửi mã otp qua gmail
            string FromEmail = info_sms.FromEmailAddressSMS;
            string EmailPassword = info_sms.FromEmailPasswordSMS;

            var DisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpport = int.Parse(ConfigurationManager.AppSettings["SMTPPort"].ToString());


            var Email = new MimeMessage();
            Email.From.Add(new MailboxAddress("send", FromEmail));
            Email.To.Add(new MailboxAddress("", toEmail));
            Email.Subject = subject;
            Email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.ServerCertificateValidationCallback = (l, j, c, m) => true;
                smtp.Connect(smtpHost, smtpport, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(FromEmail, EmailPassword);
                smtp.Send(Email);
            }

        }
    }
}