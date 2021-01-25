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
            using (var client = new SmtpClient())
            {
                var cr = new NetworkCredential()
                {
                    UserName = "test", // Without @gmail.com or ...; فقط نام کاربری بدون @gmail.com
                    Password = "ali2004h*"
                };
                client.Credentials = cr;
                client.Host = "cp42.tavanahost.com";
                client.Port = 465;
                client.EnableSsl = true;
                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(email) },
                    From = new MailAddress("test@ali2004h.ir"), // Enter Your Mail Addres; ایمیل خودتون رو بنویسید
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
