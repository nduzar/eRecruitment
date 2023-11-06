using Newtonsoft.Json;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.ApplicantAttachments;
using StatsSA.eRecruitment.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [RoutePrefix("api/fileupload")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileUploadController : ApiController
    {
        private IApplicantAttachementsProvider _applicantAttachementsProvider;

        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Uploads";

        public FileUploadController(IApplicantAttachementsProvider applicantAttachementsProvider)
        {
            _applicantAttachementsProvider = applicantAttachementsProvider;
        }

        [HttpPost, Route("uploadfiles")] 
        public async Task<IHttpActionResult> UploadFiles(FileModel fileModel)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var bytes = Convert.FromBase64String(fileModel.contentAsBase64String);




            IEnumerable<ApplicantAttachment> applicantAttachments = null;

            //return this.Request.CreateResponse(HttpStatusCode.OK, applicantAttachments);
            return Ok("Testing");
        }

        [HttpPost, Route("uploadfilestodb")]
        public async Task<HttpResponseMessage> UploadFilesToDB()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }



            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            var originalFileName = GetDeserializedFileName(result.FileData.First());

            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            var fileData = ReadFile(result.FileData.First().LocalFileName);
            // Remove this line as well as GetFormData method if you're not
            // sending any form data with your upload request
            var fileUploadObj = GetFormData<UploadDataModel>(result) as UploadDataModel;

            IEnumerable<ApplicantAttachment> applicantAttachments = null;

            //applicantAttachments = _applicantAttachementsProvider.AddApplicantAttachment(new ApplicantAttachment
            //{
            //    ApplicantProfileId = fileUploadObj.ApplicantProfileId,
            //    LocalFileName = uploadedFileInfo.Name,
            //    FilePath = "~/App_Data/Tmp/FileUploads/",
            //    OriginalFileName = originalFileName,
            //    DocumentData = fileData,
            //    DocumentFormat = Path.GetExtension(originalFileName),
            //    MaintDate = DateTime.Now,
            //    MaintUser = User.Identity.Name,
            //    DocumentTypeId = fileUploadObj.DocumentTypeId
            //}, fileUploadObj.ApplicantAttachmentId);


            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            return this.Request.CreateResponse(HttpStatusCode.OK, applicantAttachments);
        }

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            // IMPORTANT: replace "(tilde)" with the real tilde character
            // (our editor doesn't allow it, so I just wrote "(tilde)" instead)
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            string applicantAttachmentId = "0";
            var documentTypeId = "0";
            if (result.FormData.HasKeys())
            {
                var applicantProfileId = Uri.UnescapeDataString(result.FormData
                    .GetValues(0).FirstOrDefault() ?? String.Empty);

                applicantAttachmentId = Uri.UnescapeDataString(result.FormData
                        .GetValues(1).FirstOrDefault() ?? String.Empty);

                documentTypeId = Uri.UnescapeDataString(result.FormData
                    .GetValues(2).FirstOrDefault() ?? String.Empty);

                return new UploadDataModel
                {
                    ApplicantProfileId = int.Parse(applicantProfileId),
                    ApplicantAttachmentId = int.Parse(applicantAttachmentId),
                    DocumentTypeId = int.Parse(documentTypeId),
                };
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        //Open file in to a filestream and read data in a byte array.
        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            //Close BinaryReader
            br.Close();

            //Close FileStream
            fStream.Close();

            return data;
        }

    }

    public class UploadDataModel
    {
        public int ApplicantProfileId { get; set; }
        public int ApplicantAttachmentId { get; set; }
        public int DocumentTypeId { get; set; }
    }

    public class FileModel
    {
        public string contentType { get; set; }

        public string contentAsBase64String { get; set; }

        public string fileName { get; set; }

    }
}
