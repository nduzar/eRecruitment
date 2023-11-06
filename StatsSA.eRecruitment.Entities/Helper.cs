using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace StatsSA.eRecruitment.Entities
{
    public class Helper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string GenerateOTP()
        {
            int maxSize = 6; // whatever length you want
            //char[] chars = new char[62];
            string a;
            a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] chars = new char[a.Length];
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }

        public static void SendEmail(string recepient, string subject, string message)
        {
            //string emailSubj = "eRecruitment System - Password Recovery";

            string strMsg = message;
            string from = "no-reply@treasury.gov.za";  // eRecruitment@statssa.gov.za   adapt_dev     Environment.UserName + "@StatsSA.gov.za"; //"Adapt_dev@StatsSA.gov.za";
            MailMessage msg = new MailMessage();

            msg.To.Add(recepient);
            msg.From = new MailAddress(from);
            msg.Subject = subject;
            msg.Body = strMsg;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient("cenvsmtp01.treasury.gov.za");
            //SmtpClient client = new SmtpClient("zatreasury.mail.protection.outlook.com");
            //client.Credentials = new System.Net.NetworkCredential("eRecruitment@treasury.gov.za", "eR3cruitm3nt@2021");
            //client.Credentials = new System.Net.NetworkCredential("ADAPT_DEV", "adapt_dev");
            client.Port = 25;                   //'//Groupwise Port used for SMTP     //'//Groupwise IP used for SMTP in web config
            client.EnableSsl = false;           //'//This Groupwise SMTP IP doesn't use SSL

            client.Send(msg);
        }
    }

    //public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    //{
    //    public CustomMultipartFormDataStreamProvider(string rootPath) : base(rootPath)
    //    {
    //    }

    //    public CustomMultipartFormDataStreamProvider(string rootPath, int bufferSize) : base(rootPath, bufferSize)
    //    {
    //    }

    //    public override string GetLocalFileName(HttpContentHeaders headers)
    //    {
    //        //Make the file name URL safe and then use it & is the only disallowed url character allowed in a windows filename
    //        var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName)
    //          ? headers.ContentDisposition.FileName
    //          : "NoName";
    //        return name.Trim('"').Replace("&", "and");
    //    }
    //}
}
