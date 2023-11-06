using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.Applications;
using StatsSA.eRecruitment.IProvider.Users;
using StatsSA.eRecruitment.IProvider.ApplicantProfiles;
using StatsSA.eRecruitment.IProvider.ApplicantAttachments;
using StatsSA.eRecruitment.IProvider.Vacancies;
using StatsSA.eRecruitment.IProvider.VacancyStatuses;
using StatsSA.eRecruitment.IProvider.ApplicationStatuses;
using StatsSA.eRecruitment.IProvider.ApplicantContactDetails;
using StatsSA.eRecruitment.IProvider.ApplicantDeclarations;
using StatsSA.eRecruitment.IProvider.ApplicantExperiences;
using StatsSA.eRecruitment.IProvider.ApplicantLanguageProficiencies;
using StatsSA.eRecruitment.IProvider.ApplicantProhibition;
using StatsSA.eRecruitment.IProvider.ApplicantQualifications;
using StatsSA.eRecruitment.IProvider.ApplicantReferences;
using StatsSA.eRecruitment.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using StatsSA.eRecruitment.IProvider.Programmes;
using System.Globalization;
using System.IO;
using StatsSA.eRecruitment.WebApi.Models;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [ RoutePrefix("api/application")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplicationController : ApiController
    {
        #region DECLARATION AND INSTANTIATION OF INTERFACES
        private IApplicationProvider _applicationProvider;
        private IUserProvider _userProvider;
        private IApplicantProfileProvider _applicantProfileProvider;
        private IVacancyProvider _vacancyProvider;
        private IVacancyStatusProvider _vacancyStatusProvider;
        private IApplicationStatusProvider _applicationStatusProvider;
        private IApplicantAttachementsProvider _applicantAttachmentProvider;
        private IApplicantContactDetailsProvider _applicantContactDetailsProvider;
        private IApplicantDeclarationProvider _applicantDeclarationProvider;
        private IApplicantExperienceProvider _applicantExperienceProvider;
        private IApplicantLangProviciencyProvider _applicantLangProviciencyProvider;
        private IApplicantProhibitionProvider _applicantProhibitionProvider;
        private IApplicantQualificationsProvider _applicantQualificationsProvider;
        private IApplicantReferenceProvider _applicantReferenceProvider;
        private IProgrammeProvider _programmeProvider;

        public ApplicationController(IApplicationProvider applicationProvider, 
            IUserProvider userProvider, 
            IApplicantProfileProvider applicantProfileProvider,
            IVacancyProvider vacancyProvider,
            IVacancyStatusProvider vacancyStatusProvider,
            IApplicationStatusProvider applicationStatusProvider,
            IApplicantAttachementsProvider applicantAttachmentProvider,
            IApplicantContactDetailsProvider applicantContactDetailsProvider,
            IApplicantDeclarationProvider applicantDeclarationProvider,
            IApplicantExperienceProvider applicantExperienceProvider,
            IApplicantLangProviciencyProvider applicantLangProviciencyProvider,
            IApplicantProhibitionProvider applicantProhibitionProvider,
            IApplicantQualificationsProvider applicantQualificationsProvider,
            IApplicantReferenceProvider applicantReferenceProvider,
            IProgrammeProvider programmeProvider)
        {
            _applicationProvider = applicationProvider;
            _userProvider = userProvider;
            _applicantProfileProvider = applicantProfileProvider;
            _vacancyProvider = vacancyProvider;
            _vacancyStatusProvider = vacancyStatusProvider;
            _applicationStatusProvider = applicationStatusProvider;
            _applicantAttachmentProvider = applicantAttachmentProvider;
            _applicantContactDetailsProvider = applicantContactDetailsProvider;
            _applicantDeclarationProvider = applicantDeclarationProvider;
            _applicantExperienceProvider = applicantExperienceProvider;
            _applicantLangProviciencyProvider = applicantLangProviciencyProvider;
            _applicantProhibitionProvider = applicantProhibitionProvider;
            _applicantReferenceProvider = applicantReferenceProvider;
            _applicantQualificationsProvider = applicantQualificationsProvider;
            _programmeProvider = programmeProvider;

        }
        #endregion
        [HttpPost, Route("getapplicantapplications")]
        public IHttpActionResult GetApplicantApplications()
        {
            var user = _userProvider.GetUserByUserId(User.GetUserId());

            var applications = _applicationProvider.GetApplicantApplication(user.Id);
           foreach(var application in applications)
            {
                var vacancy = this._vacancyProvider.GetVacanyByVacancyId(application.VacancyId);
                var vacancyStatus = this._vacancyStatusProvider.GetVacancyStatus(vacancy.StatusId);
                vacancy.VacancyStatus = vacancyStatus;

                var statusId = application.ApplicationStatusId == (int)Entities.Enums.ApplicationStatuses.AutoRejected ? (int)Entities.Enums.ApplicationStatuses.InProgress : application.ApplicationStatusId;
                
                application.ApplicationStatus = this._applicationStatusProvider.GetApplicationStatus(statusId);
                //application.Requirement = _applicationProvider.GetRequirement(application.ApplicationId);
                application.Vacancy = vacancy;
                //application.Requirement.Programme = _programmeProvider.GetProgrammeById(application.Requirement.ProgrammeId ?? 0);
            }

            return Ok(applications);
        }

        [HttpPost, Route("addapplication")]
        public IHttpActionResult AddApplication(ApplicationModels applicationModels)
        {
            var application = applicationModels.Application;

            application.MaintUser = User.Identity.Name;

            foreach (var item in application.Requirements)
            {
                item.MaintUser = User.Identity.Name;
                item.MaintDate = DateTime.Now;
            }          

            #region CREATE VERSION PROFILE
            var applicantProfile = _applicantProfileProvider.GetApplicantProfileByUserId(User.GetUserId());
            var versionProfile = new VerApplicantProfile()
            {
                ApplicantProfileId = applicantProfile.ApplicantProfileId,
                UserId = applicantProfile.UserId,
                Surname = applicantProfile.Surname,
                FirstNames = applicantProfile.FirstNames,
                DateOfBirth = applicantProfile.DateOfBirth,
                IdentityNumber = applicantProfile.IdentityNumber,
                RaceId = applicantProfile.RaceId,
                RaceOtherDesc = applicantProfile.RaceOtherDesc,
                GenderId = applicantProfile.GenderId,
                Disability = applicantProfile.Disability,
                DisabilityDesc = applicantProfile.DisabilityDesc,
                Citizen = applicantProfile.Citizen,
                Nationality = applicantProfile.Nationality,
                WorkPermit = applicantProfile.WorkPermit,
                PreviousDismissal = applicantProfile.PreviousDismissal,
                CriminalOffence = applicantProfile.CriminalOffence,
                OccupationRegistrationDate = applicantProfile.OccupationRegistrationDate,
                OccupationRegistrationDesc = applicantProfile.OccupationRegistrationDesc,
                PreviousDismissedReason = applicantProfile.PreviousDismissedReason,
                BusinessWithState = applicantProfile.BusinessWithState,
                BusinessWithStateDesc = applicantProfile.BusinessWithStateDesc,
                DischargedOrRetired = applicantProfile.DischargedOrRetired,
                NumberOfExperiencePrivate = applicantProfile.NumberOfExperiencePrivate,
                NumberOfExperiencePublic = applicantProfile.NumberOfExperiencePublic,
                PendingCriminalCase = applicantProfile.PendingCriminalCase,
                PendingCriminalDesc = applicantProfile.PendingCriminalDesc,
                PendingDisciplinary = applicantProfile.PendingDisciplinary,
                PendingDisciplinaryDesc = applicantProfile.PendingDisciplinaryDesc,
                PublicServiceDismiss = applicantProfile.PublicServiceDismiss,
                PublicServiceDismissDesc = applicantProfile.PublicServiceDismissDesc,
                RelinquishBusinessInterest = applicantProfile.RelinquishBusinessInterest,
                ResignedPendingDisciplinary = applicantProfile.ResignedPendingDisciplinary,
                ResignedPendingDisciplinaryDesc = applicantProfile.ResignedPendingDisciplinaryDesc,
                MaintUser = User.Identity.Name,
                MaintDate = DateTime.Now

            };
            application.VerApplicantProfile = versionProfile;
            #endregion

            #region CREATE VERSION ATTACHMENTS
            //var versionAttachments = _applicantAttachmentProvider.GetApplicantAttachments(applicantProfile.ApplicantProfileId);
            var versionAttachments = _applicantAttachmentProvider.GetAttachments(applicantProfile.ApplicantProfileId);
            foreach (Attachment att in versionAttachments)
            {
                var versionAttachment = new VerAttachment()
                {
                    DocumentTypeId = att.DocumentTypeId,
                    DocumentFormat = att.DocumentFormat,
                    OriginalFileName = att.OriginalFileName,
                    FilePath = att.FilePath,
                    MaintDate = DateTime.Now,
                    MaintUser = User.Identity.Name,
                    IsNew = true
                };
                application.VerApplicantProfile.VerAttachments.Add(versionAttachment);
            }
            #endregion

            #region CREATE VERSION CONTACT DETAILS
            var versionContactDetails = _applicantContactDetailsProvider.GetApplicantContactDetail(applicantProfile.ApplicantProfileId);
            var versionContactDetail = new VerApplicantContactDetail()
            {
                LanguageId = versionContactDetails.LanguageId,
                CellNumber = versionContactDetails.CellNumber,
                ContactMethodId = versionContactDetails.ContactMethodId,
                ContactMethogDetails = versionContactDetails.ContactMethogDetails,
                MaintUser = User.Identity.Name,
                MaintDate = DateTime.Now

            };
            application.VerApplicantProfile.VerApplicantContactDetails.Add(versionContactDetail);
            #endregion

            #region CREATE VERSION DECLARATION
            var versionDeclarations = _applicantDeclarationProvider.GetApplicantDeclaration(applicantProfile.ApplicantProfileId);
            var versionDeclaration = new VerApplicantDeclaration()
            {
                AcceptedDeclaration = versionDeclarations.AcceptedDeclaration,
                AcceptedDeclarationOn = versionDeclarations.AcceptedDeclarationOn,
                MaintUser = User.Identity.Name,
                MaintDate = DateTime.Now
            };
            application.VerApplicantProfile.VerApplicantDeclaration = versionDeclaration;
            #endregion

            #region CREATE VERSION EXPERIENCE
            var versionExperiences = _applicantExperienceProvider.GetApplicantExperience(applicantProfile.ApplicantProfileId);
            foreach(ApplicantExperience experience in versionExperiences)
            {
                var versionExperience = new VerApplicantExperience()
                {
                    Employer = experience.Employer,
                    PostHeld = experience.PostHeld,
                    DateFrom = experience.DateFrom,
                    DateTo = experience.DateTo,
                    ReasonForLeaving = experience.ReasonForLeaving,
                    MaintDate = DateTime.Now,
                    MaintUser = User.Identity.Name
                };
                application.VerApplicantProfile.VerApplicantExperiences.Add(versionExperience);
            }
            #endregion

            #region CREATE VERSION PROFICIENCIES
            var versionLanguageProficiencies = _applicantLangProviciencyProvider.GetApplicantProficiencies(applicantProfile.ApplicantProfileId);
            foreach(ApplicantLangProficiency proficiency in versionLanguageProficiencies)
            {
                var versionLanguageProficiency = new VerApplicantLangProficiency()
                {
                    LanguageId = proficiency.LanguageId,
                    ReadProficiencyId = proficiency.ReadProficiencyId,
                    SpeakProficiencyId = proficiency.SpeakProficiencyId,
                    WriteReadProficiencyId = proficiency.WriteReadProficiencyId,
                    MaintDate = DateTime.Now,
                    MaintUser = User.Identity.Name
                };
                application.VerApplicantProfile.VerApplicantLangProficiencies.Add(versionLanguageProficiency);
            }
            #endregion

            #region CREATE VERSION PROHIBITION
            var currentProhibition = _applicantProhibitionProvider.GetApplicantProhibition(applicantProfile.ApplicantProfileId);
            var versionProhibition = new VerApplicantPubServProhibition()
            {
                PublicServiceProhibition = currentProhibition.PublicServiceProhibition,
                DepartmentName = currentProhibition.DepartmentName,
                MaintDate = DateTime.Now,
                MaintUser = User.Identity.Name
            };
            application.VerApplicantProfile.VerApplicantPubServProhibition = versionProhibition;
            #endregion

            #region CREATE VERSION QUALIFICATIONS

            var currentQualifications = _applicantQualificationsProvider.GetApplicantQualification(applicantProfile.ApplicantProfileId);
            foreach (ApplicantQualification qualification in currentQualifications)
            {
                var versionQualification = new VerApplicantQualification()
                {
                    InstitutionId = qualification.Institution.InstitutionId,
                    QualificationTypeId = qualification.QualificationTypeId,
                    NameOfQualification = qualification.NameOfQualification,
                    OtherInstitution = qualification.OtherInstitution,
                    YearObtained = qualification.YearObtained,
                    CurrentStudies = qualification.CurrentStudies,
                    MaintDate = DateTime.Now,
                    MaintUser = User.Identity.Name
                };
                application.VerApplicantProfile.VerApplicantQualifications.Add(versionQualification);
            }
            
            #endregion

            #region CREATE VERSION REFERENCES 
            var currentReferences = _applicantReferenceProvider.GetApplicantReferences(applicantProfile.ApplicantProfileId);
            foreach(ApplicantReference reference in currentReferences)
            {
                var versionReference = new VerApplicantReference()
                {
                    ReferenceName = reference.ReferenceName,
                    ReferenceRelationship = reference.ReferenceRelationship,
                    ReferenceContactNumber = reference.ReferenceContactNumber,
                    MaintDate = DateTime.Now,
                    MaintUser = User.Identity.Name
                };
                application.VerApplicantProfile.VerApplicantReferences.Add(versionReference);
            }
            #endregion

            var result = _applicationProvider.AddApplication(application);
            applicationModels.Application = result;
            SentAllQuestionsAndAnswers(applicationModels);
            SendEmail(application);
            return Ok();
        }

        public void SentAllQuestionsAndAnswers(ApplicationModels allQuestionsAndAnswers)
        {
            var genericQuestionsAnswers = allQuestionsAndAnswers.GenericQuestionsAnswers;
            var specificQuestionsAnswers = allQuestionsAndAnswers.SpecificQuestionsAnswers;
            var application = allQuestionsAndAnswers.Application;
            var requirements = allQuestionsAndAnswers.Application.Requirements;

            int score = 0;
            foreach (var genericQuestion in genericQuestionsAnswers)
            {                
                foreach (var answersGenericQuestions in genericQuestion.ListAnswersGenericQuestions)
                {
                    foreach (var requirement in requirements)
                    {
                        if(requirement.AnswersGenericQuestionsId == answersGenericQuestions.Id)
                        {
                            bool autoReject = answersGenericQuestions.AutoReject ?? false;
                            if(autoReject)
                            {
                                // Update the requirements with the rejected status
                                application.ApplicationStatusId = (int)Entities.Enums.ApplicationStatuses.AutoRejected;
                                var applicationToUpdate = _applicationProvider.GetApplicantApplication(application.VacancyId, application.ApplicationId);
                                _applicationProvider.UpdateApplicationStatus(applicationToUpdate);
                                return;
                            } else
                            {
                                score += answersGenericQuestions.Weight;
                            }
                        }
                    }
                }
            }

            foreach (var specificQuestions in specificQuestionsAnswers)
            {
                foreach (var answersSpecificQuestions in specificQuestions.ListAnswersSpecificQuestions)
                {
                    foreach (var requirement in requirements)
                    {
                        if (requirement.AnswersSpecificQuestionsId == answersSpecificQuestions.Id)
                        {
                            bool autoReject = answersSpecificQuestions.AutoReject ?? false;
                            if (autoReject)
                            {
                                // Update the requirements with the rejected status
                                application.ApplicationStatusId = (int)Entities.Enums.ApplicationStatuses.AutoRejected;
                                var applicationToUpdate = _applicationProvider.GetApplicantApplication(application.VacancyId, application.ApplicationId);
                                _applicationProvider.UpdateApplicationStatus(applicationToUpdate);
                                return;
                            }
                            else
                            {
                                score += answersSpecificQuestions.Weight ?? 0;
                            }
                        }
                    }
                }
            }

            double scorePercentage = ((double)score / ((double)allQuestionsAndAnswers.AnswersThreshold.SumOfWeights)) * 100;

            if(scorePercentage < allQuestionsAndAnswers.AnswersThreshold.Threshold)
            {
                // Update the requirements with the rejected status
                application.ApplicationStatusId = (int)Entities.Enums.ApplicationStatuses.AutoRejected;
                _applicationProvider.UpdateApplicationStatus(application);
            }
        }

        [HttpPost, Route("saveapplication")]
        public IHttpActionResult SaveApplication(Application application)
        {
            application.MaintUser = User.Identity.Name;
            application.MaintDate = DateTime.Now;
            //application.Requirement.MaintUser = User.Identity.Name;
            //application.Requirement.MaintDate = DateTime.Now;
            _applicationProvider.UpdateApplication(application);
            return Ok();
        }

        private void SendEmail(Application application)
        {
            TextInfo textInfo = new CultureInfo("en-ZA", false).TextInfo;
            string name = textInfo.ToTitleCase(application.VerApplicantProfile.FirstNames);
            string surname = textInfo.ToTitleCase(application.VerApplicantProfile.Surname);

            var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplates/FormSubmission.html");
            var body = createEmailBody(name + " " + surname, mappedPath);

            Helper.SendEmail(application.VerApplicantProfile.MaintUser, "eRecruitment - Application Submitted", body);

            //bool sendUnsuccessfulEmail = false;

            //if (!application.VerApplicantProfile.Citizen)
            //    sendUnsuccessfulEmail = true;

            //if (application.ApplicationRequirement.InternshipScore == 3)
            //    sendUnsuccessfulEmail = true;

            //if(application.VerApplicantProfile.VerApplicantQualifications.Count == 1)
            //{
            //    var institution = application.VerApplicantProfile.VerApplicantQualifications.FirstOrDefault().InstitutionId;

            //    if (institution == 28)
            //        sendUnsuccessfulEmail = true;
            //}

            //if(sendUnsuccessfulEmail)
            //{
            //    mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplates/Unsuccessful.html");
            //    body = createEmailBody(name + " " + surname, mappedPath);

            //    Helper.SendEmail(application.VerApplicantProfile.MaintUser, "eRecruitment - Application Unsuccessful", body);
            //}
        }

        private string createEmailBody(string fullname, string mappedPath)
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
