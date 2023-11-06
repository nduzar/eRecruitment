using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.Vacancies
{
    public interface IVacancyProvider
    {
        IEnumerable<Vacancy> GetAllActiveVacancies();
        IEnumerable<Vacancy> GetCapturedVacancies();
        IEnumerable<Vacancy> GetApprovedVacancies();
        IEnumerable<Vacancy> GetAuthorizedVacancies();
        IEnumerable<Vacancy> GetCancelledVacancies();

        Vacancy GetvacancyById(int vacancyId);

        IEnumerable<Vacancy> ChangeVacancyStatusApprover(Vacancy vacancy);
        IEnumerable<Vacancy> ChangeVacancyStatusAuthoriser(Vacancy vacancy);
        IEnumerable<Vacancy> ChangeVacancyStatusCancelled(Vacancy vacancy);
        IEnumerable<Vacancy> GetCancelableVacancies();
        IEnumerable<Vacancy> GetVacanciesForScreening();

        Vacancy UpdateVacancy(Vacancy vacancy);

        IEnumerable<Vacancy> GetEditableVacancies();

        Vacancy AddVacancy(Vacancy vacancy);
        Vacancy GetVacanyByVacancyId(int VacancyId);

        byte[] PrintToWord();
    }
}
