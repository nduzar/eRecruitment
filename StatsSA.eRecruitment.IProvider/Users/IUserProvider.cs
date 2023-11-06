using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.Users
{
    public interface IUserProvider
    {
        User AddUser(User user);
        User UpdateUser(User user);

        bool CheckUniqueUsername(string username);
        IEnumerable<User> GetAllUsers();
        User GetUserByUserName(string username);

        User GetUserByUserId(int userId);

        bool CheckIfEmailValidated(string userName);

        User ValidateCredentials(string username, string password);
        void SaveUserClient(UserClient UserClientToAdd, UserClient UserClientToRemove);
        User GetUserByIDNo(string identityNumber);
        User GetUserByEmail(string email);
        User GetUserByOtp(string otp);
        User UpdateAccount(User user);
        User UpdateOtp(User user);
        User UpdatePrimaryEmail(User user);
    }
}
