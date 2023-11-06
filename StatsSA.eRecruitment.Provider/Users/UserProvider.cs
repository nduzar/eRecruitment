using StatsSA.eRecruitment.IProvider.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.Users
{
    public class UserProvider : IUserProvider
    {
        private IUnitOfWork _unitOfWork;
        public UserProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public User AddUser(User user)
        {
            user.CreateDate = DateTime.Now;
            user.CreatedBy = user.Email;
            user.ModifiedBy = user.Email;
            user.ModifiedDate = DateTime.Now;
            user.Email = user.Email.ToLower();
            user.IdentityNumber = user.IdentityNumber;
            user.SecondaryEmail = user.SecondaryEmail.ToLower(); 

            var cellExists = _unitOfWork.UserManager.CheckUniqueCell(user.Cellphone);
            var emailExists = _unitOfWork.UserManager.CheckUniqueEmail(user.Email);
            var idNumberExists = _unitOfWork.UserManager.CheckUniqueIdNumber(user.IdentityNumber);
            if (user.Email == user.SecondaryEmail)
            {
                Exception ex = new Exception("The Primary Email '" + user.Email + "' and SecondaryEmail '" + user.SecondaryEmail + "'  cannot be same.");
                throw ex;
            }
            if (emailExists)
            {
                Exception ex = new Exception("The email '" + user.Email + "' already exists on the system, please log in. Alternatively use the password reset function to reset your password and obtain access to your account.");
                throw ex;
            }
            if (idNumberExists)
            {
                Exception ex = new Exception("The ID number '" + user.IdentityNumber + "' already exists on the system, please log in. Alternatively use the password reset function to reset your password and obtain access to your account.");
                throw ex;
            }
            if (cellExists)
            {
                Exception ex = new Exception("The cellphone number '" + user.Cellphone + "' already exists on the system, please log in. Alternatively use the password reset function to reset your password and obtain access to your account.");
                throw ex;
            }
            

            if(!emailExists && !cellExists)
            {
                user.Password = Helper.GetHash(user.Password);
                var addedUser = _unitOfWork.UserManager.AddUser(user);
                _unitOfWork.SaveChanges();

                var code = Helper.GenerateOTP();
                var verifyEmail = new VerifyEmail
                {
                    Code = code,
                    IssueDate = DateTime.Now,
                    UserId = addedUser.Id,
                    ExpiryDate = DateTime.Now.AddDays(1),
                    RequestStatusId = 1,
                    ModifiedDate = DateTime.Now
                };

                _unitOfWork.VerifyEmailManager.InsertVerifyEmail(verifyEmail);

                _unitOfWork.SaveChanges();

                _unitOfWork.VerifyEmailManager.SendEmail(addedUser, code);
                return user;
            }

            return null;             
        }

        public bool CheckUniqueUsername(string username)
        {
            return _unitOfWork.UserManager.CheckUniqueUsername(username);
        }


        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserManager.GetAllUsers();
        }

        public User GetUserByUserName(string username)
        {
            return _unitOfWork.UserManager.GetUserByUserName(username);
        }
        public User GetUserByUserId(int userId)
        {
            return _unitOfWork.UserManager.GetUserByUserId(userId);
        }

        public void SaveUserClient(UserClient UserClientToAdd, UserClient UserClientToRemove)
        {

        }

        public User ValidateCredentials(string username, string password)
        {
            return _unitOfWork.UserManager.ValidateCredentials(username, Helper.GetHash(password));
        }

        public bool CheckIfEmailValidated(string userName)
        {
            bool emailValidated = false;
            var user = _unitOfWork.UserManager.GetUserByUserName(userName);
            if(user != null)
            {
                emailValidated = _unitOfWork.VerifyEmailManager.CheckIfEmailVerified(user.Id);
            }
            return emailValidated;
        }

        public User UpdateUser(User user)
        {
            var result = _unitOfWork.UserManager.UpdateUser(user);
            _unitOfWork.SaveChanges();
            return result;
        }

        public User GetUserByIDNo(string identityNumber)
        {
            return _unitOfWork.UserManager.GetUserByIDNo(identityNumber);
        }
        public User UpdateAccount(User user)
        {
            var result = _unitOfWork.UserManager.UpdateAccount(user);
            _unitOfWork.SaveChanges();
            return result;
        }
        public User GetUserByEmail(string email)
        {
            return _unitOfWork.UserManager.GetUserByEmail(email);
        }
        public User GetUserByOtp(string otp)
        {
            return _unitOfWork.UserManager.GetUserByOtp(otp);
        }
        public User UpdateOtp(User user)
        {
            var result = _unitOfWork.UserManager.UpdateOtp(user);
            _unitOfWork.SaveChanges();
            return result;
        }
        public User UpdatePrimaryEmail(User user)
        {
            var result = _unitOfWork.UserManager.UpdatePrimaryEmail(user);
            _unitOfWork.SaveChanges();
            return result;
        }
    }
}
