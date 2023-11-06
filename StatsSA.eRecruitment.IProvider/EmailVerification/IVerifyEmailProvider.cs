using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.IProvider.EmailVerification
{
    public interface IVerifyEmailProvider
    {
        VerifyEmail VerifyEmail(string code);

        VerifyEmail GetVerifyEmail(int userId);

        bool CheckIfEmailVerified(int id);

        VerifyEmail InsertVerifyEmail(VerifyEmail verifyEmail);

        void SendEmail(User user, string code);

        VerifyEmail UpdateVerifyEmail(VerifyEmail verifyEmail);

    }
}
