using StatsSA.eRecruitment.IManager.VerApplicantQualifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.VerApplicantQualifications
{
    public class VerApplicantQualificationManager : IVerApplicantQualificationManager
    {
        private readonly eRecruitmentEntities _context;
        public VerApplicantQualificationManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public IEnumerable<VerApplicantQualification> getVerApplicantQualification(int VerApplicantProfileId)
        {
            return _context.VerApplicantQualifications.Where(appPr => appPr.VerApplicantProfileId == VerApplicantProfileId).ToList();
        }
        
    }

}
