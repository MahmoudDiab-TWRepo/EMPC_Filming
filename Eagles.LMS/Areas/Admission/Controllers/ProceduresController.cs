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
    public class ProceduresController : Controller
    {
        // GET: Admission/Procedures
        public ActionResult Index()
        {
            return View(new UnitOfWork().ProceduresManager.GetAll().FirstOrDefault());
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        [HttpPost]
        public ActionResult Index(Procedures procedures, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(procedures);


            RequestStatus requestStatus;
            if (procedures.Id == 0 && (uploadattachments == null || uploadattachments.ContentLength == 0 || !
                uploadattachments.ContentType.CheckImageExtention()) || (procedures.Id > 0 && uploadattachments != null && !
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
                    procedures.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                procedures.UserEditId = userId;
                procedures.EditeTime = DateTime.Now;
                var ctx = new UnitOfWork();
                if (procedures.Id == 0)
                    procedures = ctx.ProceduresManager.Add(procedures);
                else
                    ctx.ProceduresManager.UpdateWithSave(procedures);

                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);
                result = RedirectToAction(nameof(Index));
                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = procedures.Id,
                    TableName = "Procedures",
                    Action = "Update:Procedures"
                });


            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }


    }
}