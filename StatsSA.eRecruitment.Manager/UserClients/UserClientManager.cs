using StatsSA.eRecruitment.IManager.UserClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.UserClients
{
    public class UserClientManager : GenericRepository<UserClient>, IUserClientManager
    {
        private eRecruitmentEntities _context;
        public UserClientManager(eRecruitmentEntities context) : base(context)
        {
            _context = context;
        }
        public void AddUserClient(IEnumerable<UserClient> UserClientToAdd)
        {
            _context.UserClients.AddRange(UserClientToAdd);
        }

        public IEnumerable<UserClient> GetAllUserClient()
        {
            return _context.UserClients.ToList();
        }

        public void RemoveUserClient(IEnumerable<UserClient> UserClientToRemove)
        {
            _context.UserClients.RemoveRange(UserClientToRemove);
        }
    }
}
