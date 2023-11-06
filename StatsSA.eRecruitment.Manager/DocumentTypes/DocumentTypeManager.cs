using StatsSA.eRecruitment.IManager.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.DocumentTypes
{
    public class DocumentTypeManager : IDocumentTypeManager
    {
        private eRecruitmentEntities _context;

        public DocumentTypeManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public IEnumerable<DocumentType> GetDocumentTypes()
        {
            return _context.DocumentTypes.ToList();
        }
        public DocumentType GetDocumentTypeById(int documentTypeId)
        {
            return _context.DocumentTypes.FirstOrDefault(docType => docType.DocumentTypeId == documentTypeId);
        }
    }
}
