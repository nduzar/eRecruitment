using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IManager.Users;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.Manager.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.IManager.Auth;
using StatsSA.eRecruitment.Manager.Auth;
using StatsSA.eRecruitment.IManager.Clients;
using StatsSA.eRecruitment.Manager.Clients;
using System.Data.Entity;
using System.Data.Entity.Validation;
using StatsSA.eRecruitment.IManager.UserClients;
using StatsSA.eRecruitment.Manager.UserClients;
using StatsSA.eRecruitment.IManager.Roles;
using StatsSA.eRecruitment.Manager.Roles;
using StatsSA.eRecruitment.IManager.ClientRoles;
using StatsSA.eRecruitment.Manager.ClientRoles;
using StatsSA.eRecruitment.IManager.Vacancies;
using StatsSA.eRecruitment.Manager.Vacancies;
using StatsSA.eRecruitment.IManager.ManagePassword;
using StatsSA.eRecruitment.Manager.ManagePassword;
using StatsSA.eRecruitment.IManager.Profiles;
using StatsSA.eRecruitment.Manager.Profiles;
using StatsSA.eRecruitment.IManager.Applications;
using StatsSA.eRecruitment.Manager.Applications;
using StatsSA.eRecruitment.IManager.Races;
using StatsSA.eRecruitment.Manager.Races;
using StatsSA.eRecruitment.IManager.Genders;
using StatsSA.eRecruitment.Manager.Genders;
using StatsSA.eRecruitment.Manager.ContactMethods;
using StatsSA.eRecruitment.Manager.Languages;
using StatsSA.eRecruitment.Manager.LanguageProficiencies;
using StatsSA.eRecruitment.Manager.QualificationTypes;
using StatsSA.eRecruitment.Manager.Experiences;
using StatsSA.eRecruitment.Manager.Qualifications;
using StatsSA.eRecruitment.Manager.ApplicantAttachments;
using StatsSA.eRecruitment.Manager.ApplicantContactDetails;
using StatsSA.eRecruitment.Manager.ApplicantDeclarations;
using StatsSA.eRecruitment.Manager.ApplicantProhibition;
using StatsSA.eRecruitment.IManager.ContactMethods;
using StatsSA.eRecruitment.IManager.Languages;
using StatsSA.eRecruitment.IManager.LanguageProficiencies;
using StatsSA.eRecruitment.IManager.QualificationTypes;
using StatsSA.eRecruitment.IManager.Qualifications;
using StatsSA.eRecruitment.IManager.Experiences;
using StatsSA.eRecruitment.IManager.ApplicantAttachments;
using StatsSA.eRecruitment.IManager.ApplicantContactDetails;
using StatsSA.eRecruitment.IManager.ApplicantDeclarations;
using StatsSA.eRecruitment.IManager.ApplicantProhibition;
using StatsSA.eRecruitment.Manager.ApplicantLanguageProficiencies;
using StatsSA.eRecruitment.IManager.ApplicantLanguageProficiencies;
using StatsSA.eRecruitment.Manager.ApplicantReferences;
using StatsSA.eRecruitment.IManager.ApplicantReferences;
using StatsSA.eRecruitment.IManager.DocumentTypes;
using StatsSA.eRecruitment.Manager.DocumentTypes;
using StatsSA.eRecruitment.Manager.Salaries;
using StatsSA.eRecruitment.IManager.Salaries;
using StatsSA.eRecruitment.IManager.VacancyStatuses;
using StatsSA.eRecruitment.Manager.VacancyStatuses;
using StatsSA.eRecruitment.Manager.ApplicationRequirements;
using StatsSA.eRecruitment.IManager.Requirements;
using StatsSA.eRecruitment.Manager.ApplicationStatuses;
using StatsSA.eRecruitment.IManager.ApplicationStatuses;
using StatsSA.eRecruitment.Manager.HRUsers;
using StatsSA.eRecruitment.IManager.HRUsers;
using StatsSA.eRecruitment.Manager.HRUserRoles;
using StatsSA.eRecruitment.IManager.HRUserRoles;
using StatsSA.eRecruitment.Manager.VerProfiles;
using StatsSA.eRecruitment.IManager.VerProfiles;
using StatsSA.eRecruitment.IManager.VerApplicantAttachments;
using StatsSA.eRecruitment.Manager.VerApplicantAttachments;
using StatsSA.eRecruitment.Manager.VerApplicantContactDetails;
using StatsSA.eRecruitment.IManager.VerApplicantContactDetails;
using StatsSA.eRecruitment.IManager.VerApplicantDeclarations;
using StatsSA.eRecruitment.Manager.VerApplicantDeclarations;
using StatsSA.eRecruitment.Manager.VerApplicantExperiences;
using StatsSA.eRecruitment.IManager.VerApplicantExperiences;
using StatsSA.eRecruitment.Manager.VerApplicantLangProficiencies;
using StatsSA.eRecruitment.IManager.VerApplicantLangProficiencies;
using StatsSA.eRecruitment.IManager.VerApplicantProhibition;
using StatsSA.eRecruitment.Manager.VerApplicantProhibition;
using StatsSA.eRecruitment.IManager.VerApplicantQualifications;
using StatsSA.eRecruitment.Manager.VerApplicantQualifications;
using StatsSA.eRecruitment.IManager.VerApplicantReferences;
using StatsSA.eRecruitment.Manager.VerApplicantReferences;
using StatsSA.eRecruitment.Manager.HRRoles;
using StatsSA.eRecruitment.IManager.HRRoles;
using StatsSA.eRecruitment.IManager.PasswordResetRequests;
using StatsSA.eRecruitment.Manager.PasswordResetRequests;
using StatsSA.eRecruitment.IManager.Institutions;
using StatsSA.eRecruitment.Manager.Institutions;
using StatsSA.eRecruitment.IManager.Programmes;
using StatsSA.eRecruitment.IManager.IGenericQuestions;
using StatsSA.eRecruitment.Manager.GenericQuestions;
using StatsSA.eRecruitment.IManager.AnswersGenericQuestions;
using StatsSA.eRecruitment.Manager.AnswersGenericQuestions;
using StatsSA.eRecruitment.IManager.SpecificQuestions;
using StatsSA.eRecruitment.Manager.SpecificQuestions;
using StatsSA.eRecruitment.IManager.AnswersSpecificQuestions;
using StatsSA.eRecruitment.Manager.AnswersSpecificQuestions;
using StatsSA.eRecruitment.IManager.AnswerThreshold;
using StatsSA.eRecruitment.Manager.AnswerThreshold;
using StatsSA.eRecruitment.IManager.EmailVerification;
using StatsSA.eRecruitment.Manager.EmailVerification;
using StatsSA.eRecruitment.IManager.ViewApplicantQuestionAnswers;
using StatsSA.eRecruitment.Manager.ViewApplicantQuestionAnswers;
using StatsSA.eRecruitment.IManager.HiringManagersVacancy;
using StatsSA.eRecruitment.Manager.HiringManagersVacancy;
using StatsSA.eRecruitment.IManager.RequestAllApplications;
using StatsSA.eRecruitment.Manager.RequestAllApplications;

namespace StatsSA.eRecruitment.Manager.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private eRecruitmentEntities _context = null;
        private IUserManager _userManager;
        private IAuthManager _authManager;
        private IClientManager _clientManager;
        private IUserClientManager _serClientManager;
        private IRoleManager _roleManager;
        private IClientRoleManager _clientRoleManager;
        private IVacancyManager _vacancyManager;
        private IGenericQuestionsManager _genericQuestionManager;
        private IApplicantProfileManager _applicantProfileManager;
        private IManagePasswordManager _managePasswordManager;
        private IApplicationManager _applicationManager;
        private IRaceManager _raceManager;
        private IGenderManager _genderManager;
        private IProgrammeManager _programmeManager;
        private IInstitutionsManager _institutionManager;
        private IContactMethodManager _contactMethodManager;
        private ILanguageManager _languageManager;
        private ILanguageProficiencyManager _languageProficiencyManager;
        private IQualificicationTypeManager _qualificationTypeManager;
        private IApplicantExperienceManager _applicantExperienceManager;
        private IApplicantQualificationManager _applicantQualificationManager;
        private IApplicantAttachmentsManager _applicantAttachmentsManager;
        private IApplicantContactDetailsManager _applicantContactDetailsManager;
        private IApplicantDeclarationManager _applicantDeclarationManager;
        private IApplicantProhibitionManager _applicantProhibitionManager;
        private IApplicantLangProficiencyManager _applicantLangProficiencyManager;
        private IApplicantReferenceManager _applicantReferenceManager;
        private IDocumentTypeManager _documentTypeManager;
        private ISalaryManager _salaryManager;
        private IVacancyStatusManager _vacancyStatusManager;
        private IRequirementManager _RequirementManager;
        private IApplicationStatusManager _applicationStatusManager;
        private IHRUserManager _hrUserManager;
        private IHRUserRoleManager _hrUserRoleManager;
        private IVerApplicantProfileManager _verApplicantProfileManager;
        private IVerApplicantAttachmentsManager _verApplicantAttachmentsManager;
        private IVerApplicantContactDetailsManager _verApplicantContactDetailsManager;
        private IVerApplicantDeclarationManager _verApplicantDeclarationManager;
        private IVerApplicantExperienceManager _verApplicantExperienceManager;
        private IVerApplicantLangProficiencyManager _verApplicantLangProficiencyManager;
        private IVerApplicantProhibitionManager _verApplicantProhibitionManager;
        private IVerApplicantQualificationManager _verApplicantQualificationManager;
        private IVerApplicantReferencesManager _verApplicantReferencesManager;
        private IHRRoleManager _hrRoleManager;
        private IPasswordResetRequestManager _passwordResetRequestManager;
        private IAnswersGenericQuestionsManager _answersGenericQuestionsManager;
        private IAnswersSpecificQuestionsManager _answersSpecificQuestionsManager;
        private DbContextTransaction _dbContextTransaction;
        private ISpecificQuestionsManager _specificQuestionManager;
        private IAnswersThresholdManager _answersThresholdManager;
        private IVerifyEmailManager _verifyEmailManager;
        private IViewApplicantQuestionAnswersManager _viewApplicantQuestionAnswersManager;
        private IHiringManagersVacancyAccessManager _hiringManagersVacancyAccessManager;
        private IRequestAllApplicationsManager _requestAllApplicationsManager;

        public UnitOfWork()
        {
            _context = new eRecruitmentEntities();
            _context.Configuration.LazyLoadingEnabled = false;
            _context.Database.CommandTimeout = 180;
        }
        public IHRRoleManager HRRoleManager
        {
            get
            {
                return this._hrRoleManager ?? (this._hrRoleManager = new HRRoleManager(_context));
            }
        }
        public IVerApplicantReferencesManager VerApplicantReferencesManager
        {
            get
            {
                return this._verApplicantReferencesManager ?? (this._verApplicantReferencesManager = new VerApplicantReferencesManager(_context));
            }
        }
        public IVerApplicantQualificationManager VerApplicantQualificationManager {
            get
            {
                return this._verApplicantQualificationManager ?? (this._verApplicantQualificationManager = new VerApplicantQualificationManager(_context));
            }
        }
        public IVerApplicantProhibitionManager VerApplicantProhibitionManager
        {
            get
            {
                return this._verApplicantProhibitionManager ?? (this._verApplicantProhibitionManager = new VerApplicantProhibitionManager(_context));
            }
        }
        public IVerApplicantLangProficiencyManager VerApplicantLangProficiencyManager
        {
            get
            {
                return this._verApplicantLangProficiencyManager ?? (this._verApplicantLangProficiencyManager = new VerApplicantLangProficiencyManager(_context));
            }
        }
        public IVerApplicantExperienceManager VerApplicantExperienceManager
        {
            get
            {
                return this._verApplicantExperienceManager ?? (this._verApplicantExperienceManager = new VerApplicantExperienceManager(_context));
            }
        }
        public IVerApplicantDeclarationManager VerApplicantDeclarationManager
        {
            get
            {
                return this._verApplicantDeclarationManager ?? (this._verApplicantDeclarationManager = new VerApplicantDeclarationManager(_context));
            }
        }
        public IVerApplicantContactDetailsManager VerApplicantContactDetailsManager
        {
            get
            {
                return this._verApplicantContactDetailsManager ?? (this._verApplicantContactDetailsManager = new VerApplicantContactDetailsManager(_context));
            }
        }
        public IVerApplicantAttachmentsManager VerApplicantAttachmentsManager
        {
            get
            {
                return this._verApplicantAttachmentsManager ?? (this._verApplicantAttachmentsManager = new VerApplicantAttachmentsManager(_context));
            }
        }
        public IVerApplicantProfileManager VerApplicantProfileManager
        {
            get
            {
                return this._verApplicantProfileManager ?? (this._verApplicantProfileManager = new VerApplicantProfileManager(_context));
            }
        }

        public IUserManager UserManager
        {
            get
            {
                return this._userManager ?? (this._userManager = new UserManager(_context));
            }
        }

        public IAnswersGenericQuestionsManager AnswersGenericQuestionsManager
        {
            get
            {
                return this._answersGenericQuestionsManager ?? (this._answersGenericQuestionsManager = new AnswersGenericQuestionsManager(_context));
            }
        }

        public IAnswersSpecificQuestionsManager AnswersSpecificQuestionsManager
        {
            get
            {
                return this._answersSpecificQuestionsManager ?? (this._answersSpecificQuestionsManager = new AnswersSpecificQuestionsManager(_context));
            }
        }

        public ISpecificQuestionsManager SpecificQuestionManager
        {
            get
            {
                return this._specificQuestionManager ?? (this._specificQuestionManager = new SpecificQuestionsManager(_context));
            }
        }

        public IAuthManager AuthManager
        {
            get
            {
                return this._authManager ?? (this._authManager = new AuthManager(_context));
            }
        }

        public IClientManager ClientManager
        {
            get
            {
                return this._clientManager ?? (this._clientManager = new ClientManager(_context));
            }
        }

        public IUserClientManager UserClientManager
        {
            get
            {
                return this._serClientManager ?? (this._serClientManager = new UserClientManager(_context));
            }
        }

        public IRoleManager RoleManager
        {
            get
            {
                return this._roleManager ?? (this._roleManager = new RoleManager(_context));
            }
        }

        public IClientRoleManager ClientRoleManager
        {
            get
            {
                return this._clientRoleManager ?? (this._clientRoleManager = new ClientRoleManager(_context));
            }
        }

        public IApplicationStatusManager ApplicationStatusManager
        {
            get
            {
                return this._applicationStatusManager ?? (this._applicationStatusManager = new ApplcationStatusManager(_context));
            }
        }
        public IRequirementManager RequirementManager
        {
            get
            {
                return this._RequirementManager ?? (this._RequirementManager = new RequirementManager(_context));
            }
        }
        public ISalaryManager SalaryManager
        {
            get
            {
                return this._salaryManager ?? (this._salaryManager = new SalaryManager(_context));
            }
        }
        public IVacancyManager VacancyManager
        {
            get
            {
                return this._vacancyManager ?? (this._vacancyManager = new VacancyManager(_context));
            }
        }

        public IGenericQuestionsManager GenericQuestionsManager
        {
            get
            {
                return this._genericQuestionManager ?? (this._genericQuestionManager = new GenericQuestionsManager(_context));
            }
        }

        public IVacancyStatusManager VacancyStatusManager
        {
            get
            {
                return this._vacancyStatusManager ?? (this._vacancyStatusManager = new VacancyStatusManager(_context));
            }
        }

        public IManagePasswordManager ManagePasswordManager
        {
            get
            {
                return this._managePasswordManager ?? (this._managePasswordManager = new ManagePasswordManager(_context));
            }
        }

        public IContactMethodManager ContactMethodManager
        {
            get
            {
                return this._contactMethodManager ?? (this._contactMethodManager = new ContactMethod(_context));
            }
        }
        public IApplicantLangProficiencyManager ApplicantLangProficiencyManager
        {
            get
            {
                return this._applicantLangProficiencyManager ?? (this._applicantLangProficiencyManager = new ApplicantLangProficiencyManager(_context));

            }
        }
        public IApplicantReferenceManager ApplicantReferenceManager
        {
            get
            {
                return this._applicantReferenceManager ?? (this._applicantReferenceManager = new ApplicantReferencesManager(_context));

            }
        }

        public ILanguageManager LanguageManager
        {
            get
            {
                return this._languageManager ?? (this._languageManager = new LanguageManager(_context));

            }
        }

        public ILanguageProficiencyManager LanguageProficiencyManager
        {
            get
            {
                return this._languageProficiencyManager ?? (this._languageProficiencyManager = new LanguageProficiencyManager(_context));

            }
        }

        public IApplicantProfileManager ApplicantProfileManager
        {
            get
            {
                return this._applicantProfileManager ?? (this._applicantProfileManager = new ApplicantProfileManager(_context));

            }
        }

        public IApplicationManager ApplicationManager
        {
            get
            {
                return this._applicationManager ?? (this._applicationManager = new ApplicationManager(_context));
            }
        }

        public IRaceManager RaceManager
        {
            get
            {
                return this._raceManager ?? (this._raceManager = new RaceManager(_context));
            }
        }

        public IGenderManager GenderManager
        {
            get
            {
                return this._genderManager ?? (this._genderManager = new GenderManager(_context));
            }
        }

        public IProgrammeManager ProgrammeManager
        {
            get
            {
                return this._programmeManager ?? (this._programmeManager = new ProgrammeManager(_context));
            }
        }

        public IInstitutionsManager InstitutionsManager
        {
            get
            {
                return this._institutionManager ?? (this._institutionManager = new InstitutionsManager(_context));
            }
        }

        public IAnswersThresholdManager AnswersThresholdManager
        {
            get
            {
                return this._answersThresholdManager ?? (this._answersThresholdManager = new AnswersThresholdManager(_context));
            }
        }

        public IQualificicationTypeManager QualificicationTypeManager
        {
            get
            {
                return this._qualificationTypeManager ?? (this._qualificationTypeManager = new QualificationTypeManager(_context));

            }
        }

        public IApplicantExperienceManager ApplicantExperienceManager
        {
            get
            {
                return this._applicantExperienceManager ?? (this._applicantExperienceManager = new ApplicantExperienceManager(_context));

            }
        }
        public IApplicantQualificationManager ApplicantQualificationManager
        {
            get
            {
                return this._applicantQualificationManager ?? (this._applicantQualificationManager = new ApplicantQualificationsManager(_context));

            }
        }

        public IApplicantContactDetailsManager ApplicantContactDetailsManager
        {
            get
            {
                return this._applicantContactDetailsManager ?? (this._applicantContactDetailsManager = new ApplicantContactDetailsManager(_context));
            }
        }

        public IApplicantAttachmentsManager ApplicantAttachementsManager
        {
            get
            {
                return this._applicantAttachmentsManager ?? (this._applicantAttachmentsManager = new ApplicantAttachmentManager(_context));
            }
        }

        public IApplicantDeclarationManager ApplicantDeclarationManager
        {
            get
            {
                return this._applicantDeclarationManager ?? (this._applicantDeclarationManager = new ApplicantDeclarationManager(_context));
            }
        }

        public IApplicantProhibitionManager ApplicantProhibitionManager
        {
            get
            {
                return this._applicantProhibitionManager ?? (this._applicantProhibitionManager = new ApplicantProhibitionManager(_context));
            }
        }

        public IApplicantLangProficiencyManager ApplicantLagnuageProficiencyManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDocumentTypeManager DocumentTypeManager
        {
            get
            {
                return this._documentTypeManager ?? (this._documentTypeManager = new DocumentTypeManager(_context));
            }
        }

        public IHRUserManager HRUserManager
        {
            get
            {
                return this._hrUserManager ?? (this._hrUserManager = new HRUserManager(_context));
            }
        }

        public IHRUserRoleManager HRUserRoleManager
        {
            get
            {
                return this._hrUserRoleManager ?? (this._hrUserRoleManager = new HRUserRoleManager(_context));
            }
        }

        public IPasswordResetRequestManager PasswordResetRequestManager
        {
            get
            {
                return this._passwordResetRequestManager ?? (this._passwordResetRequestManager = new PasswordResetRequestManager(_context));
            }
        }

        public IGenericQuestionsManager GenericQuestionManager
        {
            get
            {
                return this._genericQuestionManager ?? (this._genericQuestionManager = new GenericQuestionsManager(_context));
            }
        }

        public IVerifyEmailManager VerifyEmailManager
        {
            get
            {
                return this._verifyEmailManager ?? (this._verifyEmailManager = new VerifyEmailManager(_context));
            }
        }

        public IViewApplicantQuestionAnswersManager ViewApplicantQuestionAnswersManager
        {
            get
            {
                return this._viewApplicantQuestionAnswersManager ?? (this._viewApplicantQuestionAnswersManager = new ViewApplicantQuestionAnswersManager(_context));
            }
        }

        public IHiringManagersVacancyAccessManager HiringManagersVacancyAccessManager
        {
            get
            {
                return this._hiringManagersVacancyAccessManager ?? (this._hiringManagersVacancyAccessManager = new HiringManagersVacancyAccessManager(_context));
            }
        }

        public IRequestAllApplicationsManager RequestAllApplicationsManager
        {
            get
            {
                return this._requestAllApplicationsManager ?? (this._requestAllApplicationsManager = new RequestAllApplicationsManager(_context));
            }
        }

        

        public int SaveChanges()
        {
            _context.Database.Log = s =>
           { 
               System.Diagnostics.Debug.Print(s);
           };


            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void BeginTransaction()
        {
            if (_dbContextTransaction == null)
            {
                this._dbContextTransaction = _context.Database.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            try
            {
                _dbContextTransaction.Commit();
            }
            catch (DbEntityValidationException ex)
            {                
                var exceptionMessage = ExceptionHaddler(ex);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        private string ExceptionHaddler(DbEntityValidationException ex)
        {
            // Retrieve the error messages as a list of strings.
            var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
            // Join the list to a single string.
            var fullErrorMessage = string.Join("; ", errorMessages);
            // Combine the original exception message with the new one.
            return string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void RollbackTransaction()
        {
            _dbContextTransaction.Rollback();
        }
    }
}
