using StatsSA.eRecruitment.IManager.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.Clients;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.Clients
{
    public class ClientProvider : IClientProvider
    {
        private IUnitOfWork _unitOfWork;
        public ClientProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Client AddClient(Client client)
        {
            client.Secret = Helper.GetHash("StatisticsSA@BM");
            client.CreatedBy = "DumisaneK";
            client.ModifiedBy = "DumisaneK";


            var result = _unitOfWork.ClientManager.AddClient(client);
            _unitOfWork.SaveChanges();
            return result;
        }

        public IEnumerable<Client> GetClients()
        {
            return _unitOfWork.ClientManager.GetClients();
        }
    }
}
