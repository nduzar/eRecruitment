using StatsSA.eRecruitment.IManager.VerApplicantLangProficiencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.VerApplicantLangProficiencies
{
    public class VerApplicantLangProficiencyManager :IVerApplicantLangProficiencyManager
    {
        private readonly eRecruitmentEntities _context;
        public VerApplicantLangProficiencyManager(eRecruitmentEntities context)
        {
            _context = context;
        }


        public IEnumerable<VerApplicantLangProficiency> GetVerApplicantProficiencies(int VerApplicantProfileId)
        {
            return _context.VerApplicantLangProficiencies.Where(prof => prof.VerApplicantProfileId == VerApplicantProfileId).ToList();
            // return base.GetMany(prof => prof.VerApplicantProfileId == VerApplicantProfileId);
        }
    }

}
