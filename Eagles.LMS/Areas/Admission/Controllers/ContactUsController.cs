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
    public class ContactUsController : Controller
    {
        // GET: Admission/ContactUs
        // GET: Admission/ContactUs
        public ActionResult Index()
        {
            return View(new UnitOfWork().ContactUsManager.GetAll().FirstOrDefault());
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        [HttpPost]
        public ActionResult Index(ContactUs ContactUs, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(ContactUs);


            RequestStatus requestStatus;
            if (ContactUs.Id == 0 && (uploadattachments == null || uploadattachments.ContentLength == 0 || !
                uploadattachments.ContentType.CheckImageExtention()) || (ContactUs.Id > 0 && uploadattachments != null && !
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
                    ContactUs.Logo = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                ContactUs.UserEditId = userId;
                ContactUs.EditeTime = DateTime.Now;
                var ctx = new UnitOfWork();
                if (ContactUs.Id == 0)
                    ContactUs = ctx.ContactUsManager.Add(ContactUs);
                else
                    ctx.ContactUsManager.UpdateWithSave(ContactUs);

                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = ContactUs.Id,
                    TableName = "Contact-Us",
                    Action = "Update:Contact-Us"
                });

                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);
                result = RedirectToAction(nameof(Index));



            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }

    }
}