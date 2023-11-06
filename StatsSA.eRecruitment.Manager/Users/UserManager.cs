using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.Users;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.Users
{
    public class UserManager : GenericRepository<User>, IUserManager
    {
        private eRecruitmentEntities context;
        public UserManager(eRecruitmentEntities context) : base(context)
        {
            this.context = context;
        }

        public User AddUser(User user)
        {
            return context.Users.Add(user);
        }

        public bool CheckUniqueUsername(string username)
        {
            return context.Users.FirstOrDefault(user => user.UserName.ToLower() == username.ToLower()) != null;
        }
        public bool CheckUniqueEmail(string emailAddress)
        {
            return context.Users.FirstOrDefault(user => user.Email.ToLower() == emailAddress.ToLower()) != null;
        }
        public bool CheckUniqueCell(string cellNumber)
        {
            return context.Users.FirstOrDefault(user => user.Cellphone == cellNumber) != null;
        }

        public bool CheckUniqueIdNumber(string idNumber)
        {
            return context.Users.FirstOrDefault(id => id.IdentityNumber == idNumber) != null;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public User GetUserByUserName(string username)
        {
            return context.Users.FirstOrDefault(user => user.UserName.ToLower() == username.ToLower());
        }
        public User ValidateCredentials(string username, string password)
        {
            return context.Users.FirstOrDefault(user => user.UserName.ToLower() == username.ToLower() && user.Password == password);
        }

        public User GetUserByUserId(int userId)
        {
            return context.Users.FirstOrDefault(user => user.Id == userId);
        }

        public User UpdateUser(User user)
        {
            context.Users.Attach(user);
            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            return user;
        }

        public User GetUserByIDNo(string idNumber)
        {
            return context.Users.Where(user => user.IdentityNumber == idNumber).FirstOrDefault();
        }
        public User GetUserByEmail(string email)
        {
            return context.Users.Where(user => user.Email == email).FirstOrDefault();
        }
        public User GetUserByOtp(string otp)
        {
            return context.Users.Where(user => user.EmailOtp == otp).FirstOrDefault();
        }
        public User UpdateAccount(User user)
        {
            //context.Users.Attach(user);
             var recordInDb = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            if (recordInDb != null)
            {
                recordInDb.Email=user.Email;
                recordInDb.UserName = user.UserName;
                recordInDb.SecondaryEmail = user.SecondaryEmail;
            }
            context.Entry(recordInDb).State = System.Data.Entity.EntityState.Modified;
            return user;
        }
        public User UpdateOtp(User user)
        {
            //context.Users.Attach(user);
            var recordInDb = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            if (recordInDb != null)
            {
                recordInDb.EmailOtp = user.EmailOtp;
            }
            context.Entry(recordInDb).State = System.Data.Entity.EntityState.Modified;
            return user;
        } 
        public User UpdatePrimaryEmail(User user)
        {
            //context.Users.Attach(user);
            var recordInDb = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            if (recordInDb != null)
            {
                recordInDb.Email = user.Email;
                recordInDb.UserName = user.Email;
            }
            context.Entry(recordInDb).State = System.Data.Entity.EntityState.Modified;
            return user;
        }
    }
}
