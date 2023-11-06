using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.Auth
{
    public class AuthManager : IAuthManager
    {
        private eRecruitmentEntities _context;
        public AuthManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public Client FindClient(string clientId)
        {
            var client = _context.Clients.Find(clientId);
            return client;
        }

        public void AddRefreshToken(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
        }

       // public void RemoveRefreshToken(string refreshTokenId)
        //{
            //var refreshToken = _context.RefreshTokens.Find(refreshTokenId);

            //if (refreshToken != null)
            //{
            //    _context.RefreshTokens.Remove(refreshToken);
            //    return _context.SaveChanges() > 0;
            //}

            //return false;
          //  _context.RefreshTokens.Remove(refreshToken);
       // }

        public void RemoveRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        { 
            var token = _context.RefreshTokens.FirstOrDefault(t => t.TokenId == refreshTokenId);
            if(token != null)
            {
                return await _context.RefreshTokens.FindAsync(token.Id);
            }
            return null;
        }

        public RefreshToken FindRefreshToken(RefreshToken refreshToken)
        {
            return _context.RefreshTokens.Where(r => r.UserId == refreshToken.UserId && r.ClientId == refreshToken.ClientId).FirstOrDefault();
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _context.RefreshTokens.ToList();
        }

        public UserClient ValidateUserClient(string userName, string clientId)
        {
            return _context.UserClients.FirstOrDefault(uc => uc.User.UserName == userName && uc.ClientId == clientId);
        }
    }
}
