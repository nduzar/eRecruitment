using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.HRUsers
{
    public interface IHRUserManager
    {
        HRUser GetHRUserByUserName(string UserName);
        HRUser CheckOtherExistingUsers(HRUser userObj);
        void UpdateUserDetails(HRUser UserDetails);
        HRUser GetUserById(int hrUserId);
        IEnumerable<HRUser> GetListOfUsers();
        void InsertUser(HRUser UserDetails);
        HRUser GetActiveHRUserByUserName(string UserName);

    }
}
