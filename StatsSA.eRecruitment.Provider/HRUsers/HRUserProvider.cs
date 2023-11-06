using StatsSA.eRecruitment.IProvider.HRUsersProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.HRUsersProvider
{
    public class HRUserProvider : IHRUserProvider
    {
        private IUnitOfWork _unitOfWork;
        public HRUserProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HRUser GetUserById(int hrUserId)
        {
            var user = this._unitOfWork.HRUserManager.GetUserById(hrUserId);
            return user;
        }

        public HRUser GetUserByUserName(string UserName)
        {
            var user = this._unitOfWork.HRUserManager.GetHRUserByUserName(UserName);
            return user;
        }
        public HRUser CheckOtherExistingUsers(HRUser userObj)
        {
            var user = this._unitOfWork.HRUserManager.CheckOtherExistingUsers(userObj);
            return user;
        }

        public void UpdateUserDetails(HRUser UserDetails)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.HRUserManager.UpdateUserDetails(UserDetails);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
        }
        public void UpdateUser(HRUser UserDetails)
        {
            var roles = UserDetails.HRUserRoles;
            UserDetails.HRUserRoles = null;
            var user = this._unitOfWork.HRUserManager.GetHRUserByUserName(UserDetails.HRUserName);
            user.HRUserName = UserDetails.HRUserName;
            user.IsActive = UserDetails.IsActive;
            user.MaintDate = UserDetails.MaintDate;
            user.MaintUser = UserDetails.MaintUser;
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.HRUserManager.UpdateUserDetails(user);
            this._unitOfWork.SaveChanges();
            foreach (HRUserRole role in roles)
            {
                this._unitOfWork.HRUserRoleManager.AddaUserRole(role);
            }
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
        }
        public IEnumerable<HRUser> GetListOfUsers()
        {
            var users = this._unitOfWork.HRUserManager.GetListOfUsers();
            return users;
        }
        public void InsertUser(HRUser UserDetails)
        {
            var roles = UserDetails.HRUserRoles;
            UserDetails.HRUserRoles = null;
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.HRUserManager.InsertUser(UserDetails);
            this._unitOfWork.SaveChanges();
            foreach (HRUserRole role in roles)
            {
                role.HRUserId = UserDetails.HRUserId;
                this._unitOfWork.HRUserRoleManager.AddaUserRole(role);
            }
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
        }

        public HRUser GetActiveHRUserByUserName(string UserName)
        {
            var user = this._unitOfWork.HRUserManager.GetActiveHRUserByUserName(UserName);
            return user;
        }
    }
}
