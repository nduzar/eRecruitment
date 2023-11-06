using StatsSA.eRecruitment.IManager.ApplicantLanguageProficiencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.ApplicantLanguageProficiencies
{
    public class ApplicantLangProficiencyManager : GenericRepository<ApplicantLangProficiency>, IApplicantLangProficiencyManager
    {
        private readonly eRecruitmentEntities _context;
        public ApplicantLangProficiencyManager(eRecruitmentEntities context) : base(context)
        {
            _context = context;
        }

        public ApplicantLangProficiency AddApplicantProficiency(ApplicantLangProficiency proficiency)
        {
            return _context.ApplicantLangProficiencies.Add(proficiency);
        }

        public IEnumerable<ApplicantLangProficiency> GetApplicantProficiencies(int ApplicantProfileId)
        {
            return _context.ApplicantLangProficiencies.Where(prof => prof.ApplicantProfileId == ApplicantProfileId).ToList();
        }

        public void RemoveApplicantProficiency(int ApplicantLangProficiencyId)
        {
            var profs = _context.ApplicantLangProficiencies.Where(prof => prof.ApplicantLangProficiencyId == ApplicantLangProficiencyId).ToList();
            _context.ApplicantLangProficiencies.RemoveRange(profs);
        }

        public void UpdateApplicantProficiency(ApplicantLangProficiency proficiency)
        {
            _context.ApplicantLangProficiencies.Attach(proficiency);
            _context.Entry(proficiency).State = System.Data.Entity.EntityState.Modified;
        }
    }

}
