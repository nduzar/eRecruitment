using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.VerApplicantAttachments

{
    public interface IVerApplicantAttachmentsManager
    {
        IEnumerable<VerApplicantAttachment> getVerApplicantAttachments(int VerApplicantProfileId);
        VerApplicantAttachment getVerApplicantAttachmentById(int verApplicantAttachmentId);

        IEnumerable<VerAttachment> GetVerAttachments(int VerApplicantProfileId);

        VerAttachment GetVerAttachmentById(int verApplicantAttachmentId);

    }
 
}
