using StatsSA.eRecruitment.IProvider.ApplicantAttachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using System.IO;

namespace StatsSA.eRecruitment.Provider.ApplicantAttachments
{
    public class ApplicantAttachementsProvider : IApplicantAttachementsProvider
    {
        private IUnitOfWork _unitOfWork;

        private readonly string _path = @"C:\\E-Recruitment\Attachments\";

        public ApplicantAttachementsProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        //TODO: Remove this code once everything is working
        public IEnumerable<ApplicantAttachment> AddApplicantAttachment(ApplicantAttachment attachment, int applicantAttachmentId = 0)
        {
            this._unitOfWork.BeginTransaction();
            var oldAttachment = this.getApplicantAttachmentById(attachment.ApplicantAttachmentId);
            if (oldAttachment != null)
            {
                this._unitOfWork.ApplicantAttachementsManager.removeApplicantAttachment(oldAttachment.ApplicantAttachmentId);
                this._unitOfWork.SaveChanges();
            }
            attachment.ApplicantAttachmentId = 0;
            attachment.IsNew = true;

            this._unitOfWork.ApplicantAttachementsManager.AddApplicantAttachment(attachment);
            this._unitOfWork.SaveChanges();

            var listOfAttachments = this._unitOfWork.ApplicantAttachementsManager.getApplicantAttachments(attachment.ApplicantProfileId);
            this._unitOfWork.CommitTransaction();
            foreach(ApplicantAttachment attach in listOfAttachments)
            {
                attach.DocumentData = null;
            }
            return listOfAttachments;
        }

        public IEnumerable<Attachment> AddAttachment(Attachment attachment, string idNo, int applicantAttachmentId = 0)
        {
            this._unitOfWork.BeginTransaction();
            var oldAttachment = this.getAttachmentById(attachment.AttachmentId);
            var oldAttachmentByProfile = this.getAttachmentByProfileId(attachment.ApplicantProfileId, attachment.DocumentTypeId);
            if (oldAttachment != null)
            {
                this._unitOfWork.ApplicantAttachementsManager.removeAttachment(oldAttachment.AttachmentId);
                this._unitOfWork.SaveChanges();
            }

            if(oldAttachment != null)
            {
                this._unitOfWork.ApplicantAttachementsManager.removeAttachmentByProfile(attachment.ApplicantProfileId, attachment.DocumentTypeId);
                this._unitOfWork.SaveChanges();
            }
            attachment.AttachmentId = 0;
            attachment.IsNew = true;

            attachment.FilePath = string.Format(@"{0}\{1}\{2}", _path, idNo, attachment.OriginalFileName);
            attachment.DocumentData = attachment.DocumentData;

            SaveFileToFolder(attachment);

            attachment.DocumentData = null;

            this._unitOfWork.ApplicantAttachementsManager.AddAttachment(attachment);
            this._unitOfWork.SaveChanges();           
            this._unitOfWork.CommitTransaction();

           

            var listOfAttachments = this._unitOfWork.ApplicantAttachementsManager.getAttachments(attachment.ApplicantProfileId);

            foreach (Attachment attach in listOfAttachments)
            {
                attach.DocumentData = null;
            }
            return listOfAttachments;
        }

        public IEnumerable<ApplicantAttachment> GetApplicantAttachments(int applicantProfileId)
        {
            var applicantAttachments = this._unitOfWork.ApplicantAttachementsManager.getApplicantAttachments(applicantProfileId);
            return applicantAttachments;
        }

        public IEnumerable<Attachment> GetAttachments(int applicantProfileId)
        {
            var attachments = this._unitOfWork.ApplicantAttachementsManager.getAttachments(applicantProfileId);
            return attachments;
        }

        public IEnumerable<ApplicantAttachment> RemoveApplicantAttchment(int attachmentId, int applicantProfileId)
        {

            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantAttachementsManager.removeApplicantAttachment(attachmentId);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            var applicantAttachments = this._unitOfWork.ApplicantAttachementsManager.getApplicantAttachments(applicantProfileId);
            return applicantAttachments;
        }

        public bool DeleteAttachment(Attachment attachment)
        {
            bool deleted = false;

            try
            {
                var filePath = this._unitOfWork.ApplicantAttachementsManager.getAttachmentById(attachment.AttachmentId).FilePath;
                attachment.FilePath = filePath;
                this._unitOfWork.ApplicantAttachementsManager.removeAttachment(attachment.AttachmentId);
                this._unitOfWork.SaveChanges();
                DeleteFileFromFolder(attachment);
                deleted = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return deleted;

        }

        public IEnumerable<ApplicantAttachment> UpdateApplicantAttachment(ApplicantAttachment attachment)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantAttachementsManager.UpdateApplicantAttachment(attachment.ApplicantAttachmentId);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            var applicantAttachments = this._unitOfWork.ApplicantAttachementsManager.getApplicantAttachments(attachment.ApplicantProfileId);
            return applicantAttachments;
        }
        public ApplicantAttachment getApplicantAttachmentById(int applicantAttachmentId)
        {
            return this._unitOfWork.ApplicantAttachementsManager.getApplicantAttachmentById(applicantAttachmentId);
        }

        public Attachment getAttachmentById(int applicantAttachmentId)
        {
            return this._unitOfWork.ApplicantAttachementsManager.getAttachmentById(applicantAttachmentId);
        }

        public Attachment getAttachmentByProfileId(int profileId, int documentTypeId)
        {
            return this._unitOfWork.ApplicantAttachementsManager.getAttachmentByProfileId(profileId, documentTypeId);
        }

        private void SaveFileToFolder(Attachment attachment)
        {
            var bytes = attachment.DocumentData;

            if (!Directory.Exists(Path.GetDirectoryName(attachment.FilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(attachment.FilePath));

            FileStream file = File.Create(attachment.FilePath);

            file.Write(bytes, 0, bytes.Length);

            file.Close();
        }

        private void DeleteFileFromFolder(Attachment attachment)
        {
            var bytes = attachment.DocumentData;

            if (Directory.Exists(Path.GetDirectoryName(attachment.FilePath)))
            {
                File.Delete(attachment.FilePath);
            }
        }
    }
}
