using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.EmailVerification;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace StatsSA.eRecruitment.Provider.EmailVerification
{
    public class VerifyEmailProvider : IVerifyEmailProvider
    {
        private IUnitOfWork _unitOfWork;

        public VerifyEmailProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public VerifyEmail UpdateVerifyEmail(VerifyEmail verifyEmail)
        {            
            _unitOfWork.VerifyEmailManager.UpdateVerifyEmail(verifyEmail);          

            return verifyEmail;
        }

        public VerifyEmail VerifyEmail(string code)
        {
            return _unitOfWork.VerifyEmailManager.VerifyEmail(code);
        }

        public bool CheckIfEmailVerified(int userId)
        {
            return _unitOfWork.VerifyEmailManager.CheckIfEmailVerified(userId);
        }

        public VerifyEmail CheckIfEmailVerified(string userName)
        {
            return _unitOfWork.VerifyEmailManager.VerifyEmail(userName);
        }


        public VerifyEmail InsertVerifyEmail(VerifyEmail verifyEmail)
        {
            return _unitOfWork.VerifyEmailManager.InsertVerifyEmail(verifyEmail);
        }        

        public VerifyEmail GetVerifyEmail(int userId)
        {
            return _unitOfWork.VerifyEmailManager.GetVerifyEmail(userId);
        }

        public void SendEmail(User user, string code)
        {
            _unitOfWork.VerifyEmailManager.SendEmail(user, code);
        }
    }
}
