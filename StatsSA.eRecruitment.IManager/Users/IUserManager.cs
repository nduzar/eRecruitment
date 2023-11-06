using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Users
{
    public interface IUserManager
    {
        User AddUser(User user);
        User UpdateUser(User user);
        User UpdateAccount(User user);
        User UpdateOtp(User user);
        User UpdatePrimaryEmail(User user);
        bool CheckUniqueUsername(string username);
        bool CheckUniqueEmail(string emailAddress);
        bool CheckUniqueCell(string cellNumber);

        bool CheckUniqueIdNumber(string idNumber);

        IEnumerable<User> GetAllUsers();
        User GetUserByUserName(string username);
        User GetUserByUserId(int userId);
        User ValidateCredentials(string username, string password);

        User GetUserByIDNo(string idNumber);
        User GetUserByEmail(string email);
        User GetUserByOtp(string otp);

    }
}
