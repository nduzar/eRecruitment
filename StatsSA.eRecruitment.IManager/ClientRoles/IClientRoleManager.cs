using System;
using StatsSA.eRecruitment.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ClientRoles
{
    public interface IClientRoleManager
    {
        void AddClientRole(IEnumerable<ClientRole> ClientRoleToAdd);
        void RemoveClientRole(IEnumerable<ClientRole> ClientRoleToRemove);
        IEnumerable<ClientRole> GetClientRoles();
    }
}
