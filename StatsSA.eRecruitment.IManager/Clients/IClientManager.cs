using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Clients
{
    public interface IClientManager
    {
        Client AddClient(Client client);
        IEnumerable<Client> GetClients();
    }
}
