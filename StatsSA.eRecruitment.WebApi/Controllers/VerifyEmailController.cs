using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.ApplicantContactDetails;
using StatsSA.eRecruitment.IProvider.ApplicantProfiles;
using StatsSA.eRecruitment.IProvider.ContactMethods;
using StatsSA.eRecruitment.IProvider.EmailVerification;
using StatsSA.eRecruitment.IProvider.ManagePassword;
using StatsSA.eRecruitment.IProvider.Users;
using StatsSA.eRecruitment.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [RoutePrefix("api/verifyemail")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VerifyEmailController : ApiController
    {
        private IVerifyEmailProvider _verifyEmailProvider;
        private IUserProvider _userProvider;

        public VerifyEmailController(IVerifyEmailProvider verifyEmailProvider,IUserProvider userProvider)
        {
            _verifyEmailProvider = verifyEmailProvider;
            _userProvider = userProvider;
        }

        [HttpPost, Route("searchAccount")]
        public IHttpActionResult SearchAccount(SearchAccountRequest searchAccountRequest)
        {
            User user = _userProvider.GetUserByIDNo(searchAccountRequest.IdentityNumber);
            if(user != null)
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
            return Content(HttpStatusCode.NotFound, "Your search did not return any results. Please try again with other information.");
        }

        [HttpPost, Route("addEmailVerification")]
        public IHttpActionResult AddEmailVerification(User user)
        {
            //Generate Code
            var code = Helper.GenerateOTP();
            var verifyEmail = new VerifyEmail
            {
                Code = code,
                IssueDate = DateTime.Now,
                UserId = user.Id,
                ExpiryDate = DateTime.Now.AddDays(1),
                RequestStatusId = 1,
                ModifiedDate = DateTime.Now
            };
            
            var result = _verifyEmailProvider.InsertVerifyEmail(verifyEmail);

            _verifyEmailProvider.SendEmail(user, code);

            return Ok(result);
        }

        [HttpPost, Route("updateEmailVerification")]
        public IHttpActionResult UpdateEmailVerification(VerifyEmail verifyEmail)
        {
            var verify = _verifyEmailProvider.GetVerifyEmail(verifyEmail.UserId ?? 0);

            if(verify != null)
            {
                var code = Helper.GenerateOTP();
                verify.Code = code;
                verify.IssueDate = DateTime.Now;
                verify.ExpiryDate = DateTime.Now.AddDays(1);
                verify.RequestStatusId = 1;
                verify.ModifiedDate = DateTime.Now;

                var result = _verifyEmailProvider.UpdateVerifyEmail(verify);

                var user = _userProvider.GetUserByUserId(verify.UserId ?? 0);

                _verifyEmailProvider.SendEmail(user, code);

                if (result != null)
                    return Ok(result);
            }          

            return InternalServerError(new Exception("You don't have any valid profile"));
        }

        [HttpPost, Route("checkEmailVerification")]
        public IHttpActionResult CheckEmailVerification(CodeVerificationRequest codeVerification)
        {
            var result = _verifyEmailProvider.VerifyEmail(codeVerification.ResetCode);

            if(result != null)
            {
                result.ModifiedDate = DateTime.Now;
                result.RequestStatusId = 3;
                var verifiedResults = _verifyEmailProvider.UpdateVerifyEmail(result);

                var user = _userProvider.GetUserByUserId((int)verifiedResults.UserId);
                var data = new { emailVerificationRequest = verifiedResults, username = user.UserName };
                return Ok(data);
            }

            return InternalServerError(new Exception("You don't have any valid codes right now"));
        }
    }
}