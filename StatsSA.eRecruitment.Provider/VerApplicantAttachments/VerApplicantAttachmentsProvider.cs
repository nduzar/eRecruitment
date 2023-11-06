using StatsSA.eRecruitment.IProvider.VerApplicantAttachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.VerApplicantAttachments
{
    public class VerApplicantAttachmentsProvider : IVerApplicantAttachmentProvider
    {
        private IUnitOfWork _unitOfWork;
        public VerApplicantAttachmentsProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<VerApplicantAttachment> GetVerApplicantAttachments(int VerApplicantProfileId)
        {
           return this._unitOfWork.VerApplicantAttachmentsManager.getVerApplicantAttachments(VerApplicantProfileId);
        }

        public IEnumerable<VerAttachment> GetVerAttachments(int VerApplicantProfileId)
        {
            return this._unitOfWork.VerApplicantAttachmentsManager.GetVerAttachments(VerApplicantProfileId);
        }

        public VerApplicantAttachment GetVerApplicantAttachmentById(int VerApplicantAttachmentId)
        {
            return this._unitOfWork.VerApplicantAttachmentsManager.getVerApplicantAttachmentById(VerApplicantAttachmentId);
        }

        public VerAttachment GetVerAttachmentById(int VerApplicantAttachmentId)
        {
            return this._unitOfWork.VerApplicantAttachmentsManager.GetVerAttachmentById(VerApplicantAttachmentId);
        }
    }
}
