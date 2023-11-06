using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace StatsSA.eRecruitment.InternalWeb.Extensions
{
    public class EmailExtension
    {
        public List<string> Recipients { get; set; }
        public string From { get; set; }
        public string EmailContent { get; set; }
        public string EmailSubject { get; set; }
        public bool useHtml { get; set; }
        public EmailExtension()
        {
            Recipients = new List<string>();
            this.From = "eRecruitment System <noreply@treasury.gov.za>";
            useHtml = false;
        }
        public bool Send()
        {
            bool blnSent = false;
            try
            {
                MailMessage msg = new MailMessage();
                foreach(string ricipient in Recipients)
                {
                    msg.To.Add(ricipient);
                }
                msg.From = new MailAddress(From);
                msg.Subject = EmailSubject;
                msg.Body = EmailContent;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = useHtml;
                msg.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                //client.Credentials = new System.Net.NetworkCredential(From, "adapt_dev");
                client.Port = 25;                   //'//Groupwise Port used for SMTP
                client.Host = "cenvsmtp01.treasury.gov.za";       //'//Groupwise IP used for SMTP in web config
                client.EnableSsl = false;           //'//This Groupwise SMTP IP doesn't use SSL

                client.Send(msg);
                blnSent = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                blnSent = false;
            }
            return blnSent;
        }
    }
}