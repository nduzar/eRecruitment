using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.DocumentTypes
{
    public interface IDocumentTypeManager
    {
        IEnumerable<DocumentType> GetDocumentTypes();
        DocumentType GetDocumentTypeById(int documentTypeId);
    }
}
