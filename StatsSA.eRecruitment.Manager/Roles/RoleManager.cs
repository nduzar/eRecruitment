using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.Roles;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.Roles
{
    public class RoleManager : GenericRepository<Role>,  IRoleManager
    {
        private eRecruitmentEntities _context;
        public RoleManager(eRecruitmentEntities context) : base(context)
        {
            _context = context;
        }

        public Role AddRole(Role role)
        {
            return _context.Roles.Add(role);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public IEnumerable<Role> RolesByClientId(string clientId)
        {
            return _context.Roles.Where(r => r.ClientRoles.Any(rc => rc.ClientId == clientId)).ToList();
        }
    }
}
