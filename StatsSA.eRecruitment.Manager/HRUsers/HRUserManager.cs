using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.HRUsers;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.HRUsers
{
    public class HRUserManager : GenericRepository<HRUser>, IHRUserManager
    {
        private eRecruitmentEntities context;
        public HRUserManager(eRecruitmentEntities context) : base(context)
        {
            this.context = context;
        }

        public HRUser GetActiveHRUserByUserName(string UserName)
        {
            return base.Get(user => user.HRUserName == UserName && user.IsActive == true);
        }
        public HRUser GetHRUserByUserName(string UserName)
        {
            return base.Get(user => user.HRUserName == UserName);
        }
        public HRUser CheckOtherExistingUsers(HRUser userObj)
        {
            var checkuser = base.Get(user => user.HRUserName == userObj.HRUserName && user.HRUserId != userObj.HRUserId);
            return checkuser;
        }
        public void UpdateUserDetails(HRUser UserDetails)
        {
            base.Update(UserDetails);
        }
        public HRUser GetUserById(int hrUserId)
        {
            return base.Get(user => user.HRUserId == hrUserId);
        }
        public IEnumerable<HRUser> GetListOfUsers()
        {
            return base.GetAll();
        }
        public void InsertUser(HRUser UserDetails)
        {
            base.Insert(UserDetails);
        }
    }
}
