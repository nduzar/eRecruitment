using StatsSA.eRecruitment.IProvider.VerApplicantProhibitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.VerApplicantProhibitions
{
    public class VerApplicantProhibitionProvider : IVerApplicantProhibitionProvider
    {
        private IUnitOfWork _unitOfWork;
        public VerApplicantProhibitionProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public VerApplicantPubServProhibition GetVerApplicantProhibition(int VerApplicantProfileId)
        {
            return this._unitOfWork.VerApplicantProhibitionManager.getVerApplicantProhibition(VerApplicantProfileId);
        }
    }
}
