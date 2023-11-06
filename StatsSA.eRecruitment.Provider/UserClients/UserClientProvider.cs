using StatsSA.eRecruitment.IProvider.UserClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.UserClients
{
    public class UserClientProvider : IUserClientProvider
    {
        private IUnitOfWork _unitOfWork;
        public UserClientProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<UserClient> SaveUserClient(IEnumerable<UserClient> UserClientToAdd, IEnumerable<UserClient> UserClientToRemove)
        {
            _unitOfWork.BeginTransaction();
            if (UserClientToAdd.Any())
            {
                _unitOfWork.UserClientManager.AddUserClient(UserClientToAdd);

            }
            if (UserClientToRemove.Any())
            {
                _unitOfWork.UserClientManager.RemoveUserClient(UserClientToRemove);
            }
            _unitOfWork.SaveChanges();
            _unitOfWork.CommitTransaction();

            return _unitOfWork.UserClientManager.GetAllUserClient();
        }
    }
}
