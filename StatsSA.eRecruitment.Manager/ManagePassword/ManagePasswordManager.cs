using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.ManagePassword;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.ManagePassword
{
    public class ManagePasswordManager : IManagePasswordManager
    {
        private eRecruitmentEntities _context;
        public ManagePasswordManager(eRecruitmentEntities context)
        {
            this._context = context;
        }
        
        public void LockUnlockPassword(int userId, string tempPassword, bool isActive)
        {
            var user = this._context.Users.FirstOrDefault(u => u.Id == userId);
            user.IsActive = isActive;
            user.Password = Helper.GetHash(tempPassword).ToString();
            _context.SaveChanges();
        }

    }
}
