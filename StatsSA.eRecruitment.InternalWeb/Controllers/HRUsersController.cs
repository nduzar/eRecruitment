using StatsSA.eRecruitment.IProvider.HRUsersProvider;
using StatsSA.eRecruitment.IProvider.HRUserRolesProvider;
using StatsSA.eRecruitment.IProvider.HRRoles;
using StatsSA.eRecruitment.InternalWeb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Entities.Enums;

namespace StatsSA.eRecruitment.InternalWeb.Controllers
{
    [Authorize, RoutePrefix("api/users")]
    public class HRUsersController : ApiController
    {
        private IHRUserProvider _hrUserProvider;
        private IHRUserRoleProvider _hrUserRoleProvider;
        private IHRRoleProvider _hrRoleProvider;
        public HRUsersController(IHRUserProvider hrUserProvider, IHRUserRoleProvider hrUserRoleProvider, IHRRoleProvider hrRoleProvider)
        {
            _hrUserProvider = hrUserProvider;
            _hrUserRoleProvider = hrUserRoleProvider;
            _hrRoleProvider = hrRoleProvider;
        }

        [HttpPost]
        [Route("getauthorisation")]
        public IHttpActionResult GetAuthorisation()
        {
            LoginResponse response = new LoginResponse();
            try
            {
                string UserName = User.Identity.Name;
                UserName = UserName.Replace("TREASURY\\", "");
                response.user = _hrUserProvider.GetActiveHRUserByUserName(UserName);
                if (response.user != null)
                {
                    response.user.LastLogin = DateTime.Now;
                    _hrUserProvider.UpdateUserDetails(response.user);
                    var roles = _hrUserRoleProvider.GetUserRoles(response.user.HRUserId);
                    foreach (var role in roles)
                    {
                        response.user.HRUserRoles.Add(role);
                    }
                    response.loggedIn = true;
                }
                else
                {
                    Exception noActiveUserFound = new Exception("Your user details could not be found. Please contact the system administrator for further assistance.");
                    throw (noActiveUserFound);
                }
            }
            catch(Exception Ex)
            {
                response.loggedIn = false;
                response.loggedInMessage = Ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("getallusers")]
        public IHttpActionResult GetAllUsers()
        {
            var ListOfUsers = this._hrUserProvider.GetListOfUsers();
            foreach(HRUser user in ListOfUsers)
            {
                var userRoles = _hrUserRoleProvider.GetUserRoles(user.HRUserId);
                foreach(HRUserRole role in userRoles)
                {
                    role.HRRole = _hrRoleProvider.GetRolebyId(role.HRRoleId);
                    user.HRUserRoles.Add(role);
                }
            }
            return Ok(ListOfUsers);
        }

        [HttpPost]
        [Route("updateUser")]
        public IHttpActionResult UpdateUser(HRUser objUser)
        {
            var response = new userProfileResponse();
            response.userObject = objUser;
            string RoleString = string.Empty;
            try
            {
                var testUserExists = _hrUserProvider.CheckOtherExistingUsers(objUser);
                if (testUserExists == null)
                {
                    _hrUserRoleProvider.RemoveAllUserRoles(objUser.HRUserId);
                    foreach (HRUserRole role in objUser.HRUserRoles)
                    {
                        var hrRole = _hrRoleProvider.GetRolebyId(role.HRRoleId);
                        RoleString += "<li>" + hrRole.HRUserRoleDesc + "</li>";
                        role.HRRole = null;
                        role.MaintDate = DateTime.Now;
                        role.MaintUser = User.Identity.Name;
                    }
                    objUser.MaintDate = DateTime.Now;
                    objUser.MaintUser = User.Identity.Name;
                    _hrUserProvider.UpdateUser(objUser);
                }
                else
                {
                    throw new Exception("The username \"" + objUser.HRUserName + "\" already exists for another user.");
                }
                var activeString = "";
                if (objUser.IsActive)
                {
                    activeString = "Active";
                }
                else
                {
                    activeString = "String";
                }
                EmailExtension mailer = new EmailExtension();
                var searchUser = objUser.HRUserName;
                var userEmail = UserExtensions.GetUserEmail(searchUser);
                if (userEmail != null)
                {
                    mailer.Recipients.Add(userEmail);
                    mailer.useHtml = true;
                    mailer.EmailSubject = "eRecruitment - User Account Management";
                    mailer.EmailContent = "<p>This confirms that your user account has been updated.</p><br />";
                    mailer.EmailContent += "Username: " + objUser.HRUserName + "<br />Account status: " + activeString + "<br /> Roles: ";
                    mailer.EmailContent += "<ul>" + RoleString + "</ul>";
                    mailer.Send();
                }
                response.status = true;
                response.userObject = objUser;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.statusMessage = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("insertUser")]
        public IHttpActionResult InsertUser(HRUser objUser)
        {
            var response = new userProfileResponse();
            response.userObject = objUser;
            string RoleString = string.Empty;
            try
            {
                var searchUser = objUser.HRUserName;
                var userEmail = UserExtensions.GetUserEmail(searchUser);

                var testUserExists = _hrUserProvider.GetUserByUserName(objUser.HRUserName);
                if(testUserExists == null)
                {
                    foreach (HRUserRole role in objUser.HRUserRoles)
                    {
                        var hrRole = _hrRoleProvider.GetRolebyId(role.HRRoleId);
                        RoleString += "<li>" + hrRole.HRUserRoleDesc + "</li>";
                        role.HRRole = null;
                        role.MaintDate = DateTime.Now;
                        role.MaintUser = User.Identity.Name;
                    }
                    objUser.CreatedBy = User.Identity.Name;
                    objUser.CreatedDate = DateTime.Now;
                    objUser.MaintDate = DateTime.Now;
                    objUser.MaintUser = User.Identity.Name;
                    _hrUserProvider.InsertUser(objUser);
                    response.status = true;
                }
                else
                {
                    throw new Exception("The username \"" + objUser.HRUserName + "\" already exists on this system.");
                }
                var activeString = "";
                if (objUser.IsActive)
                {
                    activeString = "Active";
                }
                else
                {
                    activeString = "String";
                }
                EmailExtension mailer = new EmailExtension();                
                if (userEmail != null)
                {
                    mailer.Recipients.Add(userEmail);
                    mailer.useHtml = true;
                    mailer.EmailSubject = "eRecruitment - User Account Management";
                    mailer.EmailContent = "<p>This confirms that you have been granted access to the eRecruitment - Internal application.</p><br />";
                    mailer.EmailContent += "Username: " + objUser.HRUserName + "<br />Account status: " + activeString + "<br /> Roles: ";
                    mailer.EmailContent += "<ul>" + RoleString + "</ul>";
                    mailer.Send();
                }
            }
            catch(Exception ex)
            {
                response.status = false;
                response.statusMessage = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("searchUser")]
        public IHttpActionResult SearchUser(searchObject searchString)
        {
            var foundUsers = UserExtensions.SearchUsers(searchString.partialString);
            return Ok(foundUsers);
        }


    }
    public class LoginResponse
    {
        public bool loggedIn { get; set; }
        public string loggedInMessage { get; set; }
        public HRUser user { get; set; }
    }
    public class userProfileResponse
    {
        public bool status { get; set; }
        public string statusMessage { get; set; }
        public HRUser userObject { get; set; }
    }
    public class searchObject
    {
        public string partialString { get; set; }
    }

}
