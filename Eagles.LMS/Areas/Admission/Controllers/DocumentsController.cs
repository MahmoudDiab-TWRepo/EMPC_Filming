using Eagles.LMS.BLL;
using Eagles.LMS.Models;
using Eagles.LMS.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Areas.Admission.Controllers
{
    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]
    public class DocumentsController : Controller
    {
        // GET: Admission/Documents
        public ActionResult Index()
        {
            return View(new UnitOfWork().DocumentsManager.GetAll().FirstOrDefault());
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        [HttpPost]
        public ActionResult Index(Documents documents, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(documents);


            RequestStatus requestStatus;
            if (documents.Id == 0 && (uploadattachments == null || uploadattachments.ContentLength == 0 || !
                uploadattachments.ContentType.CheckImageExtention()) || (documents.Id > 0 && uploadattachments != null && !
                uploadattachments.ContentType.CheckImageExtention()))
            {

                requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");
            }
            else
            {


                if (uploadattachments != null)
                {

                    string _rendom = new Random().Next(1, 99999999).ToString();

                    var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);

                    var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                    uploadattachments.SaveAs(path);
                    documents.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                documents.UserEditId = userId;
                documents.EditeTime = DateTime.Now;
                var ctx = new UnitOfWork();
                if (documents.Id == 0)
                    documents = ctx.DocumentsManager.Add(documents);
                else
                    ctx.DocumentsManager.UpdateWithSave(documents);

                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);
                result = RedirectToAction(nameof(Index));
                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = documents.Id,
                    TableName = "Documents",
                    Action = "Update:Documents"
                });


            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }

    }
}