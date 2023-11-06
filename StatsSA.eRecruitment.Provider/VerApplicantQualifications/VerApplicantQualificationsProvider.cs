using StatsSA.eRecruitment.IProvider.VerApplicantQualifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.VerApplicantQualifications
{
    public class VerApplicantQualificationsProvider : IVerApplicantQualificationsProvider
    {
        private IUnitOfWork _unitOfWork;
        public VerApplicantQualificationsProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<VerApplicantQualification> GetVerApplicantQualification(int VerApplicantProfileId)
        {
            return this._unitOfWork.VerApplicantQualificationManager.getVerApplicantQualification(VerApplicantProfileId);
        }
    }
}
