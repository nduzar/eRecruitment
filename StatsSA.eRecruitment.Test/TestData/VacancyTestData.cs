using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Test.TestDataBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test
{
    public partial class ObjectMother
    {
        public static class VacancyTestData
        {
            public static Vacancy ChiefDirector;
            public static Vacancy Director;
            public static Vacancy Developer;
            
            public static void Create()
            {

                ChiefDirector = UnitOfWork.VacancyManager.AddVacancy(new VacancyBuilder().build());

                Director = UnitOfWork.VacancyManager.AddVacancy(new VacancyBuilder()
                    .WithJobTitle("Director")
                    .WithDivisionName("Financial Management")
                    .WithOfficeName("Financial Administration chief directorate at Head Office, Pretoria")
                    .WithSalaryId(13)
                    .WithSalaryNotch(898743)
                    .WithReferenceNumber("02/10/16HO")
                    .build());

                Developer = UnitOfWork.VacancyManager.AddVacancy(new VacancyBuilder()
                    .WithJobTitle("Deleloper")
                    .WithDivisionName("Financial Management")
                    .WithOfficeName("Financial Administration chief directorate at Head Office, Pretoria")
                    .WithSalaryId(13)
                    .WithSalaryNotch(898743)
                    .WithClosingDate(DateTime.Now.AddDays(-15))
                    .WithClosingDate(DateTime.Now.AddDays(-1))
                    .WithReferenceNumber("03/10/16HO")
                    .build());



                UnitOfWork.SaveChanges();

            }
        }
    }
}
