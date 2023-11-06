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
    
    public partial class HRUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRUser()
        {
            this.HRUserRoles = new HashSet<HRUserRole>();
        }
    
        public int HRUserId { get; set; }
        public string HRUserName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int HRUserRoleId { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public bool IsActive { get; set; }
        public string MaintUser { get; set; }
        public System.DateTime MaintDate { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRUserRole> HRUserRoles { get; set; }
    }
}
