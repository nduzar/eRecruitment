using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.HRUserRoles
{
    public interface IHRUserRoleManager
    {
        IEnumerable<HRUserRole> GetUserRoles(int hrUserId);
        IEnumerable<HRUserRole> GetUserRolesByRoleId(int HrRoleId);
        void RemoveAllUserRoles(int HRUserId);
        void AddaUserRole(HRUserRole HRUserRole);
    }
}
