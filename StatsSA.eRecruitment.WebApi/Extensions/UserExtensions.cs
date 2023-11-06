using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Extensions
{
    public static class UserExtensions
    {
        public static int GetUserId(this IPrincipal User)
        {
            var identity = (ClaimsIdentity)User.Identity;
            int userId=  -1;
            if (identity.Claims.Any())
            {
                userId = int.Parse(identity.Claims.Where(c => c.Type == "id").Single().Value);
            }
            return userId;
        }

        public static string GetUserLastname(this IPrincipal User)
        {
            var identity = (ClaimsIdentity)User.Identity;
            string lastname = "";
            if (identity.Claims.Any())
            {
                lastname = identity.Claims.Where(c => c.Type == "lastname").Single().Value;
            }
            return lastname;
        }

        public static string GetUserFirstname(this IPrincipal User)
        {
            var identity = (ClaimsIdentity)User.Identity;
            string firstname  = "";
            if (identity.Claims.Any())
            {
                firstname = identity.Claims.Where(c => c.Type == "firstname").Single().Value;
            }
            return firstname;
        }
    }
}