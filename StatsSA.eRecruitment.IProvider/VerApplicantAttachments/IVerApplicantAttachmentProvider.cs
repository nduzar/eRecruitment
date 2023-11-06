using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.VerApplicantAttachments
{
    public interface IVerApplicantAttachmentProvider
    {
        IEnumerable<VerApplicantAttachment> GetVerApplicantAttachments(int VerApplicantProfileId);

        VerApplicantAttachment GetVerApplicantAttachmentById(int VerApplicantAttachmentId);

        IEnumerable<VerAttachment> GetVerAttachments(int VerApplicantProfileId);

        VerAttachment GetVerAttachmentById(int VerApplicantAttachmentId);
    }
}
