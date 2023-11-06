using StatsSA.eRecruitment.IManager.VerApplicantExperiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.VerApplicantExperiences
{
    public class VerApplicantExperienceManager :IVerApplicantExperienceManager
    {
        private readonly eRecruitmentEntities _context;
        public VerApplicantExperienceManager(eRecruitmentEntities context) 
        {
            _context = context;
        }
        IEnumerable<VerApplicantExperience> IVerApplicantExperienceManager.getApplicantExperience(int VerApplicantProfileId)
        {
            return _context.VerApplicantExperiences.Where(exp => exp.VerApplicantProfileId == VerApplicantProfileId).ToList();
           // return base.GetMany(exp => exp.VerApplicantProfileId == VerApplicantProfileId);
        } 
    }

}
