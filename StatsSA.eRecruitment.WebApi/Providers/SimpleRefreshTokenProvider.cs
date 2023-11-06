using Microsoft.Owin.Security.Infrastructure;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.Auth;
using StatsSA.eRecruitment.IProvider.Users;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace StatsSA.eRecruitment.WebApi.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {

        //private IAuthProvider _authProvider;
        //private IUserProvider _userProvider;
        public SimpleRefreshTokenProvider()
        {
           // _authProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuthProvider)) as IAuthProvider;
           // _userProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserProvider)) as IUserProvider;
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var _authProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuthProvider)) as IAuthProvider;
            var _userProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserProvider)) as IUserProvider;

            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var user = _userProvider.GetUserByUserName(context.Ticket.Identity.Name);

            var token = new RefreshToken()
            {
                TokenId = Helper.GetHash(refreshTokenId),
                ClientId = clientid,
                UserId = user.Id,
                IssuedUTC = DateTime.Now,
                ExpiresUTC = DateTime.Now.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUTC;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUTC;

            token.ProtectedTicket = context.SerializeTicket();
            context.SetToken(context.SerializeTicket());

            //var result = await _authProvider.AddRefreshToken(token);

            //if (result)
            //if(true)
            //{
            //    context.SetToken(refreshTokenId);
            //}
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {


            var _authProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuthProvider)) as IAuthProvider;
            var _userProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserProvider)) as IUserProvider;

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Helper.GetHash(context.Token);

            var refreshToken = await _authProvider.FindRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                var result = await _authProvider.RemoveRefreshToken(hashedTokenId);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}