using StatsSA.eRecruitment.IManager.ApplicantReferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.ApplicantReferences
{
    public class ApplicantReferencesManager : IApplicantReferenceManager
    {
        private readonly eRecruitmentEntities _context;
        public ApplicantReferencesManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public void AddApplicantReference(ApplicantReference reference)
        {
            _context.ApplicantReferences.Add(reference);
        }

        public IEnumerable<ApplicantReference> GetApplicantReferences(int ApplicantProfileId)
        {
            return _context.ApplicantReferences.Where(references => references.ApplicantProfileId == ApplicantProfileId).ToList();
        }


        public void RemoveApplicantReferences(int ApplicantReferenceId)
        {
            var refs = _context.ApplicantReferences.Where(reference => reference.ApplicantReferenceId == ApplicantReferenceId).ToList();
            _context.ApplicantReferences.RemoveRange(refs);
        }

        public void UpdateApplicantReference(ApplicantReference reference)
        {
            _context.ApplicantReferences.Attach(reference);
            _context.Entry(reference);
        }
    }

}
