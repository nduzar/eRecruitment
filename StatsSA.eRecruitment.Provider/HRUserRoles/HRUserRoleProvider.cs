using StatsSA.eRecruitment.IProvider.HRUserRolesProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.HRUserRolesProvider
{
    public class HRUserRoleProvider : IHRUserRoleProvider
    {
        private IUnitOfWork _unitOfWork;
        public HRUserRoleProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HRUserRole> GetUserRoles(int hrUserId)
        {
            return this._unitOfWork.HRUserRoleManager.GetUserRoles(hrUserId);
        }

        public IEnumerable<HRUserRole> GetUserRolesByRoleId(int HrRoleId)
        {
            return this._unitOfWork.HRUserRoleManager.GetUserRolesByRoleId(HrRoleId);
        }

        public List<int> GetUserRolesByRoleId(List<int> HrRoleIds)
        {
            List<int> hRUserRoles = new List<int>();

            foreach (var item in HrRoleIds)
            {
                var users = this._unitOfWork.HRUserRoleManager.GetUserRolesByRoleId(item).ToList();
                foreach (var user in users)
                {
                    if (!hRUserRoles.Contains(user.HRUserId))
                    {
                        hRUserRoles.Add(user.HRUserId);
                    }
                }
            }
            return hRUserRoles.ToList();
        }

        public void RemoveAllUserRoles(int HRUserId)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.HRUserRoleManager.RemoveAllUserRoles(HRUserId);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
        }
        public void AddUserRoles(IEnumerable<HRUserRole> HRUserRoles)
        {
            this._unitOfWork.BeginTransaction();
            foreach(var role in HRUserRoles)
            {
                this._unitOfWork.HRUserRoleManager.AddaUserRole(role);
            }
           
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
        }
    }
}
