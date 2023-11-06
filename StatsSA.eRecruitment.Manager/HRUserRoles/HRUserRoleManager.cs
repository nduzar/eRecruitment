using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.HRUserRoles;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.HRUserRoles
{
    public class HRUserRoleManager : GenericRepository<HRUserRole>, IHRUserRoleManager
    {
        private eRecruitmentEntities context;
        public HRUserRoleManager(eRecruitmentEntities context) : base(context)
        {
            this.context = context;
        }
        public IEnumerable<HRUserRole> GetUserRoles(int hrUserId)
        {
            return base.GetMany(userRoles => userRoles.HRUserId == hrUserId);
        }

        public IEnumerable<HRUserRole> GetUserRolesByRoleId(int HrRoleId)
        {
            return base.GetMany(userRole => userRole.HRRoleId == HrRoleId);
        }

        public void RemoveAllUserRoles(int HRUserId)
        {
            base.Delete(role => role.HRUserId == HRUserId);
        }
        public void AddaUserRole(HRUserRole HRUserRole)
        {
            base.Insert(HRUserRole);
        }
    }
}
