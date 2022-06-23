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
    public class AboutUSController : Controller
    {
        // GET: Admission/AboutUS
        public ActionResult Index()
        {
            return View(new UnitOfWork().AboutUsManager.GetAll().FirstOrDefault());
        }

        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }

        [HttpPost]
        public ActionResult Index(AboutUs aboutUs, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(aboutUs);


            RequestStatus requestStatus;
            if (aboutUs.Id == 0 && (uploadattachments == null || uploadattachments.ContentLength == 0 || !
                uploadattachments.ContentType.CheckImageExtention()) || (aboutUs.Id > 0 && uploadattachments != null && !
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
                    aboutUs.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                aboutUs.UserEditId = userId;
                aboutUs.EditeTime = DateTime.Now;
                var ctx = new UnitOfWork();
                if (aboutUs.Id == 0)
                    aboutUs = ctx.AboutUsManager.Add(aboutUs);
                else
                    ctx.AboutUsManager.UpdateWithSave(aboutUs);

                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);
                result = RedirectToAction(nameof(Index));
                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = aboutUs.Id,
                    TableName = "AboutUs",
                    Action = "Update:About Us"
                });


            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }


    }
}