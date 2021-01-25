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
                    UserName = "ali2004h.linux", // Without @gmail.com or ...; فقط نام کاربری بدون @gmail.com
                    Password = "Ali2004h*ali"
                };
                client.Credentials = cr;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(email) },
                    From = new MailAddress("ali2004h.linux@gmail.com"), // Enter Your Mail Addres; ایمیل خودتون رو بنویسید
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
