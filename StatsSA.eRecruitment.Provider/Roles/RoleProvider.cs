using StatsSA.eRecruitment.IProvider.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.Roles
{
    public class RoleProvider : IRoleProvider
    {
        private IUnitOfWork _unitOfWork;
        public RoleProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Role AddRole(Role role)
        {
            _unitOfWork.RoleManager.AddRole(role);
            _unitOfWork.SaveChanges();
            return role;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _unitOfWork.RoleManager.GetRoles();
        }

        public IEnumerable<Role> RolesByClientId(string clientId)
        {
            return _unitOfWork.RoleManager.RolesByClientId(clientId);
        }
    }
}
