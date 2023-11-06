using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Auth
{
    public interface IAuthManager
    {
        Client FindClient(string clientId);
        void AddRefreshToken(RefreshToken token);
        // void RemoveRefreshToken(string hashedTokenId);
        void RemoveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> FindRefreshToken(string hashedTokenId);
        RefreshToken FindRefreshToken(RefreshToken refreshToken);      
        List<RefreshToken> GetAllRefreshTokens();
        UserClient ValidateUserClient(string userName, string clientId);
    }
}
