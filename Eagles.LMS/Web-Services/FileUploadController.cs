using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eagles.LMS.Web_Services
{
    public class FileUploadController : ApiController
    {
        [HttpPost()]
        [Route("api/fileupload")]
        public string UploadFiles()
        {
            int iUploadedCnt = 0;
            //<span class="comments">// DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.</span
            string sPath = "";
          sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/locker/");
 
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
 
           // <span class="comments">// CHECK THE FILE COUNT.</span>
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];
 
                if (hpf.ContentLength > 0)
                {
                   // <span class="comments">// CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)</span>
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                     //   <span class="comments">// SAVE THE FILES IN THE FOLDER.</span>
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        iUploadedCnt = iUploadedCnt + 1;
                    }
}
            }
 
           // <span class="comments">// RETURN A MESSAGE.</span>
            if (iUploadedCnt > 0) {
                return iUploadedCnt + " Files Uploaded Successfully";
            }
            else {
                return "Upload Failed";
            }
        }
    }
}
