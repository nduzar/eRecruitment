using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Vacancies
{
    public interface IVacancyManager
    {
        Vacancy AddVacancy(Vacancy vacancy);
        IEnumerable<Vacancy> GetAllActiveVacancies();

        IEnumerable<Vacancy> GetCapturedVacancies();

        Vacancy GetVacancyById(int vacancyId);

        IEnumerable<Vacancy> GetApprovedVacancies();
        IEnumerable<Vacancy> GetAuthorizedVacancies();
        IEnumerable<Vacancy> GetCancelledVacancies();

        void ChangeVacancyStatus(Vacancy vacancy);

        IEnumerable<Vacancy> GetCancelableVacancies();

        IEnumerable<Vacancy> GetVacanciesForScreening();

        Vacancy GetVacanyByVacancyId(int VacancyId);
        IEnumerable<Vacancy> GetEditableVacancies();

    }
}
