using StatsSA.eRecruitment.IManager.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.Experiences
{
    public class ApplicantExperienceManager : IApplicantExperienceManager
    {
        private readonly eRecruitmentEntities _context;
        public ApplicantExperienceManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public void AddApplicantExperience(ApplicantExperience experience)
        {
            _context.ApplicantExperiences.Add(experience);
        }

        public IEnumerable<ApplicantExperience> getApplicantExperience(int ApplicantProfileId)
        {
            return _context.ApplicantExperiences.Where(exp => exp.ApplicantProfileId == ApplicantProfileId).ToList();
        }
        public void removeApplicantExperience(int ApplicantExperienceId)
        {
            var exps = _context.ApplicantExperiences.Where(e => e.ApplicantExperienceId == ApplicantExperienceId).ToList();
            _context.ApplicantExperiences.RemoveRange(exps);
        }

        public void UpdateApplicantExperience(ApplicantExperience experience)
        {
            _context.ApplicantExperiences.Attach(experience);
            _context.Entry(experience).State = System.Data.Entity.EntityState.Modified;
        }
    }

}
