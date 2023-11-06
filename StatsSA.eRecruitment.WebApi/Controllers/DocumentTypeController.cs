using StatsSA.eRecruitment.IProvider.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DocumentTypeController : ApiController
    {
        private IDocumentTypeProvider _documentTypeProvider;
        public DocumentTypeController(IDocumentTypeProvider documentTypeProvider)
        {
            _documentTypeProvider = documentTypeProvider;
        }

        [HttpPost, Route("GetDocumentTypes")]
        public IHttpActionResult GetDocumentTypes()
        {
            var result = _documentTypeProvider.GetDocumentTypes();
            return Ok(result);
        }
    }
}
