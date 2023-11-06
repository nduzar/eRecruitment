using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.EmailVerification
{
    public interface IVerifyEmailManager
    {
        VerifyEmail VerifyEmail(string code);

        bool CheckIfEmailVerified(int userId);

        VerifyEmail InsertVerifyEmail(VerifyEmail verifyEmail);

        VerifyEmail GetVerifyEmail(int userId);

        void SendEmail(User user, string code);

        VerifyEmail UpdateVerifyEmail(VerifyEmail verifyEmail);
    }
}
