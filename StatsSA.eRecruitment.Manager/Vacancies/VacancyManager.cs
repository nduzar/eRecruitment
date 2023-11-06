using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Entities.Enums;
using StatsSA.eRecruitment.IManager.Vacancies;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.Vacancies
{
    public class VacancyManager :  IVacancyManager
    {
        private eRecruitmentEntities _context;
        public VacancyManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public Vacancy AddVacancy(Vacancy vacancy)
        {
            vacancy.Salary = null;
            return _context.Vacancies.Add(vacancy);
        }

        public Vacancy GetVacancyById(int vacancyId)
        {
            return _context.Vacancies.Where(v => v.VacancyId == vacancyId).FirstOrDefault();
        }

        public IEnumerable<Vacancy> GetAllActiveVacancies()
        {
            var currDate = DateTime.Now;
            return _context.Vacancies.Where(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Authorised && v.OpeningDate < currDate && v.ClosingDate > currDate).ToList();
        }

        public IEnumerable<Vacancy> GetCapturedVacancies()
        {            
            var Iobj = _context.Vacancies.Where(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Captured || v.StatusId == (int)Entities.Enums.VacancyStatuses.AuthoriserRejected).ToList();
            return Iobj;
        }
        public IEnumerable<Vacancy> GetEditableVacancies()
        {
            var Iobj = _context.Vacancies.Where(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Captured || v.StatusId == (int)Entities.Enums.VacancyStatuses.ApproverRejected || v.StatusId == (int)Entities.Enums.VacancyStatuses.Approved).ToList();
            return Iobj;
        }
        public void ChangeVacancyStatus(Vacancy vacancy)
        {
            vacancy.Salary = null;
            _context.Vacancies.Attach(vacancy);
            _context.Entry(vacancy).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Vacancy> GetApprovedVacancies()
        {
            //return base.GetManyQueryable(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Approved);
           return _context.Vacancies.Where(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Approved).ToList();
        }

        public IEnumerable<Vacancy> GetAuthorizedVacancies()
        {
            //return base.GetManyQueryable(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Approved);
            return _context.Vacancies.Where(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Authorised).ToList();
        }

        public IEnumerable<Vacancy> GetCancelledVacancies()
        {
            //return base.GetManyQueryable(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Approved);
            return _context.Vacancies.Where(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Cancelled).ToList();
        }
        

        public IEnumerable<Vacancy> GetCancelableVacancies()
        {
            return _context.Vacancies.Where(v => 
            v.StatusId == (int)Entities.Enums.VacancyStatuses.Captured ||
            v.StatusId == (int)Entities.Enums.VacancyStatuses.ApproverRejected ||
            v.StatusId == (int)Entities.Enums.VacancyStatuses.Approved ||
            v.StatusId == (int)Entities.Enums.VacancyStatuses.ApproverRejected ||
            v.StatusId == (int)Entities.Enums.VacancyStatuses.Authorised ||
            v.StatusId == (int)Entities.Enums.VacancyStatuses.Screened ||
            v.StatusId == (int)Entities.Enums.VacancyStatuses.Shortlisted ||
            v.StatusId == (int)Entities.Enums.VacancyStatuses.Interviewed
            ).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesForScreening()
        {
            var currDate = DateTime.Now;           
            return _context.Vacancies.Where(v => v.StatusId == (int)Entities.Enums.VacancyStatuses.Authorised || v.StatusId == (int)Entities.Enums.VacancyStatuses.Screened && v.ClosingDate < currDate).ToList();
        }

        public Vacancy GetVacanyByVacancyId(int VacancyId)
        {
            var currDate = DateTime.Now;
            return _context.Vacancies.FirstOrDefault(v => v.VacancyId == VacancyId);
        }
    }
}
