using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.Clients;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.Clients
{
    public class ClientManager : IClientManager
    {
        private eRecruitmentEntities _context;
        public ClientManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public Client AddClient(Client client)
        {
            return _context.Clients.Add(client);
        }

        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.ToList();
        }
    }
}
