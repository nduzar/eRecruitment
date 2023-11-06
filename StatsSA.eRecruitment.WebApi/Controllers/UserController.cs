using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.EmailVerification;
using StatsSA.eRecruitment.IProvider.Users;
using StatsSA.eRecruitment.WebApi.Extensions;
using StatsSA.eRecruitment.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{

    [RoutePrefix("api/user")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private IUserProvider _userProvider;
        private IVerifyEmailProvider _verifyEmailProvider;
        public UserController(IUserProvider userProvider, IVerifyEmailProvider verifyEmailProvider)
        {
            _userProvider = userProvider;
            _verifyEmailProvider = verifyEmailProvider;
        }

        [HttpPost, Route("adduser")]
        public IHttpActionResult AddUser(User user)
        {
            var result = _userProvider.AddUser(user);
            result.Password = string.Empty;
            result.Cellphone = string.Empty;
            return Ok(result);
        }

        [HttpPost, Route("updateuser")]
        public IHttpActionResult UpdateUser(User user)
        {
            var MaintUser = "AllcationFailed";
            try
            {
                MaintUser = RequestContext.Principal.Identity.Name;
            }
            catch
            {
                MaintUser = user.UserName;
            }
            foreach (var appProfile in user.ApplicantProfiles)
            {
                appProfile.MaintUser = MaintUser;
            }
            user.CreatedBy = MaintUser;
            user.ModifiedBy = MaintUser;

            var result = _userProvider.AddUser(user);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("checkUniqueUsername")]
        public IHttpActionResult CheckUniqueUsername(CheckUniqueUsernameRequest request)
        { 
            var result = _userProvider.CheckUniqueUsername(request.Username);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("getallusers")]
        public IHttpActionResult GetAllUsers()
        {
            var result = _userProvider.GetAllUsers();
            return Ok(result);
        }

        [HttpPost, Route("getUserByUserId")]
        public IHttpActionResult GetUserByUserId()
        {
            var userid = User.GetUserId();
            var result = _userProvider.GetUserByUserId(userid);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("saveuserclient")]
        public IHttpActionResult SaveUserClient(UserClientRequest userClientRequest)
        {
            var toRemove = userClientRequest.UserClientToRemove;

            return Ok();
        }
    }
}
