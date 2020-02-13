using KPI.Model.helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace DemoDoan.helpers
{
    public static class Commons
    {
        public enum ROLE
        {
            ADM,
            DEPTHEAD,
            SUP,
            USER
        }
        public static bool SendMail(string from, string to, string subject, string content)
        {
            string title = ConfigurationManager.AppSettings["Title"].ToSafetyString();

            MailMessage mail = new MailMessage();
            mail.To.Add(to.ToString());
            mail.From = new MailAddress(from, title);
            mail.Subject = subject;
            mail.Body = content;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Priority = MailPriority.High;

            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(mail);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}