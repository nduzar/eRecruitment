using StatsSA.eRecruitment.IProvider.Vacancies;
using StatsSA.eRecruitment.IProvider.VacancyStatuses;
using StatsSA.eRecruitment.IProvider.HRUsersProvider;
using StatsSA.eRecruitment.IProvider.HRUserRolesProvider;
using StatsSA.eRecruitment.InternalWeb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Entities.Enums;
using StatsSA.eRecruitment.IProvider.Salaries;
using StatsSA.eRecruitment.IProvider.GenericQuestions;
using StatsSA.eRecruitment.IProvider.AnswersGenericQuestions;
using StatsSA.eRecruitment.IProvider.SpecificQuestions;
using StatsSA.eRecruitment.InternalWeb.Models;
using StatsSA.eRecruitment.IProvider.AnswersSpecificQuestions;
using StatsSA.eRecruitment.IProvider.AnswerThreshold;
using StatsSA.eRecruitment.WebApi.Models;
using StatsSA.eRecruitment.IProvider.RequestAllApplications;

namespace StatsSA.eRecruitment.InternalWeb.Controllers
{
    [Authorize, RoutePrefix("api/vacancy")]
    public class InternalVacancyController : ApiController
    {
        private EmailExtension emailService;
        private IVacancyProvider _vacancyProvider;
        private IAnswersGenericQuestionsProvider _answersGenericQuestions;
        private ISpecificQuestionsProvider _SpecificQuestions;
        private IAnswersThresholdProvider _answersThreshold;
        private IVacancyStatusProvider _vacancyStatusProvider;
        private IHRUserProvider _hrUserProvider;
        private IHRUserRoleProvider _hrUserRoleProvider;
        private ISalariesProvider _salariesProvider;
        private IGenericQuestionProvider _genericQuestions;
        private IAnswersSpecificQuestionsProvider _answersSpecificQuestions;
        private IRequestAllApplicationsProvider _requestAllApplications;

        public InternalVacancyController(IVacancyProvider vacancyProvider, IVacancyStatusProvider vacancyStatusProvider,
            IHRUserProvider hrUserProvider, IHRUserRoleProvider hrUserRoleProvider,
             ISalariesProvider salariesProvider, IGenericQuestionProvider genericQuestionProvider,
             IAnswersGenericQuestionsProvider answersGenericQuestions,
             ISpecificQuestionsProvider specificQuestions,
             IAnswersSpecificQuestionsProvider answersSpecificQuestions,
             IAnswersThresholdProvider answersThreshold,
             IRequestAllApplicationsProvider requestAllApplications)
        {
            _vacancyProvider = vacancyProvider;
            _vacancyStatusProvider = vacancyStatusProvider;
            _hrUserProvider = hrUserProvider;
            _hrUserRoleProvider = hrUserRoleProvider;
            _salariesProvider = salariesProvider;
            _genericQuestions = genericQuestionProvider;
            _answersGenericQuestions = answersGenericQuestions;
            _SpecificQuestions = specificQuestions;
            _answersSpecificQuestions = answersSpecificQuestions;
            _answersThreshold = answersThreshold;
            _requestAllApplications = requestAllApplications;
        }

        [HttpGet]
        [Route("getAllActiveVacancies")]
        public IHttpActionResult GetAllActiveVacancies()
        {
            var vacancies = _vacancyProvider.GetAllActiveVacancies().OrderByDescending(x => x.MaintDate);
            return Ok(vacancies);
        }

        [HttpPost]
        [Route("getcaptredvacancies")]
        public IHttpActionResult GetCaptredVacancies()
        {
            var vacancies = _vacancyProvider.GetCapturedVacancies().OrderByDescending(x => x.MaintDate);
            foreach (var vacancy in vacancies)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }

            return Ok(vacancies);
        }
        [HttpPost]

        [Route("geteditablevacancies")]
        public IHttpActionResult GetEditableVacancies()
        {
            var vacancies = _vacancyProvider.GetEditableVacancies().OrderByDescending(x => x.MaintDate);
            foreach (var vacancy in vacancies)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }

            return Ok(vacancies);
        }

        [HttpPost]
        [Route("getcancelable")]
        public IHttpActionResult GetCancelableVacancies()
        {
            var vacancies = _vacancyProvider.GetCancelableVacancies().OrderByDescending(x => x.MaintDate);
            foreach (var vacancy in vacancies)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }

            return Ok(vacancies);
        }

        [HttpPost]
        [Route("cancelvacancy")]
        public IHttpActionResult CancelVacancy(Vacancy objVacancy)
        {
            objVacancy.StatusId = (int)VacancyStatuses.Cancelled;
            objVacancy.MaintUser = User.Identity.Name;
            var result = _vacancyProvider.ChangeVacancyStatusCancelled(objVacancy);
            foreach (var vacancy in result)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }

            string subject = "Vacancy cancelled: " + objVacancy.JobTitle;
            string emailContent = "Please note that vacancy " + objVacancy.JobTitle + " has been cancelled.";

            List<int> listUserRoles = new List<int>();
            listUserRoles.Add((int)HRUserRoles.Approver);
            listUserRoles.Add((int)HRUserRoles.Authoriser);
            listUserRoles.Add((int)HRUserRoles.Capturer);
            SentEmailAll(subject, emailContent, listUserRoles);

            return Ok(result);
        }

        [HttpPost]
        [Route("getapprovedvacancies")]
        public IHttpActionResult GetApprovedVacancies()
        {
            var vacancies = _vacancyProvider.GetApprovedVacancies().OrderByDescending(x => x.MaintDate);
            foreach (var vacancy in vacancies)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }
            return Ok(vacancies);
        }

        [HttpPost]
        [Route("updatevacancies")]
        public IHttpActionResult UpdateVacancies(Vacancy objVacancy)
        {
            string subject = "Vacancy updated: " + objVacancy.JobTitle;
            objVacancy.StatusId = (int)VacancyStatuses.Captured;
            return UpdateVacancy(objVacancy, subject);           
        }

        [HttpPost]
        [Route("updateauthorisedvacancies")]
        public IHttpActionResult UpdateAuthorisedVacancies(Vacancy objVacancy)
        {
            objVacancy.StatusId = (int)VacancyStatuses.Authorised;
            string subject = "Authorised vacancy was updated: " + objVacancy.JobTitle;
            return UpdateVacancy(objVacancy, subject);
        }

        private IHttpActionResult UpdateVacancy(Vacancy objVacancy, string subject)
        {
            try
            {
                string emailContent = "Please note that vacancy " + objVacancy.JobTitle + " has been updated.";

                List<int> listUserRoles = new List<int>();
                listUserRoles.Add((int)HRUserRoles.Approver);
                listUserRoles.Add((int)HRUserRoles.Authoriser);
                listUserRoles.Add((int)HRUserRoles.Capturer);
                SentEmailAll(subject, emailContent, listUserRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }

            objVacancy.MaintUser = User.Identity.Name;
            var result = _vacancyProvider.ChangeVacancyStatusApprover(objVacancy);
            foreach (var vacancy in result)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }
            return Ok(result);
        }


        [HttpPost]
        [Route("addQuestion")]
        public IHttpActionResult AddAnswerGenericQuestion(List<AnswersGenericQuestion> answersGenericQuestion)
        {
            try
            {
                _answersGenericQuestions.AddAnswersGenericQuestion(answersGenericQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }
            return Ok();
        }

        [HttpPost]
        [Route("updateAnswerGenericQuestion")]
        public IHttpActionResult UpdateAnswerGenericQuestion(List<AnswersGenericQuestion> answersGenericQuestion)
        {
            try
            {
                _answersGenericQuestions.UpdateAnswersGenericQuestionById(answersGenericQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }
            return Ok();
        }

        [HttpPost]
        [Route("deleteAnswerGenericQuestion")]
        public IHttpActionResult DeleteAnswerGenericQuestion(AnswersGenericQuestion answerGenericQuestion)
        {
            try
            {
                _answersGenericQuestions.DeleteAnswersGenericQuestionById(answerGenericQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }
            return Ok();
        }

        [HttpPost]
        [Route("deleteGenericQuestion")]
        public IHttpActionResult DeleteGenericQuestion(GenericQuestion genericQuestion)
        {
            try
            {
                _answersGenericQuestions.DeleteGenericQuestion(genericQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }
            return Ok();
        }

        [HttpPost]
        [Route("addSpecificQuestions")]
        public IHttpActionResult AddSpecificQuestions(List<SpecificQuestionsAnswers> specificQuestions)
        {
            //var results = _SpecificQuestions.AddSpecificQuestions(specificQuestions.SpecificQuestion);

            //for (int i = 0; i < specificQuestions.ListAnswersSpecificQuestions.Count; i++)
            //{
            //    specificQuestions.ListAnswersSpecificQuestions[i].QuestionId = results.Id;
            //}

            //_answersSpecificQuestions.AddAnswersSpecificQuestion(specificQuestions.ListAnswersSpecificQuestions);

            return Ok("");
        }

        [HttpPost]
        [Route("addQuestionsAnswers")]
        public IHttpActionResult AddQuestionsAnswers(QuestionsAnswers questionsAnswers)
        {
            try
            {
                // Specifc questions
                foreach (var specificQuestion in questionsAnswers.ListSpecificQuestionsAnswers)
                {
                    var results = _SpecificQuestions.AddSpecificQuestions(specificQuestion.SpecificQuestion);

                    foreach (var answer in specificQuestion.ListAnswersSpecificQuestions)
                    {
                        answer.QuestionId = results.Id;
                    }
                    _answersSpecificQuestions.AddAnswersSpecificQuestion(specificQuestion.ListAnswersSpecificQuestions);

                }

                //Generic questions
                _answersGenericQuestions.AddAnswersGenericQuestion(questionsAnswers.ListGenericQuestionsAnswers);

                //Add threshold
                _answersThreshold.AddThreshold(questionsAnswers.AnswersThreshold);

            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }

            return Ok();
        }

        [HttpPost]
        [Route("updateSpecificQuestions")]
        public IHttpActionResult UpdateSpecificQuestions(List<SpecificQuestionsAnswers> specificQuestions)
        {
            foreach (var item in specificQuestions)
            {
                if (item.SpecificQuestion.Id == 0)
                {
                    var results = _SpecificQuestions.AddSpecificQuestions(item.SpecificQuestion);

                    foreach (var answer in item.ListAnswersSpecificQuestions)
                    {
                        answer.QuestionId = results.Id;
                    }
                    _answersSpecificQuestions.AddAnswersSpecificQuestion(item.ListAnswersSpecificQuestions);
                }
                else
                {
                    var results = _SpecificQuestions.UpdateSpecificQuestionById(item.SpecificQuestion);

                    for (int i = 0; i < item.ListAnswersSpecificQuestions.Count; i++)
                    {
                        _answersSpecificQuestions.UpdateAnswersSpecificQuestionById(item.ListAnswersSpecificQuestions[i]);
                    }
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("deleteAnswerSpecificQuestion")]
        public IHttpActionResult DeleteAnswerSpecificQuestion(AnswersSpecificQuestion answerSpecificQuestion)
        {
            try
            {
                _answersSpecificQuestions.DeleteAnswersSpecificQuestionById(answerSpecificQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }
            return Ok();
        }

        [HttpPost]
        [Route("deleteSpecificQuestion")]
        public IHttpActionResult DeleteSpecificQuestion(SpecificQuestion specificQuestion)
        {
            try
            {
                _answersSpecificQuestions.DeleteSpecificQuestion(specificQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }
            return Ok();
        }


        [HttpPost]
        [Route("addAnswersThreshold")]
        public IHttpActionResult AddAnswersThreshold(AnswersThreshold answersThreshold)
        {
            _answersThreshold.AddThreshold(answersThreshold);

            return Ok();
        }

        [HttpPost]
        [Route("getAnswersThreshold")]
        public IHttpActionResult GetAnswersThreshold(Vacancy vacancy)
        {
            var threshold = _answersThreshold.GetAnswersThreshold(vacancy.VacancyId);

            return Ok(threshold);
        }

        [HttpPost]
        [Route("updateAnswersThreshold")]
        public IHttpActionResult UpdateAnswersThreshold(AnswersThreshold answersThreshold)
        {
            var itemToBeUpdated = _answersThreshold.GetAnswersThreshold(answersThreshold.VacancyId);
            if (itemToBeUpdated == null)
            {
                _answersThreshold.AddThreshold(answersThreshold);
            }
            else
            {
                itemToBeUpdated.SumOfWeights = answersThreshold.SumOfWeights;
                itemToBeUpdated.Threshold = answersThreshold.Threshold;
                itemToBeUpdated.VacancyId = answersThreshold.VacancyId;

                return Ok(_answersThreshold.UpdateAnswersThreshold(itemToBeUpdated));
            }


            return Ok();
        }



        [HttpPost]
        [Route("vacancy")]
        public IHttpActionResult AddVacancy(Vacancy vacancy)
        {

            var objVacancy = new Vacancy
            {
                JobTitle = vacancy.JobTitle,
                OfficeName = vacancy.OfficeName,
                DivisionName = vacancy.DivisionName,
                NumberOfPosts = vacancy.NumberOfPosts,
                ReferenceNumber = vacancy.ReferenceNumber,
                KeyPerformanceAreas = vacancy.KeyPerformanceAreas,
                Prerequisites = vacancy.Prerequisites,
                PersonProfile = vacancy.PersonProfile,
                Salary = vacancy.Salary,
                SalaryId = vacancy.SalaryId,
                SalaryNotch = vacancy.SalaryNotch,
                RemunerationDescription = vacancy.RemunerationDescription,
                OpeningDate = vacancy.OpeningDate,
                ClosingDate = vacancy.ClosingDate,
                StatusId = (int)VacancyStatuses.Captured,//vacancy.StatusId,
                StatusComment = "newly created job specification",//vacancy.StatusComment,
                IsPermanent = vacancy.IsPermanent,
                MaintUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                MaintDate = DateTime.Now
            };
            var result = _vacancyProvider.AddVacancy(objVacancy);
            try
            {
                string subject = "Vacancy Approval Request for - " + vacancy.JobTitle; 
                string emailContent = "Please note that a vacancy " + vacancy.JobTitle + " has been captured. Please check it for your approval.\n Go to the link provided http://erecruitment.treasury.gov.za/ and click Vacancies menu item then click Approve Vacancy\n Kind Regars\n eRecruitment Team";

                List<int> listUserRoles = new List<int>();
                listUserRoles.Add((int)HRUserRoles.Approver);

                SentEmailAll(subject, emailContent, listUserRoles);

                //Send email to the capturer
                subject = "Vacancy Captured - " + vacancy.JobTitle;
                emailContent = "Please note that a vacancy " + vacancy.JobTitle + " has been captured";
                listUserRoles.Add((int)HRUserRoles.Capturer);

                SentEmailAll(subject, emailContent, listUserRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("approvevacancy")]
        public IHttpActionResult ApproveVacancy(Vacancy objVacancy)
        {
            try
            {
                string subject = "Vacancy Authorisation Request for - " + objVacancy.JobTitle;
                string emailContent = "Please note that a vacancy " + objVacancy.JobTitle + " has been approved. Please check it for your authorisation.\n Go to the link provided http://erecruitment.treasury.gov.za/ and click Vacancies menu item then click Authorise Vacancy \n Kind Regards\n eRecruitment Team";

                List<int> listUserRoles = new List<int>();
                listUserRoles.Add((int)HRUserRoles.Authoriser);

                SentEmailAll(subject, emailContent, listUserRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }

            objVacancy.StatusId = (int)VacancyStatuses.Approved;
            objVacancy.MaintUser = User.Identity.Name;
            var result = _vacancyProvider.ChangeVacancyStatusApprover(objVacancy);
            foreach (var vacancy in result)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("apprrejectvacancy")]
        public IHttpActionResult ApprRejectVacancy(Vacancy objVacancy)
        {
            try
            {
                string subject = "Vacancy Rejected - " + objVacancy.JobTitle;
                string emailContent = "Please note that a vacancy " + objVacancy.JobTitle + " has been rejected. \nReason: " + objVacancy.StatusComment;

                List<int> listUserRoles = new List<int>();
                listUserRoles.Add((int)HRUserRoles.Approver);
                listUserRoles.Add((int)HRUserRoles.Authoriser);
                listUserRoles.Add((int)HRUserRoles.Capturer);

                SentEmailAll(subject, emailContent, listUserRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }

            objVacancy.StatusId = (int)VacancyStatuses.ApproverRejected;
            objVacancy.MaintUser = User.Identity.Name;
            var result = _vacancyProvider.ChangeVacancyStatusApprover(objVacancy);
            return Ok(result);
        }

        [HttpPost]
        [Route("authorisevacancy")]
        public IHttpActionResult AuthoriseVacancy(Vacancy objVacancy)
        {
            objVacancy.StatusId = (int)VacancyStatuses.Authorised;
            objVacancy.MaintUser = User.Identity.Name;
            var result = _vacancyProvider.ChangeVacancyStatusAuthoriser(objVacancy);
            return Ok();
        }

        [HttpPost]
        [Route("authrejectvacancy")]
        public IHttpActionResult AuthRejectVacancy(Vacancy objVacancy)
        {
            try
            {
                string subject = "Vacancy Rejected - " + objVacancy.JobTitle;
                string emailContent = "Please note that a vacancy " + objVacancy.JobTitle + " has been rejected.\n\nReason: " + objVacancy.StatusComment;

                List<int> listUserRoles = new List<int>();
                listUserRoles.Add((int)HRUserRoles.Approver);
                listUserRoles.Add((int)HRUserRoles.Authoriser);
                listUserRoles.Add((int)HRUserRoles.Capturer);

                SentEmailAll(subject, emailContent, listUserRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }

            objVacancy.StatusId = (int)VacancyStatuses.AuthoriserRejected;
            objVacancy.MaintUser = User.Identity.Name;
            var result = _vacancyProvider.ChangeVacancyStatusAuthoriser(objVacancy);
            return Ok(result);
        }
        [HttpPost]
        [Route("screenedvacancy")]
        public IHttpActionResult ScreenedVacancy(Vacancy objVacancy)
        {
            objVacancy.StatusId = (int)VacancyStatuses.Screened;
            objVacancy.MaintUser = User.Identity.Name;
            var result = _vacancyProvider.ChangeVacancyStatusAuthoriser(objVacancy);
            return Ok(result);
        }
        [HttpPost]
        [Route("shortlistedvacancy")]
        public IHttpActionResult ShortlistedVacancy(Vacancy objVacancy)
        {
            objVacancy.StatusId = (int)VacancyStatuses.Shortlisted;
            objVacancy.MaintUser = User.Identity.Name;
            var result = _vacancyProvider.ChangeVacancyStatusAuthoriser(objVacancy);
            return Ok(result);
        }

        [HttpPost]
        [Route("getscreeninglist")]
        public IHttpActionResult GetScreeningList()
        {
            var vacancies = _vacancyProvider.GetVacanciesForScreening().OrderByDescending(x => x.MaintDate);
            foreach (var vacancy in vacancies)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }
            return Ok(vacancies);
        }


        [HttpGet, Route("getvacancystatuses")]
        public IHttpActionResult GetVacancyStatuses()
        {
            var vacancyStatuses = _vacancyStatusProvider.GetVacancyStatuses();
            return Ok(vacancyStatuses);
        }

        [HttpPost]
        [Route("getapprovedlist")]
        public IHttpActionResult GetApprovedList()
        {
            var vacancies = _vacancyProvider.GetApprovedVacancies().OrderByDescending(x => x.MaintDate);
            foreach (var vacancy in vacancies)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }
            return Ok(vacancies);
        }

        [HttpPost]
        [Route("getauthorisedList")]
        public IHttpActionResult GetAuthorisedList()
        {
            var vacancies = _vacancyProvider.GetAuthorizedVacancies().OrderByDescending(x => x.MaintDate);
            foreach (var vacancy in vacancies)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }
            return Ok(vacancies);
        }

        [HttpPost]
        [Route("getcancelledList")]
        public IHttpActionResult GetCancelledList()
        {
            var vacancies = _vacancyProvider.GetCancelledVacancies().OrderByDescending(x => x.MaintDate);
            foreach (var vacancy in vacancies)
            {
                vacancy.VacancyStatus = _vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.Salary = _salariesProvider.getSalarybyId(vacancy.SalaryId);
            }
            return Ok(vacancies);
        }


        [HttpGet]
        [Route("generatereport")]
        //public IHttpActionResult GenerateReport()
        public object GenerateReport()
        {
            var vacancies = _vacancyProvider.GetApprovedVacancies();

            byte[] byteWord = _vacancyProvider.PrintToWord();
            string mediaType = "application/msword";//;"application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            return BinaryToFile(byteWord, mediaType, "ApprovedVacancies.doc");


            //return Ok(vacancies);
        }

        [HttpGet]
        [Route("getgenericquestions")]
        public IHttpActionResult GetGenericQuestions()
        {
            var questions = _genericQuestions.GetAllQuestions();
            return Ok(questions);
        }

        [HttpPost]
        [Route("saveAllGenericQuestions")]
        public IHttpActionResult SaveAllGenericQuestions(List<GenericQuestion> genericQuestions)
        {
            try
            {
                _genericQuestions.SaveAllQuestions(genericQuestions);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }

            return Ok();
        }

        [HttpPost]
        [Route("deleteQuestion")]
        public IHttpActionResult DeleteQuestion(GenericQuestion genericQuestion)
        {
            try
            {
                _genericQuestions.DeleteGenericQuestion(genericQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }

            return Ok();
        }

        [HttpPost]
        [Route("addRequestAllApplications")]
        public IHttpActionResult AddRequestAllApplications(RequestAllApplication requestAllApplication)
        {
            try
            {
                requestAllApplication.MaintDate = DateTime.Now;
                requestAllApplication.MaintUser = User.Identity.Name;
                _requestAllApplications.AddRequestAllApplicationsManager(requestAllApplication);
                var vacancy = _vacancyProvider.GetVacanyByVacancyId(requestAllApplication.VacancyId);

                string subject = "Line Manager Requesting all Applications - " + vacancy.JobTitle;
                string emailContent = "Please note that " + _hrUserProvider.GetUserByUserName(User.Identity.Name.Replace("TREASURY\\","")).FullName + " has requested to see all the applications for " + vacancy.JobTitle;
                emailContent += "\nReason: " + requestAllApplication.Reason;
                emailContent += "\n Go to the link provided http://erecruitment.treasury.gov.za/ and click Screening menu item, click view vacancy, click Send all applications to Hiring Manager\nKind Regards\neRecruitment Team";

                List<int> listUserRoles = new List<int>();
                listUserRoles.Add((int)HRUserRoles.Approver);
                listUserRoles.Add((int)HRUserRoles.Authoriser);
                listUserRoles.Add((int)HRUserRoles.Capturer);

                SentEmailAll(subject, emailContent, listUserRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format("{0} - {1}", ex.Message, ex.StackTrace));
            }

            return Ok();
        }

        public HttpResponseMessage BinaryToFile(byte[] content, string mediaType, string fileName)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            var stream = new System.IO.MemoryStream(content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
            result.Content.Headers.Add("content-disposition", string.Format("attachment; filename={0}", System.Web.HttpUtility.UrlEncode(fileName)));
            result.Content.Headers.Add("Access-Control-Allow-Origin", "*");

            return result;
        }

        private void SentEmailAll(string subject, string emailContent, List<int> listUserRoles)
        {
            try
            {
                var emailService = new EmailExtension();

                var usersRoles = this._hrUserRoleProvider.GetUserRolesByRoleId(listUserRoles);
                foreach (var userId in usersRoles)
                {
                    var user = this._hrUserProvider.GetUserById(userId);
                    if (user.IsActive)
                    {
                        var userEmail = UserExtensions.GetUserEmail(user.HRUserName);
                        if (userEmail != null)
                        {
                            emailService.Recipients.Add(userEmail);
                        }
                    }
                }

                emailService.EmailSubject = subject;
                emailService.EmailContent = emailContent;
                if (emailService.Recipients.Count > 0)
                {
                    emailService.Send();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
