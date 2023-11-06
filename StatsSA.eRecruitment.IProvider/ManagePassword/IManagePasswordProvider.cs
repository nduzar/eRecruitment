using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ManagePassword
{
    public interface IManagePasswordProvider
    {
        ApplicantProfile ForgotPassword(string idnumber);
        PasswordResetRequest ValidateResetCode(string resetCode);
        void ResetPassword(string newPassword, int userId);
        void PasswordReset(string templatePath, int userId, string email, string cellnumber, string method, string fullname, string resetPasswordPath);


    }
}