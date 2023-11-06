using StatsSA.eRecruitment.InternalWeb.Extensions;
using StatsSA.eRecruitment.IProvider.Applications;
using StatsSA.eRecruitment.IProvider.VerApplicantProfiles;
using StatsSA.eRecruitment.IProvider.VerApplicantContactDetails;
using StatsSA.eRecruitment.IProvider.VerApplicantAttachments;
using StatsSA.eRecruitment.IProvider.VerApplicantDeclarations;
using StatsSA.eRecruitment.IProvider.VerApplicantExperiences;
using StatsSA.eRecruitment.IProvider.VerApplicantLangProviciencies;
using StatsSA.eRecruitment.IProvider.VerApplicantProhibitions;
using StatsSA.eRecruitment.IProvider.VerApplicantQualifications;
using StatsSA.eRecruitment.IProvider.VerApplicantReferences;
using StatsSA.eRecruitment.IProvider.ContactMethods;
using StatsSA.eRecruitment.IProvider.Languages;
using StatsSA.eRecruitment.IProvider.Races;
using StatsSA.eRecruitment.IProvider.Genders;
using StatsSA.eRecruitment.IProvider.DocumentTypes;
using StatsSA.eRecruitment.IProvider.QualificationTypes;
using StatsSA.eRecruitment.IProvider.LanguageProficiencies;
using StatsSA.eRecruitment.IProvider.ApplicationStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Entities.Enums;
using StatsSA.eRecruitment.InternalWeb.Models;
using System.IO;
using StatsSA.eRecruitment.IProvider.Programmes;
using StatsSA.eRecruitment.IProvider.Institutions;
using StatsSA.eRecruitment.IProvider.ViewApplicantQuestionAnswers;
using StatsSA.eRecruitment.IProvider.HRUsersProvider;
using StatsSA.eRecruitment.IProvider.HRUserRolesProvider;
using StatsSA.eRecruitment.IProvider.HiringManagersVacancy;
using System.Web;
using StatsSA.eRecruitment.IProvider.Vacancies;

namespace StatsSA.eRecruitment.InternalWeb.Controllers
{
    [Authorize, RoutePrefix("api/applications")]
    public class ApplicationsController : ApiController
    {
        #region DECLARATION & INSTATIATING OF IPROVIDERS
        private IApplicationProvider _applicationProvider;
        private IVerApplicantProfileProvider _verApplicantProfileProvider;
        private IVerApplicantAttachmentProvider _verApplicantAttachmentsProvider;
        private IVerApplicantExperienceProvider _verApplicantExperienceProvider;
        private IVerApplicantQualificationsProvider _verApplicantQualificationsProvider;
        private IVerApplicantContactDetailsProvider _verApplicantContactDatailsProvider;
        private IVerApplicantDeclarationProvider _verApplicantDeclarationProvider;
        private IVerApplicantProhibitionProvider _verApplicantProhibitionProvider;
        private IVerApplicantReferenceProvider _verApplicantReferenceProvider;
        private IVerApplicantLangProviciencyProvider _verApplicantLangProviciencyProvider;
        private IContactMethodProvider _contactMethodProvider;
        private ILanguageProvider _languageProvider;
        private IGenderProvider _genderProvider;
        private IRaceProvider _raceProvider;
        private IQualificationTypeProvider _qualificationTypeProvider;
        private ILanguageProficiencyProvider _languageProficiencyProvider;
        private IApplicationStatusProvider _applicationStatusProvider;
        private IDocumentTypeProvider _documentTypeProvider;
        private IProgrammeProvider _programmeProvider;
        private IInstitutionProvider _institutionProvider;
        private IViewApplicantQuestionAnswersProvider _viewApplicantQuestionAnswersProvider;
        private IHRUserProvider _hrUserProvider;
        private IHRUserRoleProvider _hrUserRoleProvider;
        private IHiringManagersVacancyAccessProvider _hiringManagersVacancyAccessProvider;
        private IVacancyProvider _vacancyProvider;

        public ApplicationsController(
                                IApplicationProvider applicationProvider,
                                IVerApplicantProfileProvider verApplicantProfileProvider,
                                IVerApplicantExperienceProvider verApplicantExperienceProvider,
                                IVerApplicantQualificationsProvider verApplicantQualificationsProvider,
                                IVerApplicantAttachmentProvider verApplicantAttachmentsProvider,
                                IVerApplicantContactDetailsProvider verApplicantContactDatailsProvider,
                                IVerApplicantDeclarationProvider verApplicantDeclarationProvider,
                                IVerApplicantProhibitionProvider verApplicantProhibitionProvider,
                                IVerApplicantReferenceProvider verApplicantReferenceProvider,
                                IVerApplicantLangProviciencyProvider verApplicantLangProviciencyProvider,
                                ILanguageProvider languageProvider,
                                IContactMethodProvider contactMethodProvider,
                                IGenderProvider genderProvider,
                                IRaceProvider raceProvider,
                                IQualificationTypeProvider qualificationTypeProvider,
                                ILanguageProficiencyProvider languageProficiencyProvider,
                                IApplicationStatusProvider applicationStatusProvider,
                                IDocumentTypeProvider documentTypeProvider,
                                IProgrammeProvider programmeProvider,
                                IInstitutionProvider institutionProvider,
                                IViewApplicantQuestionAnswersProvider viewApplicantQuestionAnswersProvider,
                                IHRUserProvider hrUserProvider,
                                IHRUserRoleProvider hrUserRoleProvider,
                                IHiringManagersVacancyAccessProvider hiringManagersVacancyAccessProvider,
                                IVacancyProvider vacancyProvider
            )
        {
            _applicationProvider = applicationProvider;
            _verApplicantProfileProvider = verApplicantProfileProvider;
            _verApplicantExperienceProvider = verApplicantExperienceProvider;
            _verApplicantQualificationsProvider = verApplicantQualificationsProvider;
            _verApplicantAttachmentsProvider = verApplicantAttachmentsProvider;
            _verApplicantContactDatailsProvider = verApplicantContactDatailsProvider;
            _verApplicantDeclarationProvider = verApplicantDeclarationProvider;
            _verApplicantProhibitionProvider = verApplicantProhibitionProvider;
            _verApplicantLangProviciencyProvider = verApplicantLangProviciencyProvider;
            _verApplicantReferenceProvider = verApplicantReferenceProvider;
            _languageProvider = languageProvider;
            _contactMethodProvider = contactMethodProvider;
            _genderProvider = genderProvider;
            _raceProvider = raceProvider;
            _qualificationTypeProvider = qualificationTypeProvider;
            _languageProficiencyProvider = languageProficiencyProvider;
            _applicationStatusProvider = applicationStatusProvider;
            _documentTypeProvider = documentTypeProvider;
            _programmeProvider = programmeProvider;
            _institutionProvider = institutionProvider;
            _viewApplicantQuestionAnswersProvider = viewApplicantQuestionAnswersProvider;
            _hrUserProvider = hrUserProvider;
            _hrUserRoleProvider = hrUserRoleProvider;
            _hiringManagersVacancyAccessProvider = hiringManagersVacancyAccessProvider;
            _vacancyProvider = vacancyProvider;

    }
        #endregion

        /*  [HttpPost]
          [Route("getrankedapplications")]
          public IHttpActionResult GetRankedApplications(Vacancy objVacancy)
          {
              List<RecievedApplications> RankedApplications = new List<RecievedApplications>();
              try
              {
                  int noApplications = 0;
                  var applications = _applicationProvider.GetRankedApplications(objVacancy.VacancyId).Take(100);

                  foreach (var application in applications)
                  {

                      application.ApplicationStatus = _applicationStatusProvider.GetApplicationStatus(application.ApplicationStatusId);
                      application.ApplicationRequirement = _applicationProvider.GetApplicationRequirement(application.ApplicationId);
                      application.ApplicationRequirement.Programme = _programmeProvider.GetProgrammeById(application.ApplicationRequirement.ProgrammeId ?? 0);

                      var tempRankedAppl = new RankedApplication
                      {
                          ApplicantApplication = application,
                          Profile = _verApplicantProfileProvider.GetVerAppProfileById(application.VerApplicantProfileId),
                          Score = RankingExtensions.CalculateScore(application.ApplicationRequirement)
                      };


                      VerApplicantProfile applicantProfile = _verApplicantProfileProvider.GetVerAppProfileById(tempRankedAppl.Profile.VerApplicantProfileId);

                      // GET APPLICANT ATTACHMENTS WITHOUT DATA
                      var attachments = _verApplicantAttachmentsProvider.GetVerApplicantAttachments(tempRankedAppl.Profile.VerApplicantProfileId);

                      var tempRankedApplNew = new RecievedApplications
                      {
                          ApplicantApplication = tempRankedAppl.ApplicantApplication,
                          Profile = tempRankedAppl.Profile,
                          Score = tempRankedAppl.Score,
                          hasAttachments = attachments.Count() > 0 ? true : false,
                          NumberofApplications = noApplications
                      };

                      RankedApplications.Add(tempRankedApplNew);
                  }
              }
              catch (Exception ex)
              {
                  var exception = ex;
              }

              return Ok(RankedApplications.OrderByDescending(x => x.ApplicantApplication.ApplicationRequirement.OverallAverage));           

          }*/

        /** [HttpPost]
    [Route("getapplicationbyemail")]
    public IHttpActionResult GetApplicationByEmail(Vacancy objVacancy)
    {
        List<RecievedApplications> RankedApplications = new List<RecievedApplications>();
        try
        {
            int noApplications = 0;
            var applications = _applicationProvider.GetApplicationByEmail(objVacancy.VacancyId, objVacancy.MaintUser);

            foreach (var application in applications)
            {

                application.ApplicationStatus = _applicationStatusProvider.GetApplicationStatus(application.ApplicationStatusId);
                application.ApplicationRequirement.Programme = _programmeProvider.GetProgrammeById(application.ApplicationRequirement.ProgrammeId ?? 0);
                                    
                var tempRankedAppl = new RankedApplication
                {
                    ApplicantApplication = application,
                    Profile = _verApplicantProfileProvider.GetVerAppProfileById(application.VerApplicantProfileId),
                    Score = RankingExtensions.CalculateScore(application)
                };


                VerApplicantProfile applicantProfile = _verApplicantProfileProvider.GetVerAppProfileById(tempRankedAppl.Profile.VerApplicantProfileId);

                // GET APPLICANT ATTACHMENTS WITHOUT DATA
                var attachments = _verApplicantAttachmentsProvider.GetVerApplicantAttachments(tempRankedAppl.Profile.VerApplicantProfileId);

                var tempRankedApplNew = new RecievedApplications
                {
                    ApplicantApplication = tempRankedAppl.ApplicantApplication,
                    Profile = tempRankedAppl.Profile,
                    Score = tempRankedAppl.Score,
                    hasAttachments = attachments.Count() > 0 ? true : false,
                    NumberofApplications = noApplications
                };

                RankedApplications.Add(tempRankedApplNew);
            }
        }
        catch(Exception ex)
        {
            var exception = ex;
        }

        return Ok(RankedApplications.OrderByDescending(x => x.ApplicantApplication.ApplicationRequirement.OverallAverage));
    }**/

        [HttpPost]
        [Route("getallapplications")]
        public IHttpActionResult GetAllApplications(Vacancy objVacancy)
        {
            var applications = _applicationProvider.GetAllApplications(objVacancy.VacancyId);
            var listInternships = new List<InternshipApplication>();

            foreach (var item in applications)
            {
                var user = _hrUserProvider.GetUserByUserName(item.MaintUser.ToLower().Replace("treasury\\", ""));
                var fullName = user == null ? "" : user.FullName;
                item.MaintUser = fullName;

                if (item.JobTitle.ToLower().Contains("internship"))
                {
                    var internUser = _verApplicantProfileProvider.GetVerAppProfileById(item.VerApplicantProfileId);
                    item.MaintUser = internUser.IdentityNumber;

                    var allQuestions = _viewApplicantQuestionAnswersProvider.GetAllApplicantQuestionAnswers(item.ApplicationId, item.VacancyId);
                    var internship = new InternshipApplication();

                    foreach (var question in allQuestions)
                    {

                        if (question.SpecificAnswer != null)
                        {
                            if (question.SpecificQuestion.ToLower().Contains(nameof(internship.Programme).ToLower()))
                            {
                                internship.Programme = question.SpecificAnswer;
                            }

                            if (question.SpecificQuestion.ToLower().Contains(nameof(internship.Average).ToLower()))
                            {
                                internship.Average = question.SpecificAnswer;
                            }
                        }
                    }

                    if (internship.Programme != null && internship.Average != null)
                    {
                        internship.Application = item;
                        listInternships.Add(internship);
                    }
                }                
            }

            if (listInternships.Count > 0)
                return Ok(listInternships);
            else
                return Ok(applications);

        }

        [HttpPost]
        [Route("getApplicationsHiringManager")]
        public IHttpActionResult GetApplicationsHiringManager()
        {
            string userName = User.Identity.Name;
            userName = userName.Replace("TREASURY\\", "");
            var user = _hrUserProvider.GetActiveHRUserByUserName(userName);
            List<ListOfApplication> listOfApplications = new List<ListOfApplication>();

            if(user != null)
            {
                var obj = _hiringManagersVacancyAccessProvider.GetHiringManagersVacancyAccess().Where(x => x.ManagerUserId == user.HRUserId);

                foreach (var item in obj)
                {
                    IEnumerable<ListOfApplication> applications = null;
                    if (Convert.ToBoolean(item.SendAllApplicationsHiringManager))
                    {
                        applications = _applicationProvider.GetAllApplications(item.VacancyId)
                                       .Where(x =>
                                       x.ApplicationStatusDesc == Enum.Parse(typeof(ApplicationStatuses), ApplicationStatuses.HRShortlisted.ToString()).ToString()
                                       || x.ApplicationStatusDesc == Enum.Parse(typeof(ApplicationStatuses), ApplicationStatuses.HiringManagerShortlisted.ToString()).ToString()
                                       || x.ApplicationStatusDesc == Enum.Parse(typeof(ApplicationStatuses), ApplicationStatuses.HiringManagerRejected.ToString()).ToString()
                                       || x.ApplicationStatusDesc == Enum.Parse(typeof(ApplicationStatuses), ApplicationStatuses.HRRejected.ToString()).ToString());
                    }
                    else
                    {
                         applications = _applicationProvider.GetAllApplications(item.VacancyId)
                                        .Where(x =>
                                        x.ApplicationStatusDesc == Enum.Parse(typeof(ApplicationStatuses), ApplicationStatuses.HRShortlisted.ToString()).ToString()
                                        || x.ApplicationStatusDesc == Enum.Parse(typeof(ApplicationStatuses), ApplicationStatuses.HiringManagerShortlisted.ToString()).ToString()
                                        || x.ApplicationStatusDesc == Enum.Parse(typeof(ApplicationStatuses), ApplicationStatuses.HiringManagerRejected.ToString()).ToString());
                    }
                    if (applications.Count() > 0)
                    {
                        foreach (var apps in applications)
                        {
                            listOfApplications.Add(apps);
                        }
                        
                    }
                }               
                
            }

            return Ok(listOfApplications);
        }

        [HttpPost]
        [Route("sendAllApplicationsHiringManager")]
        public IHttpActionResult SendAllApplicationsHiringManager(object vacancyId)
        {
            var obj = _hiringManagersVacancyAccessProvider.GetHiringManagersVacancyAccess().Where(x => x.VacancyId == Convert.ToInt32(vacancyId));

            if (obj.Any())
            {
                foreach (var item in obj)
                {
                    item.SendAllApplicationsHiringManager = true;
                    _hiringManagersVacancyAccessProvider.UpdateHiringManagersVacancyAccess(item);

                    var link = string.Format("<a href='{0}/#/shortlistedApplications'> Click here </a>", HttpContext.Current.Request.UrlReferrer);
                    string subject = "eRecruitment - All applications";
                    string content = "";
                    content += string.Format("<p>HR has sent you a list of all the applications. {0}  for further shortlisting. </p><br />", link);
                    //content += "<p>Click on the link provided</p><br />";
                    //content += string.Format("<a href='{0}/#/shortlistedApplications'> Click here </a>", HttpContext.Current.Request.UrlReferrer);

                    List<HRUser> users = new List<HRUser>();
                    users.Add(_hrUserProvider.GetUserById(Convert.ToInt32(item.ManagerUserId)));
                    
                    HiringmanagerVacancyAccess access = new HiringmanagerVacancyAccess
                    {
                        Users = users,
                        VacancyId = Convert.ToInt32(vacancyId)
                    };

                    SentEmail(access, subject, content);
                }   
            }
            else
            {
                Exception noActiveUserFound = new Exception("Hiring manager should ask for all the applications first");
                throw (noActiveUserFound);
            }           

            return Ok();
        }

        [HttpPost]
        [Route("getprogrammes")]
        public IHttpActionResult GetProgrammes()
        {
            var programmes = _programmeProvider.GetAllProgrammes();
            return Ok(programmes);
        }


      /**  [HttpPost]
        [Route("getapplicantprofile")]
        public IHttpActionResult GetApplicantProfile(RankedApplication objApplication)
        {
            try
            {
                // GET APPLICANT PROFILE
                VerApplicantProfile applicantProfile = _verApplicantProfileProvider.GetVerAppProfileById(objApplication.Profile.VerApplicantProfileId);

                // GET APPLICANT ATTACHMENTS WITHOUT DATA
                var attachments = _verApplicantAttachmentsProvider.GetVerApplicantAttachments(objApplication.Profile.VerApplicantProfileId);
                foreach (var attachment in attachments)
                {
                    attachment.DocumentType = this._documentTypeProvider.GetDocumentTypeById(attachment.DocumentTypeId);
                    attachment.DocumentData = null;
                    applicantProfile.VerApplicantAttachments.Add(attachment);
                }

                // GET APPLICANT CONTACT DETAILS
                VerApplicantContactDetail contactDetail = _verApplicantContactDatailsProvider.GetVerApplicantContactDetail(objApplication.Profile.VerApplicantProfileId);
                contactDetail.Language = _languageProvider.GetLanguageByLanguageId(contactDetail.LanguageId);
                contactDetail.ContactMethod = _contactMethodProvider.GetContactMethodById(contactDetail.ContactMethodId);
                applicantProfile.VerApplicantContactDetails.Add(contactDetail);

                // GET APPLICANT DECLARATION
                applicantProfile.VerApplicantDeclaration = _verApplicantDeclarationProvider.GetVerApplicantDeclaration(objApplication.Profile.VerApplicantProfileId);

                // GET APPLICANT EXPERIENCE
                var experience = _verApplicantExperienceProvider.GetVerApplicantExperience(objApplication.Profile.VerApplicantProfileId);
                foreach (var exp in experience)
                {
                    applicantProfile.VerApplicantExperiences.Add(exp);
                }

                // GET APPLICANT LANGUAGE PROFICIENCIES
                var proficiencies = _verApplicantLangProviciencyProvider.GetVerApplicantProficiencies(objApplication.Profile.VerApplicantProfileId);
                foreach (var proficiency in proficiencies)
                {
                    proficiency.Language = _languageProvider.GetLanguageByLanguageId(proficiency.LanguageId);
                    proficiency.LanguageProficiency = _languageProficiencyProvider.GetLanguageProficiencyById(proficiency.ReadProficiencyId);
                    proficiency.LanguageProficiency1 = _languageProficiencyProvider.GetLanguageProficiencyById(proficiency.SpeakProficiencyId);
                    proficiency.LanguageProficiency2 = _languageProficiencyProvider.GetLanguageProficiencyById(proficiency.WriteReadProficiencyId);
                    applicantProfile.VerApplicantLangProficiencies.Add(proficiency);
                }

                // GET APPLICANT PUBLIC SERVICE PROHIBITION
                applicantProfile.VerApplicantPubServProhibition = _verApplicantProhibitionProvider.GetVerApplicantProhibition(objApplication.Profile.VerApplicantProfileId);

                // GET APPLICANT QUALIFICATIONS
                var qualifications = _verApplicantQualificationsProvider.GetVerApplicantQualification(objApplication.Profile.VerApplicantProfileId);
                foreach (var qualification in qualifications)
                {
                    qualification.QualificationType = _qualificationTypeProvider.GetQualificationTypeById(qualification.QualificationTypeId);
                    qualification.Institution = _institutionProvider.GetInstitutionById(qualification.InstitutionId);
                    applicantProfile.VerApplicantQualifications.Add(qualification);
                }

                // GET APPLICANT REFERENCES
                var references = _verApplicantReferenceProvider.GetVerApplicantReferences(objApplication.Profile.VerApplicantProfileId);
                foreach (var reference in references)
                {
                    applicantProfile.VerApplicantReferences.Add(reference);
                }
                // GET APPLICANT GENDER & RACE
                applicantProfile.Gender = this._genderProvider.GetGenderById(applicantProfile.GenderId);
                applicantProfile.Race = this._raceProvider.GetRaceById(applicantProfile.RaceId);


                // GET APPLICATION REQUIREMENT
                objApplication.ApplicantApplication.ApplicationRequirement = _applicationProvider.GetApplicationRequirement(objApplication.ApplicantApplication.ApplicationId);
                objApplication.ApplicantApplication.ApplicationRequirement.Programme = _programmeProvider.GetProgrammeById(objApplication.ApplicantApplication.ApplicationRequirement.ProgrammeId ?? 0);
                objApplication.Profile = applicantProfile;
            }
            catch { }

            return Ok(objApplication);
        }**/

        [HttpPost]
        [Route("getapplicantprofile")]
        public IHttpActionResult GetApplicantProfile(ListOfApplication objApplication)
        {
            RankedApplication application = new RankedApplication();

            try
            {
               
                // GET APPLICANT PROFILE
                VerApplicantProfile applicantProfile = _verApplicantProfileProvider.GetVerAppProfileById(objApplication.VerApplicantProfileId);

                // GET APPLICANT ATTACHMENTS WITHOUT DATA
                var attachments = _verApplicantAttachmentsProvider.GetVerAttachments(objApplication.VerApplicantProfileId);
                foreach (var attachment in attachments)
                {
                    if (applicantProfile.VerAttachments.Count > 0)
                    {
                        var docType = applicantProfile.VerAttachments.Where(x => x.DocumentTypeId == attachment.DocumentTypeId);

                        if (!docType.Any())
                        {
                            attachment.DocumentType = this._documentTypeProvider.GetDocumentTypeById(attachment.DocumentTypeId);
                            applicantProfile.VerAttachments.Add(attachment);
                        }
                    }
                    else
                    {
                        attachment.DocumentType = this._documentTypeProvider.GetDocumentTypeById(attachment.DocumentTypeId);
                        applicantProfile.VerAttachments.Add(attachment);
                    }
                }

                // GET APPLICANT CONTACT DETAILS
                VerApplicantContactDetail contactDetail = _verApplicantContactDatailsProvider.GetVerApplicantContactDetail(objApplication.VerApplicantProfileId);
                contactDetail.Language = _languageProvider.GetLanguageByLanguageId(contactDetail.LanguageId);
                contactDetail.ContactMethod = _contactMethodProvider.GetContactMethodById(contactDetail.ContactMethodId);
                applicantProfile.VerApplicantContactDetails.Add(contactDetail);

                // GET APPLICANT DECLARATION
                applicantProfile.VerApplicantDeclaration = _verApplicantDeclarationProvider.GetVerApplicantDeclaration(objApplication.VerApplicantProfileId);

                // GET APPLICANT EXPERIENCE
                var experience = _verApplicantExperienceProvider.GetVerApplicantExperience(objApplication.VerApplicantProfileId);
                foreach (var exp in experience)
                {
                    applicantProfile.VerApplicantExperiences.Add(exp);
                }

                // GET APPLICANT LANGUAGE PROFICIENCIES
                var proficiencies = _verApplicantLangProviciencyProvider.GetVerApplicantProficiencies(objApplication.VerApplicantProfileId);
                foreach (var proficiency in proficiencies)
                {
                    proficiency.Language = _languageProvider.GetLanguageByLanguageId(proficiency.LanguageId);
                    proficiency.LanguageProficiency = _languageProficiencyProvider.GetLanguageProficiencyById(proficiency.ReadProficiencyId);
                    proficiency.LanguageProficiency1 = _languageProficiencyProvider.GetLanguageProficiencyById(proficiency.SpeakProficiencyId);
                    proficiency.LanguageProficiency2 = _languageProficiencyProvider.GetLanguageProficiencyById(proficiency.WriteReadProficiencyId);
                    applicantProfile.VerApplicantLangProficiencies.Add(proficiency);
                }

                // GET APPLICANT PUBLIC SERVICE PROHIBITION
                applicantProfile.VerApplicantPubServProhibition = _verApplicantProhibitionProvider.GetVerApplicantProhibition(objApplication.VerApplicantProfileId);

                // GET APPLICANT QUALIFICATIONS
                var qualifications = _verApplicantQualificationsProvider.GetVerApplicantQualification(objApplication.VerApplicantProfileId);
                foreach (var qualification in qualifications)
                {
                    qualification.QualificationType = _qualificationTypeProvider.GetQualificationTypeById(qualification.QualificationTypeId);
                    qualification.Institution = _institutionProvider.GetInstitutionById(qualification.InstitutionId);
                    applicantProfile.VerApplicantQualifications.Add(qualification);
                }

                // GET APPLICANT REFERENCES
                var references = _verApplicantReferenceProvider.GetVerApplicantReferences(objApplication.VerApplicantProfileId);
                foreach (var reference in references)
                {
                    applicantProfile.VerApplicantReferences.Add(reference);
                }
                // GET APPLICANT GENDER & RACE
                applicantProfile.Gender = this._genderProvider.GetGenderById(applicantProfile.GenderId);
                applicantProfile.Race = this._raceProvider.GetRaceById(applicantProfile.RaceId);

                //GET APPLICATION
                application.ApplicantApplication = _applicationProvider.GetApplicantApplication(objApplication.VacancyId, objApplication.ApplicationId);
                
                if (application.ApplicantApplication != null)
                {
                    application.ApplicantApplication.ApplicationStatus = _applicationStatusProvider.GetApplicationStatus(application.ApplicantApplication.ApplicationStatusId);
                    // GET APPLICATION QUESTIONS AND ANSWERS
                    application.AllApplicantQuestionAnswers = _viewApplicantQuestionAnswersProvider.GetAllApplicantQuestionAnswers(objApplication.ApplicationId, application.ApplicantApplication.VacancyId);
                    //application.ApplicantApplication.Requirement.Programme = _programmeProvider.GetProgrammeById(objApplication.ProgrammeId);
                    // var requirements = _viewApplicantQuestionAnswersProvider.GetAllApplicantQuestionAnswers(application.ApplicantApplication.VerApplicantProfileId);
                    application.Profile = applicantProfile;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(application);
        }

        [HttpPost]
        [Route("inProcess")]
        public IHttpActionResult InProcess(RankedApplication objApplication)
        {
            objApplication.ApplicantApplication.ApplicationStatus = null;
            objApplication.ApplicantApplication.Requirements = null;
            objApplication.ApplicantApplication.Vacancy = null;
            objApplication.ApplicantApplication.MaintUser = User.Identity.Name;
            objApplication.ApplicantApplication.MaintDate = DateTime.Now;
            objApplication.ApplicantApplication.ApplicationStatusId = (int)Entities.Enums.ApplicationStatuses.InProgress;
            _applicationProvider.UpdateApplicationStatus(objApplication.ApplicantApplication);
            return Ok(objApplication);
        }

        [HttpPost]
        [Route("successful")]
        public IHttpActionResult Successful(RankedApplication objApplication)
        {
            objApplication.ApplicantApplication.ApplicationStatus = null;
            objApplication.ApplicantApplication.Requirements = null;
            objApplication.ApplicantApplication.Vacancy = null;
            objApplication.ApplicantApplication.MaintUser = User.Identity.Name;
            objApplication.ApplicantApplication.MaintDate = DateTime.Now;
            objApplication.ApplicantApplication.ApplicationStatusId = (int)Entities.Enums.ApplicationStatuses.Successful;
            _applicationProvider.UpdateApplicationStatus(objApplication.ApplicantApplication);
            return Ok(objApplication);
        }

        [HttpPost]
        [Route("unSuccessful")]
        public IHttpActionResult UnSuccessful(RankedApplication objApplication)
        {
            objApplication.ApplicantApplication.ApplicationStatus = null;
            objApplication.ApplicantApplication.Requirements = null;
            objApplication.ApplicantApplication.Vacancy = null;
            objApplication.ApplicantApplication.MaintUser = User.Identity.Name;
            objApplication.ApplicantApplication.MaintDate = DateTime.Now;

            objApplication.ApplicantApplication.ApplicationStatusId = (int)Entities.Enums.ApplicationStatuses.Unsuccessful;

            _applicationProvider.UpdateApplicationStatus(objApplication.ApplicantApplication);

            SendUnsuccessfullEmail(objApplication.Profile.MaintUser, objApplication.Profile.FirstNames, objApplication.Profile.Surname);

            return Ok(objApplication);
        }

        [HttpPost]
        [Route("hrRejected")]
        public IHttpActionResult HRRejected(RankedApplication objApplication)
        {
            objApplication.ApplicantApplication.ApplicationStatus = null;
            objApplication.ApplicantApplication.Requirements = null;
            objApplication.ApplicantApplication.Vacancy = null;
            objApplication.ApplicantApplication.MaintUser = User.Identity.Name;
            objApplication.ApplicantApplication.MaintDate = DateTime.Now;

            objApplication.ApplicantApplication.ApplicationStatusId = (int)Entities.Enums.ApplicationStatuses.HRRejected;

            _applicationProvider.UpdateApplicationStatus(objApplication.ApplicantApplication);

            return Ok(objApplication);
        }

        [HttpPost]
        [Route("interview")]
        public IHttpActionResult Interview(RankedApplication objApplication)
        {
            objApplication.ApplicantApplication.ApplicationStatus = null;
            objApplication.ApplicantApplication.Requirements = null;
            objApplication.ApplicantApplication.Vacancy = null;
            objApplication.ApplicantApplication.MaintUser = User.Identity.Name;
            objApplication.ApplicantApplication.MaintDate = DateTime.Now;

            objApplication.ApplicantApplication.ApplicationStatusId = (int)Entities.Enums.ApplicationStatuses.ScheduledInterview;

            _applicationProvider.UpdateApplicationStatus(objApplication.ApplicantApplication);
            return Ok(objApplication);
        }

        [HttpPost]
        [Route("shortlistappliction")]
        public IHttpActionResult ShortListApplication(RankedApplication objApplication)
        {
            //objApplication.ApplicantApplication.ApplicationStatus = null;
            objApplication.ApplicantApplication.Requirements = null;
            objApplication.ApplicantApplication.Vacancy = null;
            objApplication.ApplicantApplication.MaintUser = User.Identity.Name;
            objApplication.ApplicantApplication.MaintDate = DateTime.Now;

            objApplication.ApplicantApplication.ApplicationStatusId = (int)Enum.Parse(typeof(ApplicationStatuses), objApplication.ApplicantApplication.ApplicationStatus.ApplicationStatusDesc);
            objApplication.ApplicantApplication.ApplicationStatus.ApplicationStatusId = objApplication.ApplicantApplication.ApplicationStatusId;
            //objApplication.ApplicantApplication.ApplicationStatusId =  (int)Entities.Enums.ApplicationStatuses.Shortlisted;
            _applicationProvider.UpdateApplicationStatus(objApplication.ApplicantApplication);

            if(objApplication.ApplicantApplication.ApplicationStatus.ApplicationStatusDesc == "HiringManagerShortlisted")
            {
                SendShorlistedEmail(objApplication.Profile.MaintUser, objApplication.Profile.FirstNames, objApplication.Profile.Surname);
            }
            return Ok(objApplication);
        }

        [HttpPost]
        [Route("getAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            return Ok(_hrUserProvider.GetListOfUsers());
        }

        [HttpPost]
        [Route("sentEmail")]
        public IHttpActionResult SentEmail(HiringmanagerVacancyAccess application)
        {
            var vacancy = _vacancyProvider.GetvacancyById(application.VacancyId);
            string subject = "HR Shortlisted Vacancies - " + vacancy.JobTitle;
            string content = "";
            content += "<p>Please note that HR has sent you a list of shorlisted applications for further shortlisting</p><br />";
            content += "\n Go to the link provided http://erecruitment.treasury.gov.za/ and click 'List of HR Shortlisted Applications' menu item, then select an application you want to shortlist.\n\n\nKind Regards\n\n\neRecruitment Team";
            content += string.Format("<a href='{0}/#/shortlistedApplications'> link </a>", HttpContext.Current.Request.UrlReferrer);

            SentEmail(application, subject, content);

            //for (int i = 0; i < application.Users.Count; i++)
            //{
            //    var hiringManagerAccess = new HiringManagersVacancyAccess
            //    {
            //        ManagerUserId = application.Users[i].HRUserId,
            //        VacancyId = application.VacancyId,                    
            //        MaintDate = DateTime.Now
            //    };
            //    hiringManagerAccess.MaintUser = User.Identity.Name; 

            //    _hiringManagersVacancyAccessProvider.InsertHiringManagersVacancyAccess(hiringManagerAccess);

            //    var emailAddress = UserExtensions.GetUserEmail(application.Users[i].HRUserName);

            //    if (emailAddress != null)
            //    {
            //        EmailExtension email = new EmailExtension();
            //        email.Recipients.Add(emailAddress);
            //        email.useHtml = true;
            //        email.EmailSubject += "eRecruitment - HR shortlisted applications";
            //        email.EmailContent += "<p>HR has send you a list of shorlisted applications for further shortlisting</p><br />";
            //        email.EmailContent += "<p>Click on the link provided</p><br />";
            //        email.EmailContent += string.Format("<a href='{0}/#/shortlistedApplications'> link </a>", HttpContext.Current.Request.UrlReferrer);
            //        email.Send();
            //    }
            //}

            return Ok(application);
        }


        [HttpGet]
        [Route("getapplicationattachment/{attachmentId}")]
        public object getApplicationAttachment(int attachmentId)
        {
            var attachment = _verApplicantAttachmentsProvider.GetVerAttachmentById(attachmentId);
            var content = System.IO.File.ReadAllBytes(attachment.FilePath);
            return BinaryToFile(content, attachment.DocumentFormat, attachment.OriginalFileName);
        }

        private void SentEmail(HiringmanagerVacancyAccess application, string subject, string content)
        {
            for (int i = 0; i < application.Users.Count; i++)
            {
                var hiringManagerAccess = new HiringManagersVacancyAccess
                {
                    ManagerUserId = application.Users[i].HRUserId,
                    VacancyId = application.VacancyId,
                    MaintDate = DateTime.Now
                };
                hiringManagerAccess.MaintUser = User.Identity.Name;

                var userExists = _hiringManagersVacancyAccessProvider.GetHiringManagersVacancyAccess()
                                    .Where(x => x.ManagerUserId == hiringManagerAccess.ManagerUserId && x.VacancyId == hiringManagerAccess.VacancyId);

                if(!userExists.Any())
                {
                    _hiringManagersVacancyAccessProvider.InsertHiringManagersVacancyAccess(hiringManagerAccess);
                }               

                var emailAddress = UserExtensions.GetUserEmail(application.Users[i].HRUserName);

                if (emailAddress != null)
                {
                    EmailExtension email = new EmailExtension();
                    email.Recipients.Add(emailAddress);
                    email.useHtml = true;
                    email.EmailSubject = subject;
                    email.EmailContent = content;
                    email.Send();
                }
            }
        }

        public HttpResponseMessage BinaryToFile(byte[] content, string mediaType, string fileName)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            var stream = new MemoryStream(content);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
            result.Content.Headers.Add("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
            //result.Content.Headers.Add("content-disposition", string.Format(@"attachment; filename=\{0}", fileName.Replace(" ","")));
            result.Content.Headers.Add("Access-Control-Allow-Origin", "*");

            return result;
        }

        private void SendUnsuccessfullEmail(string recepient, string firstName, string surname)
        {
            SendEmail(recepient, firstName, surname, "Unsuccessful");
        }

        private void SendShorlistedEmail(string recepient, string firstName, string surname)
        {
            SendEmail(recepient, firstName, surname, "LineManagerShortlist");
        }

        private void SendEmail(string recepient, string firstName, string surname, string template)
        {
            var directories = Directory.GetDirectories(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")));

            foreach (var item in directories)
            {
                if (item.Contains(template))
                {
                    var fileName = Directory.GetFiles(item);
                    var path = fileName[0];
                    var body = CreateEmailBody(firstName + " " + surname, path);

                    Helper.SendEmail(recepient, "eRecruitment - Application Unsuccessful", body);
                }
            }
        }

        private static string CreateEmailBody(string fullname, string mappedPath)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(mappedPath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{name}", fullname);
            return body;
        }
    }
}
