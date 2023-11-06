using StatsSA.eRecruitment.IManager.VerApplicantContactDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.VerApplicantContactDetails
{
    public class VerApplicantContactDetailsManager :  IVerApplicantContactDetailsManager
    {
        private readonly eRecruitmentEntities _context;
        public VerApplicantContactDetailsManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public VerApplicantContactDetail getVerApplicantContactDetail(int verApplicantProfileId)
        {
            return _context.VerApplicantContactDetails.Where(contDet => contDet.VerApplicantProfileId == verApplicantProfileId).FirstOrDefault();
            // return base.Get(contDet => contDet.VerApplicantProfileId == verApplicantProfileId);
        }   
    }

}
