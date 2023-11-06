using StatsSA.eRecruitment.IProvider.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.DocumentTypes
{
    public class DocumentTypeProvider : IDocumentTypeProvider
    {
        private IUnitOfWork _unitOfWork;
        public DocumentTypeProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<DocumentType> GetDocumentTypes()
        {
            return _unitOfWork.DocumentTypeManager.GetDocumentTypes();
        }
        QualificationType GetQualificationTypeById(int qualificationTypeId)
        {
            return _unitOfWork.QualificicationTypeManager.GetQualificationTypeById(qualificationTypeId);
        }
        public DocumentType GetDocumentTypeById(int documentTypeId)
        {
            return _unitOfWork.DocumentTypeManager.GetDocumentTypeById(documentTypeId);
        }
    }
}
