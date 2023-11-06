using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.ManagePassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using System.Net.Mail;
using System.IO;
using System.Globalization;


namespace StatsSA.eRecruitment.Provider.ManagePassword
{
    public class ManagePasswordProvider : IManagePasswordProvider
    {
        private IUnitOfWork _unitOfWork;

        public ManagePasswordProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Entities.ApplicantProfile ForgotPassword(string idNumber)
        {
            string tempPassword = string.Empty;
            this._unitOfWork.BeginTransaction();
            //1. get the applicant
            var applicant = this._unitOfWork.ApplicantProfileManager.GetApplicantProfileByIdNo(idNumber);
            //2 generate temp password
            if (applicant != null)
            {
                tempPassword = this.GenerateTempPassword();

                if (tempPassword != string.Empty)
                {
                    //lock application and update password for the Applicant
                    LockUnlockPassword(Convert.ToInt16(applicant.UserId), tempPassword, false);
                    //send sms
                    //if using complext object u will file cellnumber ApplicantContactDetails
                    this.SendSMS("07609393", "Hi blal blal bl", "80010289397920");
                    TextInfo textInfo = new CultureInfo("en-ZA", false).TextInfo;
                    var user = this._unitOfWork.UserManager.GetUserByUserId(Convert.ToInt16(applicant.UserId));
                    string msgBody = "Dear " + textInfo.ToTitleCase(applicant.FirstNames + applicant.Surname) + Environment.NewLine;
                    msgBody += Environment.NewLine + "Your request for password notification has been processed.";
                    msgBody += Environment.NewLine + "Click on this url to reset your password: http://10.121.144.44:9001/#/resetpassword" + Environment.NewLine;
                    msgBody += Environment.NewLine + "Use a combination of your ID Number and this temporary password " + tempPassword + Environment.NewLine;
                    msgBody += Environment.NewLine + "Statistics South Africa - HR Recruitment Team";

                   Helper.SendEmail(user.Email, "eRecruitment System - Password Recovery", msgBody );
                }
            }
            this._unitOfWork.CommitTransaction();

            return applicant;
        }

        private string GenerateTempPassword()
        {
            Random random = new Random();

            string s = "";
            for (int i = 0; i < 8; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public void ResetPassword(string newPassword, int userId)
        {
            var request = _unitOfWork.PasswordResetRequestManager.GetPasswordResetRequestByUserId(userId);
            var user = this._unitOfWork.UserManager.GetUserByUserId(userId);

            if(request.ExpiryDate >= DateTime.Now)
            {
                if(request.PasswordRequestStatusId == 1)
                {
                    if (user != null)
                    {
                        _unitOfWork.BeginTransaction();
                        user.Password = Helper.GetHash(newPassword);
                        user.IsActive = true;
                        user.ModifiedDate = DateTime.Now;
                        _unitOfWork.UserManager.UpdateUser(user);
                        _unitOfWork.SaveChanges();
                        request.PasswordRequestStatusId = 2;
                        _unitOfWork.PasswordResetRequestManager.UpdatePasswordResetRequest(request);
                        _unitOfWork.SaveChanges();
                        _unitOfWork.CommitTransaction();
                    }
                }                
            }           
        }
        private void SendSMS(string cellNumber, string smsText, string idNumber)
        {
            //to do consume web service to send sms or ...
        }

        //public bool SendEmail(string receiver, string message)
        //{
        //    bool blnSent = false;
        //    string to = receiver;
            
        //    string emailSubj = "eRecruitment System - Password Recovery";
        //    string strMsg = message;
        //    string from = "eRecruitment - National Treasury <noreply@treasury.gov.za>";  // eRecruitment@statssa.gov.za   adapt_dev     Environment.UserName + "@StatsSA.gov.za"; //"Adapt_dev@StatsSA.gov.za";
        //    MailMessage msg = new MailMessage();

        //    msg.To.Add(to);
        //    msg.From = new MailAddress(from);
        //    msg.Subject = emailSubj;
        //    msg.Body = strMsg;
        //    msg.BodyEncoding = System.Text.Encoding.UTF8;
        //    msg.IsBodyHtml = true;
        //    msg.Priority = MailPriority.High;
        //    SmtpClient client = new SmtpClient("cenvsmtp01.treasury.gov.za");
        //    //client.Credentials = new System.Net.NetworkCredential("ADAPT_DEV", "adapt_dev");
        //    client.Port = 25;                   //'//Groupwise Port used for SMTP     //'//Groupwise IP used for SMTP in web config
        //    client.EnableSsl = false;           //'//This Groupwise SMTP IP doesn't use SSL

        //    client.Send(msg);
        //    blnSent = true;
        //    return blnSent;
        //}

        public void LockUnlockPassword(int userId, string tempPassword, bool isActive)
        {
            this._unitOfWork.ManagePasswordManager.LockUnlockPassword(userId, tempPassword, isActive);
        }


        public void PasswordReset(string templatePath, int userId, string email, string cellnumber, string method, string fullname, string resetPasswordPath)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var existingRequest = _unitOfWork.PasswordResetRequestManager.GetPasswordResetRequestByUserId(userId);

                if (existingRequest != null)
                {
                    existingRequest.PasswordRequestStatusId = 3;
                    existingRequest.ModifiedDate = DateTime.Now;
                    _unitOfWork.PasswordResetRequestManager.UpdatePasswordResetRequest(existingRequest);

                }
                //Generate Code
                var code = Helper.GenerateOTP();
                var PasswordResetRequest = new PasswordResetRequest
                {
                    ResetCode = code,
                    UserId = userId,
                    IssueDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddHours(24),
                    PasswordRequestStatusId = 1
                };

                var request = _unitOfWork.PasswordResetRequestManager.AddPasswordResetRequest(PasswordResetRequest);
                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
                TextInfo textInfo = new CultureInfo("en-ZA", false).TextInfo;
                fullname = textInfo.ToTitleCase(fullname);
                if (method == "email")
                {
                    string body = createEmailBody(templatePath, fullname, resetPasswordPath, email, request.ResetCode);
                    Helper.SendEmail(email, "eRecruitment System - Password Recovery", body);
                }
                if (method == "mobile")
                {
                    SendSMS(cellnumber, "", fullname);
                }
            }
            catch(Exception ex)
            {                
                throw ex;
            }
        }

        public PasswordResetRequest ValidateResetCode(string resetCode)
        {
            var passwordResetRequest = _unitOfWork.PasswordResetRequestManager.GetPasswordResetRequestByResetCode(resetCode);

            if(passwordResetRequest == null)
            {
                return null;
            }
            if (passwordResetRequest.ExpiryDate < DateTime.Now)
            {
                passwordResetRequest.PasswordRequestStatusId = 3;
                passwordResetRequest.ModifiedDate = DateTime.Now;
                _unitOfWork.PasswordResetRequestManager.UpdatePasswordResetRequest(passwordResetRequest);
                _unitOfWork.SaveChanges();
                return null;
            }
            //if (passwordResetRequest.PasswordRequestStatusId != 1)
            //{
            //    passwordResetRequest.PasswordRequestStatusId = 3;
            //    passwordResetRequest.ModifiedDate = DateTime.Now;
            //    _unitOfWork.PasswordResetRequestManager.UpdatePasswordResetRequest(passwordResetRequest);
            //    _unitOfWork.SaveChanges();
            //    return false;
            //}
            return passwordResetRequest;
        }

        private string createEmailBody(string templatePath, string fullname, string callbackUrl, string email, string otp)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(templatePath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{name}", fullname);
            body = body.Replace("{url}", callbackUrl);
            body = body.Replace("{otp}", otp);
            body = body.Replace("{email}", email);
            return body;
        }
    }
}
