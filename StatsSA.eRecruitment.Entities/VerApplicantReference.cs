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
    
    public partial class VerApplicantReference
    {
        public int VerApplicantReferenceId { get; set; }
        public int VerApplicantProfileId { get; set; }
        public string ReferenceName { get; set; }
        public string ReferenceRelationship { get; set; }
        public string ReferenceContactNumber { get; set; }
        public string MaintUser { get; set; }
        public System.DateTime MaintDate { get; set; }
    
        public virtual VerApplicantProfile VerApplicantProfile { get; set; }
    }
}
