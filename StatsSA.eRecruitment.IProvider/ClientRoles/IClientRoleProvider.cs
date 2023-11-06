using System;
using StatsSA.eRecruitment.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ClientRoles
{
    public interface IClientRoleProvider
    {
        IEnumerable<ClientRole> SaveClientRole(IEnumerable<ClientRole> ClientRoleToAdd, IEnumerable<ClientRole> ClientRoleToRemove);
    }
}
