using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.PasswordResetRequests
{
    public interface IPasswordResetRequestManager
    {
        PasswordResetRequest AddPasswordResetRequest(PasswordResetRequest passwordResetRequest);
        PasswordResetRequest GetPasswordResetRequestByUserId(int userId);
        void UpdatePasswordResetRequest(PasswordResetRequest passwordResetRequest);
        PasswordResetRequest GetPasswordResetRequestByResetCode(string resetCode);
    }
}
