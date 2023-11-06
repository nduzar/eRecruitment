using StatsSA.eRecruitment.IManager.VerApplicantDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.VerApplicantDeclarations
{
    public class VerApplicantDeclarationManager :  IVerApplicantDeclarationManager
    {
        private readonly eRecruitmentEntities _context;
        public VerApplicantDeclarationManager(eRecruitmentEntities context) 
        {
            _context = context;
        }

        public VerApplicantDeclaration getVerApplicantDeclaration(int verApplicantProfileId)
        {
            return _context.VerApplicantDeclarations.Where(exp => exp.VerApplicantProfileId == verApplicantProfileId).FirstOrDefault();
           // return base.Get(exp => exp.VerApplicantProfileId == verApplicantProfileId);
        }
    }

}
