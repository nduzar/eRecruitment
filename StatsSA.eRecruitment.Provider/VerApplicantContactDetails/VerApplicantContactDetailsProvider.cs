using StatsSA.eRecruitment.IProvider.VerApplicantContactDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.VerApplicantContactDetails
{
    public class VerApplicantContactDetailsProvider : IVerApplicantContactDetailsProvider
    {
        private IUnitOfWork _unitOfWork;
        public VerApplicantContactDetailsProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public VerApplicantContactDetail GetVerApplicantContactDetail(int VerApplicantProfileId)
        {
            return this._unitOfWork.VerApplicantContactDetailsManager.getVerApplicantContactDetail(VerApplicantProfileId);
        }
    }
}
