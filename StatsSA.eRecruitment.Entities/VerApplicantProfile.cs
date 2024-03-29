//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StatsSA.eRecruitment.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class VerApplicantProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VerApplicantProfile()
        {
            this.Applications = new HashSet<Application>();
            this.VerApplicantAttachments = new HashSet<VerApplicantAttachment>();
            this.VerApplicantContactDetails = new HashSet<VerApplicantContactDetail>();
            this.VerApplicantExperiences = new HashSet<VerApplicantExperience>();
            this.VerApplicantLangProficiencies = new HashSet<VerApplicantLangProficiency>();
            this.VerApplicantReferences = new HashSet<VerApplicantReference>();
            this.VerApplicantQualifications = new HashSet<VerApplicantQualification>();
            this.VerAttachments = new HashSet<VerAttachment>();
        }
    
        public int VerApplicantProfileId { get; set; }
        public int ApplicantProfileId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Surname { get; set; }
        public string FirstNames { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string IdentityNumber { get; set; }
        public int RaceId { get; set; }
        public int GenderId { get; set; }
        public bool Disability { get; set; }
        public string DisabilityDesc { get; set; }
        public bool Citizen { get; set; }
        public string Nationality { get; set; }
        public Nullable<bool> WorkPermit { get; set; }
        public bool PreviousDismissal { get; set; }
        public bool CriminalOffence { get; set; }
        public Nullable<System.DateTime> OccupationRegistrationDate { get; set; }
        public string OccupationRegistrationDesc { get; set; }
        public string MaintUser { get; set; }
        public System.DateTime MaintDate { get; set; }
        public string CriminalOffenceDesc { get; set; }
        public string PreviousDismissedReason { get; set; }
        public Nullable<bool> PendingCriminalCase { get; set; }
        public string PendingCriminalDesc { get; set; }
        public Nullable<bool> PublicServiceDismiss { get; set; }
        public string PublicServiceDismissDesc { get; set; }
        public Nullable<bool> PendingDisciplinary { get; set; }
        public string PendingDisciplinaryDesc { get; set; }
        public Nullable<int> NumberOfExperiencePublic { get; set; }
        public Nullable<int> NumberOfExperiencePrivate { get; set; }
        public Nullable<System.DateTime> OfficialRegistrationDate { get; set; }
        public string OfficialRegistrationNumber { get; set; }
        public Nullable<bool> ResignedPendingDisciplinary { get; set; }
        public string ResignedPendingDisciplinaryDesc { get; set; }
        public Nullable<bool> DischargedOrRetired { get; set; }
        public Nullable<bool> BusinessWithState { get; set; }
        public string BusinessWithStateDesc { get; set; }
        public Nullable<bool> RelinquishBusinessInterest { get; set; }
        public string RaceOtherDesc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application> Applications { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Race Race { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantAttachment> VerApplicantAttachments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantContactDetail> VerApplicantContactDetails { get; set; }
        public virtual VerApplicantDeclaration VerApplicantDeclaration { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantExperience> VerApplicantExperiences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantLangProficiency> VerApplicantLangProficiencies { get; set; }
        public virtual VerApplicantPubServProhibition VerApplicantPubServProhibition { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantReference> VerApplicantReferences { get; set; }
        public virtual ApplicantProfile ApplicantProfile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantQualification> VerApplicantQualifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerAttachment> VerAttachments { get; set; }
    }
}
