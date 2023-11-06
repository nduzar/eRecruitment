using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.UserClients
{
    public interface IUserClientManager
    {
        void AddUserClient(IEnumerable<UserClient> UserClientToAdd);
        void RemoveUserClient(IEnumerable<UserClient> UserClientToRemove);

        IEnumerable<UserClient> GetAllUserClient();
    }
}
