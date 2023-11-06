using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.UserClients
{
    public interface IUserClientProvider
    {
        IEnumerable<UserClient> SaveUserClient(IEnumerable<UserClient> UserClientToAdd, IEnumerable<UserClient> UserClientToRemove);
    }
}
