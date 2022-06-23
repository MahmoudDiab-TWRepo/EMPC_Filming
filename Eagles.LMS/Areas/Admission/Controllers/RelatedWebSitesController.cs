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
    public class RelatedWebSitesController : Controller
    {
        // GET: Admission/RelatedWebSites
        public ActionResult Index()
        {
            return View(new UnitOfWork().RelatedWebSiteManager.GetAll().OrderByDescending(s => s.Id).ToList());
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        public ActionResult Edit(int id)
        {
            var entity = new UnitOfWork().RelatedWebSiteManager.GetBy(id);
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }
        [HttpPost]
        public ActionResult Edit(RelatedWebSite relatedWebSite, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(relatedWebSite);


            RequestStatus requestStatus;
            if (uploadattachments != null && !
                uploadattachments.ContentType.CheckImageExtention())
            {

                requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");
            }
            else
            {


                if (uploadattachments != null)
                {

                    string _rendom = new Random().Next(1, 99999999).ToString();

                    //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                    string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                    var fileName = _rendom + extention;

                    var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                    uploadattachments.SaveAs(path);
                    relatedWebSite.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                relatedWebSite.UserEditId = userId;
                relatedWebSite.EditeTime = DateTime.Now;


                var ctx = new UnitOfWork();
                ctx.RelatedWebSiteManager.UpdateWithSave(relatedWebSite);
                var user = ctx.UserManager.GetById(GetUserId());



                ctx.logManager.Add(new log
                {
                    UserId = GetUserId(),
                    ActionTime = DateTime.Now,
                    EntityId = relatedWebSite.Id,
                    TableName = "Related-Website",
                    Action = "Update:Related-Website",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = relatedWebSite.TitleArabic

                });
             
                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RelatedWebSite relatedWebSite, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(relatedWebSite);

            if (ModelState.IsValid)
            {

                RequestStatus requestStatus;
                if (uploadattachments == null || uploadattachments.ContentLength == 0 || !
                    uploadattachments.ContentType.CheckImageExtention())
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");
                }
                else
                {




                    string _rendom = new Random().Next(1, 99999999).ToString();

                    //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                    string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                    var fileName = _rendom + extention;


                    var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                    uploadattachments.SaveAs(path);
                    relatedWebSite.Image = $"/attachments/{fileName}";

                    int userId = GetUserId();
                    relatedWebSite.UserCreateId = userId;
                    relatedWebSite.CreateTime = DateTime.Now;
                    relatedWebSite.UserEditId = userId;
                    relatedWebSite.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();
                    ctx.RelatedWebSiteManager.Add(relatedWebSite);
                    var user = ctx.UserManager.GetById(GetUserId());




                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = relatedWebSite.Id,
                        TableName = "Related-Website",
                        Action = "Create:Related-Website",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = relatedWebSite.TitleArabic

                    });
          
                    requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                    result = RedirectToAction(nameof(Create));



                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.RelatedWebSiteManager.GetBy(id);

            ctx.RelatedWebSiteManager.Delete(entity);
            var user = ctx.UserManager.GetById(GetUserId());



            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Related-Website",
                Action = "Delete:Related-Website",
                    LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic

            });


            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}