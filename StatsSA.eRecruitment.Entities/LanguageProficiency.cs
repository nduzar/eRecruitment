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
    
    public partial class LanguageProficiency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LanguageProficiency()
        {
            this.ApplicantLangProficiencies = new HashSet<ApplicantLangProficiency>();
            this.ApplicantLangProficiencies1 = new HashSet<ApplicantLangProficiency>();
            this.ApplicantLangProficiencies2 = new HashSet<ApplicantLangProficiency>();
            this.VerApplicantLangProficiencies = new HashSet<VerApplicantLangProficiency>();
            this.VerApplicantLangProficiencies1 = new HashSet<VerApplicantLangProficiency>();
            this.VerApplicantLangProficiencies2 = new HashSet<VerApplicantLangProficiency>();
        }
    
        public int LanguageProficiencyId { get; set; }
        public string LanguageProficiencyDesc { get; set; }
        public string MaintUser { get; set; }
        public System.DateTime MaintDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicantLangProficiency> ApplicantLangProficiencies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicantLangProficiency> ApplicantLangProficiencies1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicantLangProficiency> ApplicantLangProficiencies2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantLangProficiency> VerApplicantLangProficiencies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantLangProficiency> VerApplicantLangProficiencies1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantLangProficiency> VerApplicantLangProficiencies2 { get; set; }
    }
}