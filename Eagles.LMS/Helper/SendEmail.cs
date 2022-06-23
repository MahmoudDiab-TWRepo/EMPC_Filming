using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;

namespace Eagles.LMS.Helper
{
    public class SendEmail
    {
        EmcNewsContext db = new EmcNewsContext();

        public bool SendMail(EmailDTO obj)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string Emails = System.Configuration.ConfigurationManager.AppSettings["TOEMail"];
            string Adimn_Subject = System.Configuration.ConfigurationManager.AppSettings["Admin_New_Booking"];
            string User_Subject = System.Configuration.ConfigurationManager.AppSettings["User_New_Booking"];
            string Bodymessage = System.Configuration.ConfigurationManager.AppSettings["UserBody"];
            //Keys k1 = (Keys)Enum.Parse(typeof(Keys), Reciever);
            //Keys key;
            //if (Enum.TryParse<Keys>(Reciever, out key))
            //{
            //}
            string Reciever = Emails/* + "," + obj.To*/;
            //using (MailMessage mm = new MailMessage(smtpSection.From, Reciever))
            //{
            //    mm.Subject = Adimn_Subject;
            //    mm.Body = obj.Message;
            //    mm.IsBodyHtml = false;
            //    SmtpClient smtp = new SmtpClient();
            //    smtp.Host = smtpSection.Network.Host;
            //    smtp.EnableSsl = smtpSection.Network.EnableSsl;
            //    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
            //    smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
            //    smtp.Credentials = networkCred;
            //    smtp.Port = smtpSection.Network.Port;
            //    smtp.Send(mm);
            //}
            using (MailMessage mm = new MailMessage(smtpSection.From, obj.To))
            {
                mm.Subject = User_Subject;
                mm.Body = Bodymessage + "\n" + obj.Message;
                mm.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpSection.Network.Host;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                smtp.Credentials = networkCred;
                smtp.Port = smtpSection.Network.Port;
                smtp.Send(mm);
            }

            return true;
        }

    }
    public class EmailDTO
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}