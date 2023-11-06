using StatsSA.eRecruitment.IManager.VerApplicantReferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.VerApplicantReferences
{
    public class VerApplicantReferencesManager : IVerApplicantReferencesManager
    {
        private readonly eRecruitmentEntities _context;
        public VerApplicantReferencesManager(eRecruitmentEntities context) 
        {
            _context = context;
        }
        public IEnumerable<VerApplicantReference> GetVerApplicantReferences(int VerApplicantProfileId)
        {
            return _context.VerApplicantReferences.Where(references => references.VerApplicantProfileId == VerApplicantProfileId).ToList();
        }
    }
  
}
