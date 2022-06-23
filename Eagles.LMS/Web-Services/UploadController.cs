using Eagles.LMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eagles.LMS.Models;
using Newtonsoft.Json.Linq;
using System.Web;
using Eagles.LMS.DTO;
using System.Threading.Tasks;
using System.IO;

namespace Eagles.LMS.Web_Services
{



    public class UploadController : ApiController
    {
        private readonly LMS.Data.EmcNewsContext ctx = new LMS.Data.EmcNewsContext();
        private readonly UnitOfWork ctx1 = new UnitOfWork();

        public static class FilePaths
        {

            public static string GetPath(int location)
            {
                switch (location)
                {
                    case 1:
                        return "~/attachments/QuestionsImages/";
                    case 2:
                        return "~/attachments/PriviteVideos/";
                    default:
                        return string.Empty;
                }
            }

        }


        [HttpPost]
        [Route("api/web/Upload/UploadFile/{location}")]
        public async Task<List<string>> UploadFile(int location)
        {
            var uniqueFileNames = new List<string>();
            var httpContext = HttpContext.Current;

            if (!Directory.Exists(httpContext.Server.MapPath(FilePaths.GetPath(location))))
            {
                Directory.CreateDirectory(httpContext.Server.MapPath(FilePaths.GetPath(location)));
            }
            var root = httpContext.Server.MapPath(FilePaths.GetPath(location));

            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                //remove double quotes from String.
                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;
                    name = name.Trim('"');
                    var uniqueName = Guid.NewGuid() + name;
                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, uniqueName);

                    File.Move(localFileName, filePath);
                    var _fileName = file.Headers.ContentDisposition.Name;
                    if (_fileName.Contains("AnswerVideo"))
                    {
                        uniqueName += "&AnswerVideoSelected";
                    }
                    uniqueFileNames.Add(uniqueName);
                }
                return uniqueFileNames;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }




    }
}
