using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.EmailVerification;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.EmailVerification
{
    public class VerifyEmailManager : GenericRepository<VerifyEmail>, IVerifyEmailManager
    {
        private readonly eRecruitmentEntities _context;

        public VerifyEmailManager(eRecruitmentEntities context) : base(context)
        {
            _context = context;
        }

        public VerifyEmail InsertVerifyEmail(VerifyEmail verifyEmail)
        {
            _context.VerifyEmails.Add(verifyEmail);
            _context.SaveChanges();
            return verifyEmail;
        }

        public VerifyEmail UpdateVerifyEmail(VerifyEmail verifyEmail)
        {
            base.Update(verifyEmail);
            _context.SaveChanges();
            return verifyEmail;
        }

        public VerifyEmail GetVerifyEmail(int userId)
        {
            return _context.VerifyEmails.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public bool CheckIfEmailVerified(int id)
        {
            bool verified = false;
            var verifyEmail = _context.VerifyEmails.Where(x => x.UserId == id && x.RequestStatusId == 3).FirstOrDefault();
            if(verifyEmail != null)
            {
                verified = true;
            }
            return verified ;
        }        

        public VerifyEmail VerifyEmail(string code)
        {
            return _context.VerifyEmails.Where(x => x.Code == code).FirstOrDefault();
        }

        public void SendEmail(User user, string code)
        {
            var templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplates/EmailVerificationTemplate.html");
            var emailVerifyPath = ConfigurationManager.AppSettings["emailVerificationPath"];

            TextInfo textInfo = new CultureInfo("en-ZA", false).TextInfo;
            string fullName = user.FirstName + ' ' + user.Surname;

            fullName = textInfo.ToTitleCase(fullName);

            string body = createEmailBody(templatePath, fullName, emailVerifyPath, user.Email, code);
            Helper.SendEmail(user.Email, "eRecruitment System - Email Verification", body);
        }

        private string createEmailBody(string templatePath, string fullname, string callbackUrl, string email, string otp)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(templatePath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{name}", fullname);
            body = body.Replace("{verifyEmail}", callbackUrl);
            body = body.Replace("{otp}", otp);
            body = body.Replace("{email}", email);
            return body;
        }
    }
}
