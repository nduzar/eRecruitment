using StatsSA.eRecruitment.IManager.VerApplicantProhibition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.VerApplicantProhibition
{
    public class VerApplicantProhibitionManager : IVerApplicantProhibitionManager
    {
        private readonly eRecruitmentEntities _context;
        public VerApplicantProhibitionManager(eRecruitmentEntities context) 
        {
            _context = context;
        }

        public VerApplicantPubServProhibition getVerApplicantProhibition(int VerApplicantProfileId)
        {
            return _context.VerApplicantPubServProhibitions.Where(prohib => prohib.VerApplicantProfileId == VerApplicantProfileId).FirstOrDefault();
            // return base.Get(prohib => prohib.VerApplicantProfileId == VerApplicantProfileId);
        }
    }

}
