using StatsSA.eRecruitment.IManager.Auth;
using StatsSA.eRecruitment.IManager.Clients;
using StatsSA.eRecruitment.IManager.Roles;
using StatsSA.eRecruitment.IManager.UserClients;
using StatsSA.eRecruitment.IManager.Users;
using StatsSA.eRecruitment.IManager.ClientRoles;
using System.Threading.Tasks;
using StatsSA.eRecruitment.IManager.Vacancies;
using StatsSA.eRecruitment.IManager.Profiles;
using StatsSA.eRecruitment.IManager.ManagePassword;
using StatsSA.eRecruitment.IManager.Applications;
using StatsSA.eRecruitment.IManager.Races;
using StatsSA.eRecruitment.IManager.Genders;
using StatsSA.eRecruitment.IManager.ContactMethods;
using StatsSA.eRecruitment.IManager.LanguageProficiencies;
using StatsSA.eRecruitment.IManager.Languages;
using StatsSA.eRecruitment.IManager.QualificationTypes;
using StatsSA.eRecruitment.IManager.Qualifications;
using StatsSA.eRecruitment.IManager.Experiences;
using StatsSA.eRecruitment.IManager.ApplicantAttachments;
using StatsSA.eRecruitment.IManager.ApplicantContactDetails;
using StatsSA.eRecruitment.IManager.ApplicantProhibition;
using StatsSA.eRecruitment.IManager.ApplicantDeclarations;
using StatsSA.eRecruitment.IManager.ApplicantLanguageProficiencies;
using StatsSA.eRecruitment.IManager.ApplicantReferences;
using StatsSA.eRecruitment.IManager.DocumentTypes;
using StatsSA.eRecruitment.IManager.Salaries;
using StatsSA.eRecruitment.IManager.VacancyStatuses;
using StatsSA.eRecruitment.IManager.Requirements;
using StatsSA.eRecruitment.IManager.ApplicationStatuses;
using StatsSA.eRecruitment.IManager.HRUsers;
using StatsSA.eRecruitment.IManager.HRUserRoles;
using StatsSA.eRecruitment.IManager.VerProfiles;
using StatsSA.eRecruitment.IManager.VerApplicantAttachments;
using StatsSA.eRecruitment.IManager.VerApplicantContactDetails;
using StatsSA.eRecruitment.IManager.VerApplicantDeclarations;
using StatsSA.eRecruitment.IManager.VerApplicantExperiences;
using StatsSA.eRecruitment.IManager.VerApplicantLangProficiencies;
using StatsSA.eRecruitment.IManager.VerApplicantProhibition;
using StatsSA.eRecruitment.IManager.VerApplicantQualifications;
using StatsSA.eRecruitment.IManager.VerApplicantReferences;
using StatsSA.eRecruitment.IManager.HRRoles;
using StatsSA.eRecruitment.IManager.PasswordResetRequests;
using StatsSA.eRecruitment.IManager.Institutions;
using StatsSA.eRecruitment.IManager.Programmes;
using StatsSA.eRecruitment.IManager.IGenericQuestions;
using StatsSA.eRecruitment.IManager.AnswersGenericQuestions;
using StatsSA.eRecruitment.IManager.SpecificQuestions;
using StatsSA.eRecruitment.IManager.AnswersSpecificQuestions;
using StatsSA.eRecruitment.IManager.AnswerThreshold;
using StatsSA.eRecruitment.IManager.EmailVerification;
using StatsSA.eRecruitment.IManager.ViewApplicantQuestionAnswers;
using StatsSA.eRecruitment.IManager.HiringManagersVacancy;
using StatsSA.eRecruitment.IManager.RequestAllApplications;

namespace StatsSA.eRecruitment.IManager.UnitOfWork
{
    public interface IUnitOfWork
    {
        IHRRoleManager HRRoleManager { get; }
        IVerApplicantReferencesManager VerApplicantReferencesManager { get; }
        IVerApplicantQualificationManager VerApplicantQualificationManager { get; }
        IVerApplicantProhibitionManager VerApplicantProhibitionManager { get; }
        IVerApplicantLangProficiencyManager VerApplicantLangProficiencyManager { get; }
        IVerApplicantExperienceManager VerApplicantExperienceManager { get; }
        IVerApplicantProfileManager VerApplicantProfileManager { get; }
        IVerApplicantAttachmentsManager VerApplicantAttachmentsManager { get; }
        IVerApplicantContactDetailsManager VerApplicantContactDetailsManager { get; }
        IVerApplicantDeclarationManager VerApplicantDeclarationManager { get; }
        IUserManager UserManager { get; }
        IAuthManager AuthManager { get; }
        IClientManager ClientManager { get; }
        IUserClientManager UserClientManager { get; }
        IRoleManager RoleManager { get; }
        IClientRoleManager ClientRoleManager { get; }
        IHRUserManager HRUserManager { get; }
        IHRUserRoleManager HRUserRoleManager { get; }
        IVacancyManager VacancyManager { get; }
        IGenericQuestionsManager GenericQuestionManager { get; }

        ISpecificQuestionsManager SpecificQuestionManager { get; }

        IGenderManager GenderManager { get; }

        IAnswersGenericQuestionsManager AnswersGenericQuestionsManager { get; }

        IAnswersSpecificQuestionsManager AnswersSpecificQuestionsManager { get; }

        IProgrammeManager ProgrammeManager { get; }
        IManagePasswordManager ManagePasswordManager { get; }
        IApplicationManager ApplicationManager { get; }
        IApplicantProfileManager ApplicantProfileManager { get; }
        IRaceManager RaceManager { get; }
        IContactMethodManager ContactMethodManager { get; }
        ILanguageManager LanguageManager { get; }
        ILanguageProficiencyManager LanguageProficiencyManager { get; }
        IQualificicationTypeManager QualificicationTypeManager { get; }
        IApplicantExperienceManager ApplicantExperienceManager { get; }
        IApplicantQualificationManager ApplicantQualificationManager { get; }
        IApplicantContactDetailsManager ApplicantContactDetailsManager { get; }
        IApplicantAttachmentsManager ApplicantAttachementsManager { get; }
        IApplicantDeclarationManager ApplicantDeclarationManager { get; }
        IApplicantProhibitionManager ApplicantProhibitionManager { get; }
        IApplicantLangProficiencyManager ApplicantLangProficiencyManager { get; }
        IApplicantReferenceManager ApplicantReferenceManager { get; }
        IDocumentTypeManager DocumentTypeManager { get; }        
        ISalaryManager SalaryManager { get; }
        IRequirementManager RequirementManager { get; }
        IVacancyStatusManager VacancyStatusManager { get; }
        IApplicationStatusManager ApplicationStatusManager { get; }
        IPasswordResetRequestManager PasswordResetRequestManager { get; }
        IInstitutionsManager InstitutionsManager { get; }

        IAnswersThresholdManager AnswersThresholdManager { get; }

        IVerifyEmailManager VerifyEmailManager { get; }

        IViewApplicantQuestionAnswersManager ViewApplicantQuestionAnswersManager { get; }

        IHiringManagersVacancyAccessManager HiringManagersVacancyAccessManager { get; }

        IRequestAllApplicationsManager RequestAllApplicationsManager { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
