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
    
    public partial class PasswordRequestStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PasswordRequestStatus()
        {
            this.PasswordResetRequests = new HashSet<PasswordResetRequest>();
        }
    
        public int PasswordRequestStatusId { get; set; }
        public string PasswordRequestStatusDesc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PasswordResetRequest> PasswordResetRequests { get; set; }
    }
}