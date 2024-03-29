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
    
    public partial class DocumentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentType()
        {
            this.ApplicantAttachments = new HashSet<ApplicantAttachment>();
            this.VerApplicantAttachments = new HashSet<VerApplicantAttachment>();
            this.VerAttachments = new HashSet<VerAttachment>();
        }
    
        public int DocumentTypeId { get; set; }
        public string DocumentTypeDesc { get; set; }
        public string MaintUser { get; set; }
        public System.DateTime MaintDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicantAttachment> ApplicantAttachments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerApplicantAttachment> VerApplicantAttachments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerAttachment> VerAttachments { get; set; }
    }
}
