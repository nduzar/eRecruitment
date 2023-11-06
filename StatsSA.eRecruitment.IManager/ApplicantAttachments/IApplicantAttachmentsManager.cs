using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ApplicantAttachments

{
    public interface IApplicantAttachmentsManager
    {
        void AddApplicantAttachment(ApplicantAttachment attachment);
        void UpdateApplicantAttachment(int applicantAttachmentId);
        IEnumerable<ApplicantAttachment> getApplicantAttachments(int ApplicantProfileId);

        void removeApplicantAttachment(int ApplicantAttachmentId);

        ApplicantAttachment getApplicantAttachmentById(int applicantAttachmentId);
        IEnumerable<Attachment> getAttachments(int ApplicantProfileId);
        Attachment getAttachmentById(int applicantAttachmentId);
        void AddAttachment(Attachment attachment);
        void removeAttachment(int applicantAttachmentId);
        Attachment getAttachmentByProfileId(int profileId, int documentTypeId);
        void removeAttachmentByProfile(int profileId, int documentTypeId);
    }
 
}
