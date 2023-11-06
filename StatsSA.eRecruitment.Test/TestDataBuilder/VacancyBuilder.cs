using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test.TestDataBuilder
{
    public class VacancyBuilder
    {
        private string JobTitle = "Chief Director";
        private string OfficeName = "Mpumalanga Provincial Office";
        private string DivisionName = "Mpumalanga Provincial Management";
        private int NumberOfPosts = 1;
        private string KeyPerformanceAreas = "<ul><li>Develop strategic, business and operational plans for the component</li><li>Develop content for all labour market surveys and related modules<br/></li><li>Develop and test survey instruments, procedures and guidelines for labour statistics related surveys<br/></li><li>Facilitate analysis of data and writing of reports<br/></li><li>Conduct research and recommend appropriate methodologies for the production of Labour Statistics<br/></li><li>Liaise with and provide statistical support to stakeholders<br/></li><li>Conduct training in collaboration with Survey Operations<br/></li><li>Supervise staff<br/></li></ul>";
        private string Prerequisites = "<ul><li>​A three-year tertiary qualification in Statistics/Demography/Econometrics/Economics/Social Sciences or related fields</li><li>Training in statistical analysis is essential<br/></li><li>Training in Project Management would be an added advantage<br/></li><li>At least five years experience in analysis of data, quantitative research and writing reports of which two years must be on Assistant Director level or equivalent<br/></li><li>Experience in the use of SAS statistical software for data analysis<br/></li><li>Knowledge of labour force conceptual framework and labour market issues<br/></li><li>Knowledge of MS Office Suite<br/></li></ul>";
        private string PersonProfile = "<ul><li>​Good communication, conceptual, analytical, numerical, strategic, operational planning, co-ordination and liaison skills</li><li>Customer oriented<br/></li><li>Ability to work under pressure<br/></li><li>Ability to handle multiple and complex tasks and projects<br/></li></ul>";
 
        private int SalaryId = 14;
        private decimal SalaryNotch = 1068564;
        private string RemunerationDescription = "all-inclusive remuneration package per annum";
        private DateTime OpeningDate = DateTime.Now.AddDays(-2);
        private DateTime ClosingDate = DateTime.Now.AddDays(14);
        private string ReferenceNumber = "01/10/16MP";
        private int StatusId = 5;
        private string StatusComment = "";
        private bool IsPermanent = true;
        private string MaintUser = "HRSuperUser";
        private DateTime MaintDate = DateTime.Now;

        public VacancyBuilder WithJobTitle(string jobTitle)
        {
            this.JobTitle = jobTitle;
            return this;
        }
        public VacancyBuilder WithOfficeName(string officeName)
        {
            this.OfficeName = officeName;
            return this;
        }
        public VacancyBuilder WithDivisionName(string divisionName)
        {
            this.DivisionName = divisionName;
            return this;
        }
        public VacancyBuilder WithNumberOfPosts(int numberOfPosts)
        {
            this.NumberOfPosts = numberOfPosts;
            return this;
        }
        public VacancyBuilder WithKeyPerformanceAreas(string keyPerformanceAreas)
        {
            this.KeyPerformanceAreas = keyPerformanceAreas;
            return this;
        }
        public VacancyBuilder WithPrerequisites(string prerequisites)
        {
            this.Prerequisites = prerequisites;
            return this;
        }
        public VacancyBuilder WithPersonProfile(string personProfile)
        {
            this.PersonProfile = personProfile;
            return this;
        }
        public VacancyBuilder WithSalaryId(int salaryLevel)
        {
            this.SalaryId = salaryLevel;
            return this;
        }
        public VacancyBuilder WithSalaryNotch(decimal salaryNotch)
        {
            this.SalaryNotch = salaryNotch;
            return this;
        }
        public VacancyBuilder WithReferenceNumber(string referenceNumber)
        {
            this.ReferenceNumber = referenceNumber;
            return this;
        }

        public VacancyBuilder WithOpeningDate(DateTime openingDate)
        {
            this.OpeningDate = openingDate;
            return this;
        }

        public VacancyBuilder WithClosingDate(DateTime closingDate)
        {
            this.OpeningDate = closingDate;
            return this;
        }
        public Vacancy build()
        {
            return new Vacancy()
            {
                JobTitle = this.JobTitle,
                OfficeName = this.OfficeName,
                DivisionName = this.DivisionName,
                NumberOfPosts = this.NumberOfPosts,
                KeyPerformanceAreas = this.KeyPerformanceAreas,
                Prerequisites = this.Prerequisites,
                PersonProfile = this.PersonProfile, 
                SalaryId = this.SalaryId,
                SalaryNotch = this.SalaryNotch,
                RemunerationDescription = this.RemunerationDescription,
                OpeningDate = this.OpeningDate,
                ClosingDate = this.ClosingDate,
                ReferenceNumber = this.ReferenceNumber,
                StatusId = this.StatusId,
                StatusComment = this.StatusComment,
                IsPermanent = this.IsPermanent,
                MaintUser = this.MaintUser,
                MaintDate = this.MaintDate
            };
        }
    }
}
