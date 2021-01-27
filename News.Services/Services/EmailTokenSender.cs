using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using News.Services.Repositories;

namespace News.Services.Services
{
    public class EmailTokenSender:IEmailSend
    {

        public Task Sendsms(string email, string subjet, string message, bool ishtml = false)
        {
            using (SmtpClient client = new SmtpClient())
            {
                NetworkCredential cr = new NetworkCredential()
                {
                    UserName = "", // Without @gmail.com or ...; فقط نام کاربری بدون @gmail.com
                    Password = ""
                };
                client.Credentials = cr;
                client.Host = "gmail.com";
                client.Port = 468;
                client.EnableSsl = true;
                using(MailMessage emailMessage = new MailMessage())
                {
                    To = { new MailAddress(email) },
                    From = new MailAddress(""), // Enter Your Mail Addres; ایمیل خودتون رو بنویسید
                    Subject = subjet,
                    Body = message,
                    IsBodyHtml = ishtml
                };
                client.Send(emailMessage);
            };
            return Task.CompletedTask;
        }
    }
}
