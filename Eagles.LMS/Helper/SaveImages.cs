using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Helper
{
    public class SaveImages
    {

        public string SaveImage(string image)
        {


            //this is a simple white background image
            var myfilename = string.Format(@"{0}", Guid.NewGuid());

            //Generate unique filenameD:\ali\lms\learning mangment system\Eagles.LMS\Eagles.LMS\Areas\Admission\UploadedFiles\
            string filepath = HttpContext.Current.Request.PhysicalApplicationPath + "/attachments/UserImages/" + myfilename + ".jpeg";
            var bytess = Convert.FromBase64String(image);
            using (var imageFile = new FileStream(filepath, FileMode.Create))
            {
                imageFile.Write(bytess, 0, bytess.Length);
                imageFile.Flush();
            }

            return filepath= "~/attachments/UserImages/" + myfilename + ".jpeg"; 
        }
        public static string SaveQuestionImage(string image)
        {
            try
            {

                //this is a simple white background image
                var myfilename = string.Format(@"{0}", Guid.NewGuid());

                //Generate unique filenameD:\ali\lms\learning mangment system\Eagles.LMS\Eagles.LMS\Areas\Admission\UploadedFiles\
                string filepath = HttpContext.Current.Request.PhysicalApplicationPath + "/attachments/QuestionsImages/" + myfilename + ".jpeg";
                var bytess = Convert.FromBase64String(image);
                using (var imageFile = new FileStream(filepath, FileMode.Create))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }

                return filepath = myfilename + ".jpeg";
            }
            catch (Exception)
            {
                return "-";
               
            }

        }
    }
}