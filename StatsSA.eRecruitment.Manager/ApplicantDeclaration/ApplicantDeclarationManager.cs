using StatsSA.eRecruitment.IManager.ApplicantDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.ApplicantDeclarations
{
    public class ApplicantDeclarationManager : IApplicantDeclarationManager
    {
        private readonly eRecruitmentEntities _context;
        public ApplicantDeclarationManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public void AddApplicantDeclaration(Entities.ApplicantDeclaration declaration)
        {
            _context.ApplicantDeclarations.Add(declaration);
        }

        public Entities.ApplicantDeclaration getApplicantDeclaration(int applicantProfileId)
        {
            return _context.ApplicantDeclarations.FirstOrDefault(exp => exp.ApplicantProfileId == applicantProfileId);
        }

        public ApplicantDeclaration UpdateApplicantDeclaration(ApplicantDeclaration declaration)
        {
            _context.ApplicantDeclarations.Attach(declaration);
            _context.Entry(declaration).State = System.Data.Entity.EntityState.Modified;
            return declaration;
        }
    }

}
