using StatsSA.eRecruitment.IProvider.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.Auth
{
    public class AuthProvider : IAuthProvider
    {
        private IUnitOfWork _unitOfWork;
        public AuthProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Client FindClient(string clientId)
        {
            return _unitOfWork.AuthManager.FindClient(clientId);
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var existingToken = _unitOfWork.AuthManager.FindRefreshToken(token);
            if (existingToken != null)
            {
                _unitOfWork.AuthManager.RemoveRefreshToken(existingToken);
                _unitOfWork.SaveChanges();
            }
            //_unitOfWork.AuthManager.AddRefreshToken(token);
            //var result = _unitOfWork.SaveChanges();

            return await Task.Run(() =>
            {
                //return result > 0;
                return true;
            });

        }

        public async Task<bool> RemoveRefreshToken(string hashedTokenId)
        {
            RefreshToken refreshToken = await _unitOfWork.AuthManager.FindRefreshToken(hashedTokenId);
            if (refreshToken != null)
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.AuthManager.RemoveRefreshToken(refreshToken);
                var result = await _unitOfWork.SaveChangesAsync() > 0;
                _unitOfWork.CommitTransaction();
                return result;
            }
            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.AuthManager.RemoveRefreshToken(refreshToken);
           var result = await _unitOfWork.SaveChangesAsync() > 0;
            _unitOfWork.CommitTransaction();
            return result;
        }

        public async Task<RefreshToken> FindRefreshToken(string hashedTokenId)
        {
            return await _unitOfWork.AuthManager.FindRefreshToken(hashedTokenId);
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _unitOfWork.AuthManager.GetAllRefreshTokens();
        }

        public bool ValidateUserClient(string userName, string clientId)
        {
            var result = _unitOfWork.AuthManager.ValidateUserClient(userName, clientId);
            return !(result == null);
        }
    }
}
