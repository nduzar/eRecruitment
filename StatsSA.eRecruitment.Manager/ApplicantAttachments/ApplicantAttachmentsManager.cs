using StatsSA.eRecruitment.IManager.ApplicantAttachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using System.Data.Entity;
using System.IO;

namespace StatsSA.eRecruitment.Manager.ApplicantAttachments
{
    public class ApplicantAttachmentManager : IApplicantAttachmentsManager
    {
        private readonly eRecruitmentEntities _context;
       
        public ApplicantAttachmentManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public void AddApplicantAttachment(ApplicantAttachment attachment)
        {
            _context.ApplicantAttachments.Add(attachment);
        }

        public void AddAttachment(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
        }

        public IEnumerable<ApplicantAttachment> getApplicantAttachments(int ApplicantProfileId)
        {           
             return _context.ApplicantAttachments.Where(att => att.ApplicantProfileId == ApplicantProfileId && att.IsNew).ToList();
        }

        public void removeApplicantAttachment(int applicantAttachmentId)
        {
            var attachments = _context.ApplicantAttachments.Where(a => a.ApplicantAttachmentId == applicantAttachmentId).ToList();

            if (attachments.Any())
            {
                _context.ApplicantAttachments.RemoveRange(attachments);
            }
        }

        public void removeAttachment(int applicantAttachmentId)
        {
            var attachments = _context.Attachments.Where(a => a.AttachmentId == applicantAttachmentId).ToList();

            if (attachments.Any())
            {
                _context.Attachments.RemoveRange(attachments);
            }
        }

        public void removeAttachmentByProfile(int profileId, int documentTypeId)
        {
            var attachments = _context.Attachments.Where(a => a.ApplicantProfileId == profileId && a.DocumentTypeId == documentTypeId).ToList();

            if (attachments.Any())
            {
                _context.Attachments.RemoveRange(attachments);
            }
        }

        public void UpdateApplicantAttachment(int applicantAttachmentId)
        {
            var att = _context.ApplicantAttachments.FirstOrDefault(a => a.ApplicantAttachmentId == applicantAttachmentId);

            if(att != null)
            {
                att.IsNew = false;
                _context.ApplicantAttachments.Attach(att);
                _context.Entry(att).State = EntityState.Modified;
            }
        }
        public ApplicantAttachment getApplicantAttachmentById(int applicantAttachmentId)
        {
            return _context.ApplicantAttachments.FirstOrDefault(attachment => attachment.ApplicantAttachmentId == applicantAttachmentId);
        }

        public Attachment getAttachmentById(int applicantAttachmentId)
        {
            return getAttachment(applicantAttachmentId);
        }

        public Attachment getAttachmentByProfileId(int profileId, int documentTypeId)
        {
            return getAttachment(profileId, documentTypeId);
        }

        public IEnumerable<Attachment> getAttachments(int ApplicantProfileId)
        {
            return getAllAttachments(ApplicantProfileId);
        }

        private IEnumerable<Attachment> getAllAttachments(int ApplicantProfileId)
        {
            var files = _context.Attachments.Where(att => att.ApplicantProfileId == ApplicantProfileId && att.IsNew).ToList();

            List<Attachment> attachments = new List<Attachment>();

            foreach (var file in files)
            {
                try
                {
                    FileStream stream = File.OpenRead(file.FilePath);
                    byte[] content = new byte[stream.Length];
                    stream.Read(content, 0, (int)stream.Length);
                    stream.Close();
                    file.DocumentData = content;
                    attachments.Add(file);
                }
                catch(Exception ex)
                {

                }
            }
            return attachments;
        }

        private Attachment getAttachment(int attachmentId)
        {
            return _context.Attachments.Where(att => att.AttachmentId == attachmentId).FirstOrDefault();
        }

        private Attachment getAttachment(int applicationProfileId, int documentTypeId)
        {
            return _context.Attachments.Where(att => att.ApplicantProfileId == applicationProfileId && att.DocumentTypeId == documentTypeId).FirstOrDefault();
        }

        //private Attachment getAttachment(int attachmentId)
        //{
        //    var file = _context.Attachments.Where(att => att.AttachmentId == attachmentId).FirstOrDefault();

        //    if (file != null)
        //    {
        //        FileStream stream = File.OpenRead(file.FilePath);
        //        byte[] content = new byte[stream.Length];
        //        stream.Read(content, 0, (int)stream.Length);
        //        stream.Close();
        //        file.DocumentData = content;
        //    }

        //    return file;
        //}
    }

}
