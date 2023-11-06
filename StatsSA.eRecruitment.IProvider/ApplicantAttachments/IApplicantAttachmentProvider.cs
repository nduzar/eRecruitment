using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicantAttachments
{
    public interface IApplicantAttachementsProvider
    {
        IEnumerable<ApplicantAttachment> AddApplicantAttachment(ApplicantAttachment attachment, int applicantAttachmentId = 0);
        IEnumerable<ApplicantAttachment> UpdateApplicantAttachment(ApplicantAttachment attachment);
        IEnumerable<ApplicantAttachment> GetApplicantAttachments(int applicantProfileId);

        IEnumerable<Attachment> GetAttachments(int applicantProfileId);

        IEnumerable<ApplicantAttachment> RemoveApplicantAttchment(int applicantAttachmentId, int applicantProfileId);

        ApplicantAttachment getApplicantAttachmentById(int applicantAttachmentId);

        Attachment getAttachmentById(int applicantAttachmentId);

        bool DeleteAttachment(Attachment attachment);

        IEnumerable<Attachment> AddAttachment(Attachment attachment, string idNo, int applicantAttachmentId = 0);
    }
}
