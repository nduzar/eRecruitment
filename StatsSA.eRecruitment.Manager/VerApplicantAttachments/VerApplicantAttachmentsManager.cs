using StatsSA.eRecruitment.IManager.VerApplicantAttachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.VerApplicantAttachments
{
    public class VerApplicantAttachmentsManager : IVerApplicantAttachmentsManager
    {
        private readonly eRecruitmentEntities _context;
        public VerApplicantAttachmentsManager(eRecruitmentEntities context) 
        {
            _context = context;
        }
        public IEnumerable<VerApplicantAttachment> getVerApplicantAttachments(int VerApplicantProfileId)
        {
            return _context.VerApplicantAttachments.Where(attachment => attachment.VerApplicantProfileId == VerApplicantProfileId).ToList();
           // return base.GetMany(attachment => attachment.VerApplicantProfileId == VerApplicantProfileId);
        }

        public IEnumerable<VerAttachment> GetVerAttachments(int VerApplicantProfileId)
        {
            return _context.VerAttachments.Where(attachment => attachment.VerApplicantProfileId == VerApplicantProfileId).ToList();
            // return base.GetMany(attachment => attachment.VerApplicantProfileId == VerApplicantProfileId);
        }

        public VerApplicantAttachment getVerApplicantAttachmentById(int verApplicantAttachmentId)
        {
            return _context.VerApplicantAttachments.Where(attachment => attachment.VerApplicantAttachmentId == verApplicantAttachmentId).FirstOrDefault();
           // return base.Get(attachment => attachment.VerApplicantAttachmentId == verApplicantAttachmentId);
        }

        public VerAttachment GetVerAttachmentById(int verApplicantAttachmentId)
        {
            return _context.VerAttachments.Where(attachment => attachment.VerAttachmentId == verApplicantAttachmentId).FirstOrDefault();
            // return base.Get(attachment => attachment.VerApplicantAttachmentId == verApplicantAttachmentId);
        }
    }

}
