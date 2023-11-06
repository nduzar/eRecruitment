using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.HRUsersProvider
{
    public interface IHRUserProvider
    {
        HRUser GetUserByUserName(string UserName);
        void UpdateUserDetails(HRUser UserDetails);
        void UpdateUser(HRUser UserDetails);
        HRUser GetUserById(int hrUserId);
        IEnumerable<HRUser> GetListOfUsers();
        void InsertUser(HRUser UserDetails);
        HRUser CheckOtherExistingUsers(HRUser userObj);
        HRUser GetActiveHRUserByUserName(string UserName);
    }
}
