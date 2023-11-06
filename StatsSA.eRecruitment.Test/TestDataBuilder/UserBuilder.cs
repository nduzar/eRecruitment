using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test.TestDataBuilder
{
    public class UserBuilder
    {
        private string UserName = "SuperUser";
        private DateTime CreateDate = DateTime.Now;
        private string CreatedBy = "TestUser";
        private DateTime? ModifiedDate = null;
        private string ModifiedBy = "TestUser";
        private DateTime? LastLogin = null;
        private bool IsActive = true;
        private string Password = "SuperUser";
        private string Email;
        private string Cellphone;

        public UserBuilder WithUserName(string userName)
        {
            this.UserName = userName;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            this.Email = email;
            return this;
        }
        public UserBuilder WithCellphone(string cellphone)
        {
            this.Cellphone = cellphone;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            this.Password = password;
            return this;
        }

        public User Build()
        {
            return new User
            {
                UserName = this.UserName,
                CreateDate = this.CreateDate,
                CreatedBy = this.CreatedBy,
                ModifiedDate = this.ModifiedDate,
                ModifiedBy = this.ModifiedBy,
                LastLogin = this.LastLogin,
                IsActive = this.IsActive,
                Password = Helper.GetHash(this.Password),
                Email = this.Email,
                Cellphone = this.Cellphone
            };
        }
    }
}
