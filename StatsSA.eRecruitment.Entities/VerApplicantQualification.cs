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
    
    public partial class VerApplicantQualification
    {
        public int VerApplicantQualificationId { get; set; }
        public int VerApplicantProfileId { get; set; }
        public int QualificationTypeId { get; set; }
        public string NameOfQualification { get; set; }
        public Nullable<int> YearObtained { get; set; }
        public bool CurrentStudies { get; set; }
        public string MaintUser { get; set; }
        public System.DateTime MaintDate { get; set; }
        public int InstitutionId { get; set; }
        public string OtherInstitution { get; set; }
    
        public virtual Institution Institution { get; set; }
        public virtual QualificationType QualificationType { get; set; }
        public virtual VerApplicantProfile VerApplicantProfile { get; set; }
    }
}
