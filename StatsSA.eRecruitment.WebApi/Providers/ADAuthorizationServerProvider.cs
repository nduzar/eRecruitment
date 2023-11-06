using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Entities.Enums;
using StatsSA.eRecruitment.IProvider.Auth;
using StatsSA.eRecruitment.IProvider.Users;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace StatsSA.eRecruitment.WebApi.Providers
{
    public class ADAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        //private IAuthProvider _authProvider;
        //private IUserProvider _userProvider;
        public ADAuthorizationServerProvider()
        {
            //_authProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuthProvider)) as IAuthProvider;
            //_userProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserProvider)) as IUserProvider;
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var _authProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuthProvider)) as IAuthProvider;
            var _userProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserProvider)) as IUserProvider;

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.Validated();
                return Task.FromResult<object>(null);
            }


            client = _authProvider.FindClient(context.ClientId);

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            var clientApplicationType = (ApplicationTypes)client.ApplicationType;

            if (clientApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null)
            {
                allowedOrigin = "*";
            }

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            User currentUser = new User();
            currentUser = await ValidateUser(context.UserName, context.Password);

            if (currentUser == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }
            else
            {
                bool emailValidated = CheckIfEmailValidated(context.UserName);
                if (!emailValidated)
                {
                    context.SetError("invalid_grant", "The email was not verified, Click on the Verify Email button to verify your email");
                    return;
                }
            }

            //var canUserAccessApp = _authProvider.ValidateUserClient(context.UserName, context.ClientId);

            //if (!canUserAccessApp)
            //{
            //    context.SetError("invalid_grant", "User " + context.UserName + " does not have permission to use this application.");
            //    return;
            //}

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("id", currentUser.Id.ToString()));
            identity.AddClaim(new Claim("firstname", currentUser.FirstName));
            identity.AddClaim(new Claim("lastname", currentUser.Surname));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    },
                    {
                        "fullname", currentUser.FirstName + " "+currentUser.Surname
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        private async Task<User> ValidateUser(string username, string password)
        {
            var _authProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuthProvider)) as IAuthProvider;
            var _userProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserProvider)) as IUserProvider;

            return await Task.Run(() =>
            {
                User currentUser = null;
                //using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "statssa.gov.za"))
                //using (PrincipalContext pc = new PrincipalContext(ContextType.Machine, null))
                //{
                //   isValid = pc.ValidateCredentials(username, password);                   
                //}
                currentUser = _userProvider.ValidateCredentials(username, password);
                return currentUser;
            });
        }

        private bool CheckIfEmailValidated(string username)
        {
            var _userProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserProvider)) as IUserProvider;

            return _userProvider.CheckIfEmailValidated(username);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

    }
}