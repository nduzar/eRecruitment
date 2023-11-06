using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.ApplicantProfiles;
using StatsSA.eRecruitment.IProvider.ApplicantExperiences;
using StatsSA.eRecruitment.IProvider.ApplicantQualifications;
using StatsSA.eRecruitment.IProvider.ApplicantAttachments;
using StatsSA.eRecruitment.IProvider.ApplicantContactDetails;
using StatsSA.eRecruitment.IProvider.ApplicantDeclarations;
using StatsSA.eRecruitment.IProvider.ApplicantProhibition;
using StatsSA.eRecruitment.IProvider.ApplicantLanguageProficiencies;
using StatsSA.eRecruitment.IProvider.ApplicantReferences;
using StatsSA.eRecruitment.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Web.Http;
using StatsSA.eRecruitment.WebApi.Models;
using StatsSA.eRecruitment.IProvider.Languages;
using StatsSA.eRecruitment.IProvider.Genders;
using StatsSA.eRecruitment.IProvider.Races;
using StatsSA.eRecruitment.IProvider.LanguageProficiencies;
using StatsSA.eRecruitment.IProvider.ContactMethods;
using StatsSA.eRecruitment.IProvider.QualificationTypes;
using StatsSA.eRecruitment.IProvider.DocumentTypes;
using System.IO;
using System.Net.Http;
using System.Net;
using StatsSA.eRecruitment.IProvider.Users;
using StatsSA.eRecruitment.IProvider.VerApplicantProfiles;
using System.Linq;
using StatsSA.eRecruitment.IProvider.Applications;
using System.Security.Claims;
using System.Web.Http.Cors;
using StatsSA.eRecruitment.IProvider.Institutions;
using StatsSA.eRecruitment.IProvider.Programmes;
using System.Web;
using System.Threading.Tasks;
using System.Text;
using StatsSA.eRecruitment.IManager.Users;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [RoutePrefix("api/applicantprofile")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplicantProfileController : ApiController
    {
        #region Declaration and Instantiation of IProviders
        private IApplicantProfileProvider _applicantProfileProvider;
        private IApplicantExperienceProvider _applicantExperienceProvider;
        private IApplicantQualificationsProvider _applicantQualificationsProvider;
        private IApplicantAttachementsProvider _applicantAttachementsProvider;
        private IApplicantContactDetailsProvider _applicantContactDatailsProvider;
        private IApplicantDeclarationProvider _applicantDeclarationProvider;
        private IApplicantProhibitionProvider _applicantProhibitionProvider;
        private IApplicantReferenceProvider _applicantReferenceProvider;
        private IApplicantLangProviciencyProvider _applicantLangProviciencyProvider;

        private ILanguageProvider _languageProvider;
        private IGenderProvider _genderProvider;
        private IInstitutionProvider _institutionProvider;
        private IProgrammeProvider _programmeProvider;
        private IRaceProvider _raceProvider;
        private ILanguageProficiencyProvider _languageProficiencyProvider;
        private IContactMethodProvider _contactMethodProvider;
        private IQualificationTypeProvider _qualificationTypeProvider;
        private IDocumentTypeProvider _documentTypeProvider;
        private IUserProvider _userProvider;
        private IApplicationProvider _applicationProvider;
        

        public ApplicantProfileController(IApplicantProfileProvider applicantProfileProvider, 
                                IApplicantExperienceProvider applicantExperienceProvider, 
                                IApplicantQualificationsProvider applicantQualificationsProvider,
                                IApplicantAttachementsProvider applicantAttachementsProvider,
                                IApplicantContactDetailsProvider applicantContactDatailsProvider,
                                IApplicantDeclarationProvider applicantDeclarationProvider,
                                IApplicantProhibitionProvider applicantProhibitionProvider, 
                                IApplicantReferenceProvider applicantReferenceProvider,
                                IApplicantLangProviciencyProvider applicantLangProviciencyProvider,
                                ILanguageProvider languageProvider,
                                IGenderProvider genderProvider,
                                IRaceProvider raceProvider,
                                ILanguageProficiencyProvider languageProficiencyProvider,
                                IContactMethodProvider contactMethodProvider,
                                IQualificationTypeProvider qualificationTypeProvider,
                                IDocumentTypeProvider documentTypeProvider,
                                IUserProvider userProvider,
                                IApplicationProvider applicationProvider,
                                IInstitutionProvider institutionProvider,
                                IProgrammeProvider programmeProvider)
        {
            _applicationProvider = applicationProvider;
            _applicantProfileProvider = applicantProfileProvider;
            _applicantExperienceProvider = applicantExperienceProvider;
            _applicantQualificationsProvider = applicantQualificationsProvider;
            _applicantAttachementsProvider = applicantAttachementsProvider;
            _applicantContactDatailsProvider = applicantContactDatailsProvider;
            _applicantDeclarationProvider = applicantDeclarationProvider;
            _applicantProhibitionProvider = applicantProhibitionProvider;
            _applicantLangProviciencyProvider = applicantLangProviciencyProvider;
            _applicantReferenceProvider = applicantReferenceProvider;
            _userProvider = userProvider;

            _languageProvider = languageProvider;
            _genderProvider = genderProvider;
            _raceProvider = raceProvider;
            _institutionProvider = institutionProvider;
            _languageProficiencyProvider = languageProficiencyProvider;
            _contactMethodProvider = contactMethodProvider;
            _qualificationTypeProvider = qualificationTypeProvider;
            _documentTypeProvider = documentTypeProvider;
            _programmeProvider = programmeProvider;
        }
        #endregion
       

        [Authorize, HttpPost, Route("addprofile")]
        public IHttpActionResult AddProfile(ApplicantProfile profile)
        {
            profile.Race = null;
            profile.Gender = null;
            profile.FirstNames = profile.FirstNames;
            profile.Surname = profile.Surname;
            profile.MaintUser = User.Identity.Name;
            profile.UserId = User.GetUserId();
            var user = _userProvider.GetUserByUserId(User.GetUserId());
            user.Surname = profile.Surname;
            user.FirstName = profile.FirstNames;

            _userProvider.UpdateUser(user);
            var result = _applicantProfileProvider.AddApplicantProfile(profile);
            
            return Ok(result);
        }

        [Authorize, HttpPost, Route("addqualification")]
        public IHttpActionResult AddQualification(ApplicantQualification qualification)
        {
            qualification.QualificationType = null;
            qualification.MaintUser = User.Identity.Name;
            qualification.MaintDate = DateTime.Now;
            var result = _applicantQualificationsProvider.AddApplicantQualification(qualification);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("deleteAttachment")]
        public IHttpActionResult DeleteAttachment(Attachment attachment)
        {
            var deleted = _applicantAttachementsProvider.DeleteAttachment(attachment);

            return Ok(deleted);
        }

        [Authorize, HttpPost, Route("updateapplicantqualification")]
        public IHttpActionResult UpdateApplicantQualification(ApplicantQualification qualification)
        {
            qualification.QualificationType = null;
            qualification.Institution = null;
            qualification.MaintUser = User.Identity.Name;
            qualification.MaintDate = DateTime.Now;
            var result = _applicantQualificationsProvider.UpdateApplicantQualification(qualification);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("removeapplicantqualification")]
        public IHttpActionResult RemoveApplicantQualification(ApplicantQualification qualification)
        {
            
            qualification.MaintUser = User.Identity.Name;
            qualification.MaintDate = DateTime.Now;
            _applicantQualificationsProvider.RemoveApplicantQualification(qualification);

            return Ok();
        }

        [Authorize, HttpPost, Route("addexperience")]
        public IHttpActionResult AddExperience(ApplicantExperience experience)
        {

            experience.MaintUser = User.Identity.Name; ;
            experience.MaintDate = DateTime.Now;
            var result = _applicantExperienceProvider.AddApplicantExperience(experience);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("updateexperience")]
        public IHttpActionResult UpdateExperience(ApplicantExperience experience)
        {

            experience.MaintUser = User.Identity.Name;
            experience.MaintDate = DateTime.Now;
            var result = _applicantExperienceProvider.UpdateApplicantExperience(experience);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("removeworkexperience")]
        public IHttpActionResult RemoveExperience(ApplicantExperience experience)
        {
            experience.MaintUser = User.Identity.Name;
            experience.MaintDate = DateTime.Now;
            _applicantExperienceProvider.RemoveApplicantExperience(experience);
            return Ok();
        }


        [Authorize, HttpPost, Route("addattachments")]
        public async Task<IHttpActionResult> AddAttachments(ApplicantAttachmentViewModel attachment)
        {
            IEnumerable<Attachment> result = null;
            try
            {
                string body = await Request.Content.ReadAsStringAsync();
                string idNo = "";

                attachment.MaintUser = User.Identity.Name;
                attachment.MaintDate = DateTime.Now;

                if(attachment.ApplicantProfile == null)
                {
                    var userid = User.GetUserId();
                    idNo = _applicantProfileProvider.GetApplicantProfileByUserId(userid).IdentityNumber;
                }
                else
                {
                    idNo = attachment.ApplicantProfile.IdentityNumber;
                }

               result  = _applicantAttachementsProvider.AddAttachment(FormatAttachments(attachment), idNo, attachment.AttachmentId);
      
            }
            catch (Exception ex)
            {

            }

            return Ok(result);
        }

        [Authorize, HttpPost, Route("removeattachments")]
        public IHttpActionResult RemoveAttachments(ApplicantAttachment attachment)
        {
            var MaintUser = "AllcationFailed";
            try
            {
                MaintUser = RequestContext.Principal.Identity.Name;
                if (MaintUser == null)
                {
                    MaintUser = "ERROR OBTAINING MAINT";
                }
            }
            catch
            {
                MaintUser = "ERROR OBTAINING MAINT";
            }
            attachment.MaintUser = MaintUser;
            attachment.MaintDate = DateTime.Now;
            var result = _applicantAttachementsProvider.RemoveApplicantAttchment(attachment.ApplicantAttachmentId, attachment.ApplicantProfileId);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("addcontactdatails")]
        public IHttpActionResult AddContactDetails(ApplicantContactDetail contactdetail)
        {
            contactdetail.ContactMethod = null;
            contactdetail.MaintUser = User.Identity.Name;
            contactdetail.MaintDate = DateTime.Now;
            var result = _applicantContactDatailsProvider.AddApplicantContactDetail(contactdetail);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("adddeclaration")]
        public IHttpActionResult AddDeclaretion(ApplicantDeclaration declaration)
        {
            
            declaration.AcceptedDeclarationOn = declaration.AcceptedDeclarationOn;
            declaration.MaintUser = User.Identity.Name;
            declaration.MaintDate = DateTime.Now;
            var result = _applicantDeclarationProvider.AddApplicantDeclaration(declaration);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("addprohibition")]
        public IHttpActionResult AddProhibition(ApplicantPubServProhibition prohibition)
        {
            prohibition.MaintUser = User.Identity.Name;
            prohibition.MaintDate = DateTime.Now;
            var result = _applicantProhibitionProvider.AddApplicantProhibition(prohibition);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("addreferences")]
        public IHttpActionResult AddReferences(ApplicantReference reference)
        {
            reference.MaintUser = User.Identity.Name;
            reference.MaintDate = DateTime.Now;
            var result = _applicantReferenceProvider.AddApplicantReference(reference);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("updatereferences")]
        public IHttpActionResult UpdateReferences(ApplicantReference reference)
        {
            reference.MaintUser = User.Identity.Name;
            reference.MaintDate = DateTime.Now;
            var result = _applicantReferenceProvider.UpdateApplicantReference(reference);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("removereference")]
        public IHttpActionResult RemoveReferences(ApplicantReference reference)
        {           
            reference.MaintUser = User.Identity.Name;
            reference.MaintDate = DateTime.Now;
            _applicantReferenceProvider.RemoveApplicantReference(reference);
            return Ok();
        }

        [Authorize, HttpPost, Route("addapplicantlangproficiencies")]
        public IHttpActionResult AddApplicantLangProficiencies(ApplicantLangProficiency proficiency)
        {

            proficiency.MaintUser = User.Identity.Name;
            proficiency.MaintDate = DateTime.Now;
            var result = _applicantLangProviciencyProvider.AddApplicantProficiency(proficiency);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("updateapplicantlangproficiencies")]
        public IHttpActionResult UpdateApplicantLangProficiencies(ApplicantLangProficiency proficiency)
        {
            proficiency.MaintUser = User.Identity.Name;
            proficiency.MaintDate = DateTime.Now;
            var result = _applicantLangProviciencyProvider.UpdateApplicantProficiency(proficiency);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("removeapplicantlangproficiency")]
        public IHttpActionResult RemoveApplicantLangProficiency(ApplicantLangProficiency proficiency)
        {
            proficiency.MaintUser = User.Identity.Name;
            proficiency.MaintDate = DateTime.Now;
            _applicantLangProviciencyProvider.RemoveApplicantProficiency(proficiency);
            return Ok();
        }
        [Authorize, HttpPost, Route("updateAccount")]
        public IHttpActionResult UpdateAccount(User user)   // Amit Kamal
        {
           
            _userProvider.UpdateAccount(user);
            return Ok();
        }

        [Authorize, HttpPost, Route("getprofile")]
        public IHttpActionResult GetProfile()
        {
            var userid = User.GetUserId();

            var result = _applicantProfileProvider.GetApplicantProfileByUserId(userid);// _applicantProfileProvider.;
            return Ok(result);
        }

        [Authorize, HttpPost, Route("updateprofile")]
        public IHttpActionResult UpdateProfile(ApplicantProfile profile)
        {
            var result = _applicantProfileProvider.UpdateApplicantProfile(profile);
            return Ok(result);
        }

        [Authorize, HttpPost, Route("getProfileInitialData")]
        public IHttpActionResult GetProfileInitialData()
        {
            var profile = _applicantProfileProvider.GetApplicantProfileByUserId(User.GetUserId());
            var languages = _languageProvider.GetAllLanguages();
            var genders = _genderProvider.GetAllGenders();
            var races = _raceProvider.GetAllRaces();
            var languageProficiencies = _languageProficiencyProvider.GetLanguageProficiencies();
            var contactMethods = _contactMethodProvider.GetContactMethods();
            var qualificationTypes = _qualificationTypeProvider.GetAllQaulificationTypes();
            var documentTypes = _documentTypeProvider.GetDocumentTypes();
            var user = _userProvider.GetUserByUserId(User.GetUserId());
            var institutions = _institutionProvider.GetAllInstitutions();
            var programmes = _programmeProvider.GetAllProgrammes();

            IEnumerable <ApplicantQualification> applicantQualifications = new List<ApplicantQualification>();
            ApplicantContactDetail applicantContactDetail = null;
            ApplicantPubServProhibition applicantPubServProhibition = null;
            ApplicantDeclaration applicantDeclaration = null;
            IEnumerable<ApplicantLangProficiency> applicantLangProficiencies = new List<ApplicantLangProficiency>();
            IEnumerable<ApplicantExperience> applicantExperiences = new List<ApplicantExperience>();
            IEnumerable<ApplicantReference> applicantReferences = new List<ApplicantReference>();
            //IEnumerable<ApplicantAttachment> applicantAttachments = new List<ApplicantAttachment>();
            IEnumerable<Attachment> attachments = new List<Attachment>();
            IEnumerable<Application> applicantApplications = new List<Application>();
            

            if (profile != null)
            {
                applicantContactDetail = _applicantContactDatailsProvider.GetApplicantContactDetail(profile.ApplicantProfileId);
                applicantLangProficiencies = _applicantLangProviciencyProvider.GetApplicantProficiencies(profile.ApplicantProfileId);
                applicantQualifications = _applicantQualificationsProvider.GetApplicantQualification(profile.ApplicantProfileId);
                applicantExperiences = _applicantExperienceProvider.GetApplicantExperience(profile.ApplicantProfileId);
                applicantReferences = _applicantReferenceProvider.GetApplicantReferences(profile.ApplicantProfileId);
                applicantPubServProhibition = _applicantProhibitionProvider.GetApplicantProhibition(profile.ApplicantProfileId);
                //applicantAttachments = _applicantAttachementsProvider.GetApplicantAttachments(profile.ApplicantProfileId);
                attachments = _applicantAttachementsProvider.GetAttachments(profile.ApplicantProfileId);
                applicantDeclaration = _applicantDeclarationProvider.GetApplicantDeclaration(profile.ApplicantProfileId);
                applicantApplications = _applicationProvider.GetApplicantApplication(user.Id);
            }

            var profileInitialData = new ProfileInitialData
            {
                User = new User
                {
                    DateOfBirth = user.DateOfBirth,
                    IdentityNumber = user.IdentityNumber,
                    Email = user.Email,
                    ModifiedBy = user.ModifiedBy,
                    Citizen = user.Citizen,
                    UserName = user.UserName, 
                    SecondaryEmail=user.SecondaryEmail

                },
                Languages = languages,
                Genders = genders,
                Races = races,
                Institutions = institutions,
                LanguageProficiencies = languageProficiencies,
                ContactMethods = contactMethods,
                ApplicantContactDetail = applicantContactDetail,
                ApplicantLangProficiencies = applicantLangProficiencies,
                Profile = profile,
                QualificationTypes = qualificationTypes,
                ApplicantQualifications = applicantQualifications,
                ApplicantExperiences = applicantExperiences,
                ApplicantReferences = applicantReferences,
                ApplicantPubServProhibition = applicantPubServProhibition,
                DocumentTypes = documentTypes,
                Attachments = attachments,
                //ApplicantAttachments = applicantAttachments,
                ApplicantDeclaration = applicantDeclaration,
                ApplicantApplications = applicantApplications,
                Programmes = programmes
                
            };
            return Ok(profileInitialData);
        }

        //[HttpGet]
        //[Route("getattachment/{attachmentId}")]
        //public object getApplicationAttachment(int attachmentId)
        //{
        //    var attachment = _applicantAttachementsProvider.getApplicantAttachmentById(attachmentId);
        //    return BinaryToFile(attachment.DocumentData, attachment.DocumentFormat, attachment.OriginalFileName);
        //}

        [HttpGet]
        [Route("getattachment/{attachmentId}")]
        public object getApplicationAttachment(int attachmentId)
        {
            var attachment = _applicantAttachementsProvider.getAttachmentById(attachmentId);
            var content  = System.IO.File.ReadAllBytes(attachment.FilePath);
            return BinaryToFile(content, attachment.DocumentFormat, attachment.OriginalFileName);
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

        private Attachment FormatAttachments(ApplicantAttachmentViewModel attachmentViewModel)
        {
            var bytes = Convert.FromBase64String(attachmentViewModel.DocumentData.ToString());

            Attachment attachment = new Attachment();
            attachment.ApplicantProfileId = attachmentViewModel.ApplicantProfileId;
            attachment.AttachmentId = attachmentViewModel.AttachmentId;
            attachment.DocumentData = bytes;
            attachment.DocumentDataText = attachmentViewModel.DocumentDataText;
            attachment.DocumentFormat = attachmentViewModel.DocumentFormat;
            attachment.DocumentTypeId = attachmentViewModel.DocumentTypeId;
            attachment.FilePath = attachmentViewModel.FilePath;
            attachment.IsNew = attachmentViewModel.IsNew;
            attachment.LocalFileName = attachmentViewModel.LocalFileName;
            attachment.MaintDate = attachmentViewModel.MaintDate;
            attachment.MaintUser = attachmentViewModel.MaintUser;
            attachment.OriginalFileName = attachmentViewModel.OriginalFileName;

            return attachment;
        }
    }

    public class ApplicantAttachmentViewModel
    {
        public int AttachmentId { get; set; }
        public int ApplicantProfileId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentFormat { get; set; }
        public object DocumentData { get; set; }
        public string FilePath { get; set; }
        public string LocalFileName { get; set; }
        public string OriginalFileName { get; set; }
        public string MaintUser { get; set; }
        public System.DateTime MaintDate { get; set; }
        public bool IsNew { get; set; }
        public string DocumentDataText { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}

