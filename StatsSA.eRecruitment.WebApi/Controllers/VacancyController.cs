using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.AnswersGenericQuestions;
using StatsSA.eRecruitment.IProvider.AnswersSpecificQuestions;
using StatsSA.eRecruitment.IProvider.AnswerThreshold;
using StatsSA.eRecruitment.IProvider.Applications;
using StatsSA.eRecruitment.IProvider.GenericQuestions;
using StatsSA.eRecruitment.IProvider.Salaries;
using StatsSA.eRecruitment.IProvider.SpecificQuestions;
using StatsSA.eRecruitment.IProvider.Vacancies;
using StatsSA.eRecruitment.IProvider.VerApplicantProfiles;
using StatsSA.eRecruitment.WebApi.Extensions;
using StatsSA.eRecruitment.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [RoutePrefix("api/vacancy")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VacancyController : ApiController
    {
        private IVacancyProvider _vacancyProvider;
        private IVerApplicantProfileProvider _verApplicantProfileProvider;
        private IApplicationProvider _applicationProvider;
        private ISalariesProvider _salariesProvider;
        private IGenericQuestionProvider _genericQuestions;
        private ISpecificQuestionsProvider _specificQuestions;
        private IAnswersSpecificQuestionsProvider _answersSpecificQuestions;
        private IAnswersGenericQuestionsProvider _answersGenericQuestions;
        private IAnswersThresholdProvider _answersThreshold;

        public VacancyController(IVacancyProvider vacancyProvider,
                                IVerApplicantProfileProvider verApplicantProfileProvider,
                                IApplicationProvider applicationProvider,
                                ISalariesProvider salariesProvider,
                                IGenericQuestionProvider genericQuestions,
                                ISpecificQuestionsProvider specificQuestions,
                                IAnswersSpecificQuestionsProvider answersSpecificQuestions,
                                IAnswersGenericQuestionsProvider answersGenericQuestions,
                                IAnswersThresholdProvider answersThreshold)
        {
            _vacancyProvider = vacancyProvider;
            _verApplicantProfileProvider = verApplicantProfileProvider;
            _applicationProvider = applicationProvider;
            _salariesProvider = salariesProvider;
            _genericQuestions = genericQuestions;
            _specificQuestions = specificQuestions;
            _answersSpecificQuestions = answersSpecificQuestions;
            _answersGenericQuestions = answersGenericQuestions;
            _answersThreshold = answersThreshold;
        }

        [HttpPost]
        [Route("getAllActiveVacancies")]
        public IHttpActionResult GetAllActiveVacancies()
        {
            IEnumerable<Application> applicantApplications = new List<Application>();
            if (User.Identity.IsAuthenticated)
            {
                var verApplicantProfiles = _verApplicantProfileProvider.GetVerAppProfileByUserId(User.GetUserId());
                var verApplicantProfileIds = verApplicantProfiles.Select(p => p.VerApplicantProfileId);
                applicantApplications = _applicationProvider.GetApplicantApplication(verApplicantProfileIds);

            }
            
            var vacancies = _vacancyProvider.GetAllActiveVacancies();

            foreach (var vacancy in vacancies)
            {
                //System.Diagnostics.Debugger.Break();

                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
               // vacancy.FilteringQuestions = _filteringQuestions.GetAllQuestions(vacancy.VacancyId).ToList();
            }

            var result = new VacancyResult
            {
                Vacancies = vacancies,
                CurrentApplications = applicantApplications
            }; 
            return Ok(result);
        }

        [HttpPost]
        [Route("getAllQuestionsAndAnswers")]
        public IHttpActionResult GetAllQuestionsAndAnswers(Vacancy vacancy)
        {
            ApplicationModels applicationModels = new ApplicationModels();
            if (vacancy != null)
            {
                var listGenericQuestions = _genericQuestions.GetAllQuestions();
                var listAllGenericAnswers = _answersGenericQuestions.GetAllAnswersGenericQuestionByVacancy(vacancy.VacancyId);

                var listAllSpecificQuestions = _specificQuestions.GetAllQuestions(vacancy.VacancyId);

                List<SpecificQuestionsAnswers> listSpecificAnswers = new List<SpecificQuestionsAnswers>();

                foreach (var item in listAllSpecificQuestions)
                {
                    var specificQuestionsAnswers = new SpecificQuestionsAnswers()
                    {
                        SpecificQuestion = item,
                        ListAnswersSpecificQuestions = _answersSpecificQuestions.GetAllAnswersSpecificQuestion(item.Id).ToList()
                    };
                    listSpecificAnswers.Add(specificQuestionsAnswers);
                };
                
                var selectedGenericQuestion = from g in listGenericQuestions
                                              join a in listAllGenericAnswers
                                              on g.Id equals a.QuestionId
                                              select new GenericQuestion
                                              {
                                                  Id = g.Id,
                                                  Description = g.Description,
                                                  Question = g.Question
                                              };

                var grouped = selectedGenericQuestion.GroupBy(x => x.Id).Select(x => x.First());
                List<GenericQuestionsAnswers> listSelectedGenericQuestions = new List<GenericQuestionsAnswers>();

                foreach (var item in grouped)
                {
                    GenericQuestionsAnswers genericQuestionsAnswers = new GenericQuestionsAnswers
                    {
                        GenericQuestion = item,
                        ListAnswersGenericQuestions = listAllGenericAnswers.Where(x => x.QuestionId == item.Id).ToList()

                    };                    
                    listSelectedGenericQuestions.Add(genericQuestionsAnswers);
                }
                if (vacancy != null)
                {
                    applicationModels = new ApplicationModels()
                    {
                        GenericQuestionsAnswers = listSelectedGenericQuestions,
                        SpecificQuestionsAnswers = listSpecificAnswers,
                        AnswersThreshold = _answersThreshold.GetAnswersThreshold(vacancy.VacancyId)
                    };
                }
            }
            return Ok(applicationModels);
        }

    }
    public class VacancyResult
    {
        public  IEnumerable<Vacancy> Vacancies { get; set; }
        public IEnumerable<Application> CurrentApplications { get; set; }
    }
}
