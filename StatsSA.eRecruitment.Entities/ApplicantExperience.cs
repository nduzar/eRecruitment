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
    
    public partial class ApplicantExperience
    {
        public int ApplicantExperienceId { get; set; }
        public int ApplicantProfileId { get; set; }
        public string Employer { get; set; }
        public string PostHeld { get; set; }
        public System.DateTime DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public string ReasonForLeaving { get; set; }
        public string MaintUser { get; set; }
        public System.DateTime MaintDate { get; set; }
    
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}
