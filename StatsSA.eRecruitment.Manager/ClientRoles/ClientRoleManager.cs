using System;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.ClientRoles;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.ClientRoles
{
    public class ClientRoleManager: IClientRoleManager
    {
        private eRecruitmentEntities _context;
        public ClientRoleManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public void AddClientRole(IEnumerable<ClientRole> ClientRoleToAdd)
        {
            _context.ClientRoles.AddRange(ClientRoleToAdd);
        }
        public void RemoveClientRole(IEnumerable<ClientRole>ClientRoleToRemove)
        {
            _context.ClientRoles.RemoveRange(ClientRoleToRemove);
        }
        public IEnumerable<ClientRole> GetClientRoles()
        {
            return _context.ClientRoles.ToList();
        }
    }
}
