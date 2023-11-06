using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.ApplicantContactDetails;
using StatsSA.eRecruitment.IProvider.ApplicantProfiles;
using StatsSA.eRecruitment.IProvider.ContactMethods;
using StatsSA.eRecruitment.IProvider.ManagePassword;
using StatsSA.eRecruitment.IProvider.Users;
using StatsSA.eRecruitment.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using static System.Net.WebRequestMethods;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [RoutePrefix("api/managepassword")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ManagePasswordController : ApiController
    {
        private IManagePasswordProvider _managePasswordProvider;
        private IApplicantProfileProvider _applicantProfileProvider;
        private IApplicantContactDetailsProvider _applicantContactDatailsProvider;
        private IUserProvider _userProvider;

        public ManagePasswordController(IManagePasswordProvider managePasswordProvider,
            IApplicantProfileProvider applicantProfileProvider,
            IApplicantContactDetailsProvider applicantContactDatailsProvider,
            IUserProvider userProvider)
        {
            _managePasswordProvider = managePasswordProvider;
            _applicantProfileProvider = applicantProfileProvider;
            _applicantContactDatailsProvider = applicantContactDatailsProvider;
            _userProvider = userProvider;
        }

        [HttpPost, Route("forgotpassword/")]
        public IHttpActionResult ForgotPassword(ForgotIt obj)
        {
            var result = _managePasswordProvider.ForgotPassword(obj.IDNo);
            return Ok(result);
        }

        [HttpPost, Route("resetpassword")]
        public IHttpActionResult ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            _managePasswordProvider.ResetPassword(resetPasswordRequest.NewPassword, resetPasswordRequest.UserId);
            return Ok();
        }

        [HttpPost, Route("searchAccount")]
        public IHttpActionResult SearchAccount(SearchAccountRequest searchAccountRequest)
        {
            User user = _userProvider.GetUserByIDNo(searchAccountRequest.IdentityNumber);
            if (user != null)
            {

                var result = new SearchAccountResult
                {
                    HashedCellNumber = user.Cellphone.Substring(user.Cellphone.Length - 4).PadLeft(user.Cellphone.Length, '*'),
                    HashedEmail = Regex.Replace(user.UserName, @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)", m => new string('*', m.Length)),
                    CellNumber = user.Cellphone,
                    Email = user.UserName,
                    UserId = user.Id,
                    Name = user.FirstName,
                    Surname = user.Surname
                };

                return Ok(result);
            }
            return Content(HttpStatusCode.NotFound, "ID Number provided does not exist");
        }
        [HttpPost, Route("searchAccountByEmail")]
        public IHttpActionResult SearchAccountByEmail(SearchAccountRequestByEmail searchAccountRequestByEmail)
        {
            User user = _userProvider.GetUserByEmail(searchAccountRequestByEmail.Email);
            if (user != null)
            {

                var result = new SearchAccountResultByEmail
                {
                    Email = user.Email,
                    HashedEmail = Regex.Replace(user.Email, @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)", m => new string('*', m.Length)),
                    SecondaryEmail = user.SecondaryEmail,
                    HashedSecondaryEmail = Regex.Replace(user.SecondaryEmail, @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)", m => new string('*', m.Length)),
                    UserName = user.UserName,
                    Id = user.Id,
                    IdentityNumber = user.IdentityNumber,
                    FirstName = user.FirstName,
                    Surname = user.Surname
                };

                return Ok(result);
            }
            return Content(HttpStatusCode.NotFound, "Email provided does not exist");
        }

        [HttpPost, Route("sendOtp")]
        public IHttpActionResult SendOtp(SearchAccountResultByEmail otpRequestForEmail)
        {
            string secEmail = otpRequestForEmail.SecondaryEmail;
            //var recipent = otpRequestForEmail.SearchAccountResultByEmail.Select(x => x.SecondaryEmail);
            var otp = Helper.GenerateOTP();

            string from = "no-reply@treasury.gov.za";  // eRecruitment@statssa.gov.za   adapt_dev     Environment.UserName + "@StatsSA.gov.za"; //"Adapt_dev@StatsSA.gov.za";
            MailMessage msg = new MailMessage();

            msg.To.Add(secEmail);
            msg.From = new MailAddress(from);
            msg.Subject = "";
            msg.Body = otp;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient("cenvsmtp01.treasury.gov.za");
            //SmtpClient client = new SmtpClient("zatreasury.mail.protection.outlook.com");
            //client.Credentials = new System.Net.NetworkCredential("eRecruitment@treasury.gov.za", "eR3cruitm3nt@2021");
            //client.Credentials = new System.Net.NetworkCredential("ADAPT_DEV", "adapt_dev");
            client.Port = 25;                   //'//Groupwise Port used for SMTP     //'//Groupwise IP used for SMTP in web config
            client.EnableSsl = false;           //'//This Groupwise SMTP IP doesn't use SSL
            User user = new User();
            user.EmailOtp = otp;
            user.Id = otpRequestForEmail.Id;

            _userProvider.UpdateOtp(user);
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {               
            }

            return Ok(true);

        }
        [HttpPost, Route("otpVerifivation")]
        public IHttpActionResult OtpVerification(SearchAccountResultByEmail otpRequestForEmail)
        {
            var result = _userProvider.GetUserByOtp(otpRequestForEmail.Otp);
            if (result != null)
            {
                User user = new User();
                user.Email = otpRequestForEmail.Email;
                user.Id = otpRequestForEmail.Id;
                _userProvider.UpdatePrimaryEmail(user);
                return Ok();
            }

            return InternalServerError(new Exception("You don't have any valid Otp right now"));

        }
        [HttpPost, Route("passwordReset")]
        public IHttpActionResult PasswordReset(PasswordResetRequestModel passwordResetRequestModel)
        {
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var resetPasswordPath = ConfigurationManager.AppSettings["resetPasswordPath"];
            var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplates/ResetPasswordTemplate.html");
            var fullname = passwordResetRequestModel.SearchAccountResult.Name + " " + passwordResetRequestModel.SearchAccountResult.Surname;
            _managePasswordProvider.PasswordReset(mappedPath, passwordResetRequestModel.SearchAccountResult.UserId, passwordResetRequestModel.SearchAccountResult.Email, passwordResetRequestModel.SearchAccountResult.CellNumber, passwordResetRequestModel.ResertPasswordMethod, fullname, resetPasswordPath);
            return Ok();
        }

        [HttpPost, Route("codeVerifivation")]
        public IHttpActionResult CodeVerification(CodeVerificationRequest request)
        {
            var result = _managePasswordProvider.ValidateResetCode(request.ResetCode);
            if (result != null)
            {
                var user = _userProvider.GetUserByUserId(result.UserId);
                var data = new { PasswordResetRequest = result, username = user.UserName };
                return Ok(data);
            }

            return InternalServerError(new Exception("You don't have any valid codes right now"));

        }
    }

    public class ForgotIt
    {
        public string IDNo { get; set; }
    }

    public class ResetIt
    {
        public string IDNo { get; set; }
        public string tempPass { get; set; }
        public string newPass { get; set; }
    }
}