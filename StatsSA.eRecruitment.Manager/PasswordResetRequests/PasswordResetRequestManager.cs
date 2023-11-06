using StatsSA.eRecruitment.IManager.PasswordResetRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.PasswordResetRequests
{
    public class PasswordResetRequestManager : IPasswordResetRequestManager
    {
        private eRecruitmentEntities _context;
        public PasswordResetRequestManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public PasswordResetRequest AddPasswordResetRequest(PasswordResetRequest passwordResetRequest)
        {
            return _context.PasswordResetRequests.Add(passwordResetRequest);
        }

        public PasswordResetRequest GetPasswordResetRequestByUserId(int userId)
        {
            return _context.PasswordResetRequests.FirstOrDefault(r => r.UserId == userId && r.PasswordRequestStatusId == 1);
        }

        public void UpdatePasswordResetRequest(PasswordResetRequest passwordResetRequest)
        {
            _context.PasswordResetRequests.Attach(passwordResetRequest);
            _context.Entry(passwordResetRequest).State = System.Data.Entity.EntityState.Modified;
        }

        public PasswordResetRequest GetPasswordResetRequestByResetCode(string resetCode)
        {
            return _context.PasswordResetRequests.FirstOrDefault(p => p.ResetCode == resetCode && p.PasswordRequestStatusId == 1);
        }
    }
}
