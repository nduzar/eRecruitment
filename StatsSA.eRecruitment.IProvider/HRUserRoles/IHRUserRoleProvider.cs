using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.HRUserRolesProvider
{
    public interface IHRUserRoleProvider
    {
        IEnumerable<HRUserRole> GetUserRoles(int hrUserId);

        IEnumerable<HRUserRole> GetUserRolesByRoleId(int HrRoleId);

        List<int> GetUserRolesByRoleId(List<int> hrRoleIds);

        void RemoveAllUserRoles(int HRUserId);
        void AddUserRoles(IEnumerable<HRUserRole> HRUserRoles);
    }
}


