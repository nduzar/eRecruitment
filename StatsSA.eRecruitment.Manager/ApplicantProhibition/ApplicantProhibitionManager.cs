using StatsSA.eRecruitment.IManager.ApplicantProhibition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.ApplicantProhibition
{
    public class ApplicantProhibitionManager : IApplicantProhibitionManager
    {
        private readonly eRecruitmentEntities _context;
        public ApplicantProhibitionManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public void AddApplicantProhibition(ApplicantPubServProhibition prohibition)
        {
            _context.ApplicantPubServProhibitions.Add(prohibition);
        }

        public ApplicantPubServProhibition getApplicantProhibition(int applicantProfileId)
        {
            return _context.ApplicantPubServProhibitions.FirstOrDefault(prohib => prohib.ApplicantProfileId == applicantProfileId);
        }
        public ApplicantPubServProhibition UpdateApplicantProhibition(ApplicantPubServProhibition prohibition)
        {
            _context.ApplicantPubServProhibitions.Attach(prohibition);
            _context.Entry(prohibition).State = System.Data.Entity.EntityState.Modified;
            return prohibition;
        }
    }

}
