using StatsSA.eRecruitment.IManager.ApplicantContactDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using System.Data.Entity;

namespace StatsSA.eRecruitment.Manager.ApplicantContactDetails
{
    public class ApplicantContactDetailsManager : IApplicantContactDetailsManager
    {
        private readonly eRecruitmentEntities _context;
        public ApplicantContactDetailsManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public ApplicantContactDetail getApplicantContactDetail(int applicantProfileId)
        {
            return _context.ApplicantContactDetails.FirstOrDefault(contDet => contDet.ApplicantProfileId == applicantProfileId);
        }

        public ApplicantContactDetail UpdateContactDetail(ApplicantContactDetail contactDetail)
        {
            contactDetail.Language = null;
            _context.ApplicantContactDetails.Attach(contactDetail);
            _context.Entry(contactDetail).State = EntityState.Modified;
            return contactDetail;
        }

        public ApplicantContactDetail AddApplicantContactDetails(ApplicantContactDetail contactDetail)
        {
            contactDetail.Language = null;
            _context.ApplicantContactDetails.Add(contactDetail);
            return contactDetail;
        }
    }

}
