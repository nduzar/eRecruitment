using StatsSA.eRecruitment.IManager.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System.Globalization;
using System.IO;

namespace StatsSA.eRecruitment.Manager.Applications
{
    public class ApplicationManager : IApplicationManager
    {
        private eRecruitmentEntities _context;
        public ApplicationManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public Application AddApplication(Application ApplicationToAdd)
        {
            try
            {                
                return _context.Applications.Add(ApplicationToAdd);
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        public void UpdateApplication(Application ApplicationToUpdate)
        {
            _context.Applications.Attach(ApplicationToUpdate);
            _context.Entry(ApplicationToUpdate).State = System.Data.Entity.EntityState.Modified;
        }
        public void UpdateApplicationStatus(Application ApplicationToUpdate)
        {
            _context.Applications.Attach(ApplicationToUpdate);
            _context.Entry(ApplicationToUpdate).State = System.Data.Entity.EntityState.Modified;
        }
        public IEnumerable<Application> GetApplicantApplications(int applicantProfileId)
        {
            return _context.Applications.Where(a => a.VerApplicantProfileId == applicantProfileId).ToList();
        }
        public Application GetApplicationById(int ApplicationId)
        {
            return _context.Applications.FirstOrDefault(application => application.ApplicationId == ApplicationId);
        }

        public IEnumerable<Application> GetRankedApplications(int vacancyId)
        {
            return _context.Applications.Where(a => a.VacancyId == vacancyId).ToList();
        }

        public IEnumerable<ListOfApplication> GetAllApplications(int vacancyId)
        {
            return _context.ListOfApplications.Where(a => a.VacancyId == vacancyId).ToList();
        }

        public IEnumerable<Application> GetApplicationByEmail(int vacancyId, string email)
        {
            return _context.Applications.Where(a => a.VacancyId == vacancyId && a.ApplicationStatusId == (int)Entities.Enums.ApplicationStatuses.Received).ToList();
        }

        public IEnumerable<Application> GetApplicantApplication(IEnumerable<int> verApplicantProfileIds)
        {
            return _context.Applications.Where(a => verApplicantProfileIds.Any(id => id == a.VerApplicantProfileId)).ToList();
        }
    }
}
