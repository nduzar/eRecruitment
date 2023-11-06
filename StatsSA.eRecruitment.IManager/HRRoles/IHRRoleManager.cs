using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.HRRoles
{
    public interface IHRRoleManager
    {
        HRRole GetRolebyId(int hrRoleId);
        IEnumerable<HRRole> GetAllHRRoles();
    }
}
