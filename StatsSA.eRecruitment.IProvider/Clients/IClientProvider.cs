using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.Clients
{
    public interface IClientProvider
    {
        Client AddClient(Client client);
        IEnumerable<Client> GetClients();
    }
}
