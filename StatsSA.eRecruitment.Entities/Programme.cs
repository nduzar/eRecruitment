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
    
    public partial class Programme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Programme()
        {
            this.ApplicationRequirements = new HashSet<ApplicationRequirement>();
        }
    
        public int ProgrammeId { get; set; }
        public string ProgrammeDesc { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<System.DateTime> MaintDate { get; set; }
        public string MaintUser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationRequirement> ApplicationRequirements { get; set; }
    }
}
