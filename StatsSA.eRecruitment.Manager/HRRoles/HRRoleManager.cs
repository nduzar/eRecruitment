using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.HRRoles;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.HRRoles
{
    public class HRRoleManager : GenericRepository<HRRole>, IHRRoleManager
    {
        private eRecruitmentEntities context;
        public HRRoleManager(eRecruitmentEntities context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<HRRole> GetAllHRRoles()
        {
            return base.GetAll();
        }

        public HRRole GetRolebyId(int hrRoleId)
        {
            return base.Get(role => role.HRUserRoleId == hrRoleId);
        }
    }
}
