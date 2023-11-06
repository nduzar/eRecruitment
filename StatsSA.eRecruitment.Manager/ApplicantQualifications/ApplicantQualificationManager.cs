using StatsSA.eRecruitment.IManager.Qualifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.Qualifications
{
    public class ApplicantQualificationsManager : IApplicantQualificationManager
    {
        private readonly eRecruitmentEntities _context;
        public ApplicantQualificationsManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public IEnumerable<ApplicantQualification> getApplicantQualification(int ApplicantProfileId)
        {
            List<ApplicantQualification> list = new List<ApplicantQualification>();

            var applicantQualifications = _context.ApplicantQualifications.Where(appPr => appPr.ApplicantProfileId == ApplicantProfileId).ToList();

            foreach (var item in applicantQualifications)
            {
                var institution = _context.Institutions.Where(id => id.InstitutionId == item.InstitutionId).FirstOrDefault();
                item.Institution = institution;
                list.Add(item);
            }

            return list;
        }

        public void AddApplicantQualification(ApplicantQualification qualification)
        {
            _context.ApplicantQualifications.Add(qualification);
        }

        public void RemoveApplicantQualification(int ApplicantQualificationId)
        {
            var quals = _context.ApplicantQualifications.Where(appPr => appPr.ApplicantQualificationId == ApplicantQualificationId).ToList();
            _context.ApplicantQualifications.RemoveRange(quals);
        }

        public void UpdateApplicantQualification(ApplicantQualification qualification)
        {
            _context.ApplicantQualifications.Attach(qualification);
            _context.Entry(qualification).State = System.Data.Entity.EntityState.Modified;
        }
    }

}
