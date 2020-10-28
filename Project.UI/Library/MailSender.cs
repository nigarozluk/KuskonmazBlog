using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project.UI.Library
{
    public class MailSender
    {
        private readonly IWebHostEnvironment env;

        public MailSender(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public void SendMail(string content, string senderMail)
        {
            string settingsContent = System.IO.File.ReadAllText(env.ContentRootPath + "/settings.json");
            JObject root = JObject.Parse(settingsContent);
            JObject mailSettings = (JObject)root["mail-settings"];

            string host = (string)mailSettings["host"];
            int port = (int)mailSettings["port"];
            string UserName = (string)mailSettings["username"];
            string mail_pass = (string)mailSettings["password"];
            

            SmtpClient sc_mail = new SmtpClient();

            //gmail kullanarak mail gönderme
            sc_mail.Host = host;
            sc_mail.Port = port;
            sc_mail.Credentials = new NetworkCredential(UserName,mail_pass);
            sc_mail.EnableSsl = true;
            sc_mail.UseDefaultCredentials = false;
            sc_mail.DeliveryMethod = SmtpDeliveryMethod.Network;


            string newContent = $@"
            <table border='1'>
            <tr><td>Sender Mail : </td><td>{senderMail}</td></tr>
            <tr><td>Content : </td><td>{content}</td></tr>
            </table>
            ";

            MailMessage mail = new MailMessage();
            MailAddress fromAddress = new MailAddress(UserName); //mailin hangi adresten geldiği
            mail.From = fromAddress;
            mail.To.Add(UserName); //mail gönderilecek adres
            mail.Subject = "test";
            mail.IsBodyHtml = true;
            mail.Body = newContent;
            sc_mail.Send(mail);
        }
    }
}

