using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.DocumentTypes
{
    public interface IDocumentTypeProvider
    {
        IEnumerable<DocumentType> GetDocumentTypes();

        DocumentType GetDocumentTypeById(int documentTypeId);
    }
}
