using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class ProfileInitialData
    {
        public ApplicantProfile Profile { get; set; }
        public User User { get; set; }
        public ApplicantContactDetail ApplicantContactDetail { get; set; }
        public ApplicantPubServProhibition ApplicantPubServProhibition { get; set; }
        public ApplicantDeclaration ApplicantDeclaration { get; set; }
        public IEnumerable<ApplicantExperience> ApplicantExperiences { get; set; }
        public IEnumerable<Race> Races { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        public IEnumerable<Institution> Institutions { get; set; }
        public IEnumerable<Programme> Programmes { get; set; }
        public IEnumerable<LanguageProficiency> LanguageProficiencies { get; set; }
        public IEnumerable<ApplicantLangProficiency> ApplicantLangProficiencies { get; set; }
        public IEnumerable<QualificationType> QualificationTypes { get; set; }
        public IEnumerable<ApplicantQualification> ApplicantQualifications { get; set; }
        public IEnumerable<ContactMethod> ContactMethods { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<ApplicantReference> ApplicantReferences { get; set; }
        public IEnumerable<DocumentType> DocumentTypes { get; set; }
        public IEnumerable<ApplicantAttachment> ApplicantAttachments { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }

        public IEnumerable<Application> ApplicantApplications { get; set; }
    }
}