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
    public class WhoWeAreController : Controller
    {
        // GET: Admission/WhoWeAre
        public ActionResult Index()
        {
            return View(new UnitOfWork().WhoWeAreManager.GetAll().FirstOrDefault());
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        [HttpPost]
        public ActionResult Index(WhoWeAre whoweare, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(whoweare);


            RequestStatus requestStatus;
            if (whoweare.Id == 0 && (uploadattachments == null || uploadattachments.ContentLength == 0 || !
                uploadattachments.ContentType.CheckImageExtention()) || (whoweare.Id > 0 && uploadattachments != null && !
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
                    whoweare.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                whoweare.UserEditId = userId;
                whoweare.EditeTime = DateTime.Now;
                var ctx = new UnitOfWork();
                if (whoweare.Id == 0)
                    whoweare = ctx.WhoWeAreManager.Add(whoweare);
                else
                    ctx.WhoWeAreManager.UpdateWithSave(whoweare);

                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);
                result = RedirectToAction(nameof(Index));
                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = whoweare.Id,
                    TableName = "WhoWeAre",
                    Action = "Update:WhoWeAre"
                });


            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }

    }
}