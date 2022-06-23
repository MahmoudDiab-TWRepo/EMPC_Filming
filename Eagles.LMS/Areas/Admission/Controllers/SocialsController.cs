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
    public class SocialsController : Controller
    {
        // GET: Admission/Socials
        public ActionResult Index()
        {
            return View(new UnitOfWork().SocialMediaManager.GetAll().OrderByDescending(s => s.Id).ToList());
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        //11k
        public ActionResult Edit(int id)
        {
            var ctx = new UnitOfWork();
            var entity = ctx.SocialMediaManager.GetBy(id);
            if (entity == null)
                return HttpNotFound();
            return View(entity);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(SocialMedia socialMedia, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(socialMedia);


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

                    var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);

                    var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                    uploadattachments.SaveAs(path);
                    socialMedia.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                socialMedia.UserEditId = userId;
                socialMedia.EditeTime = DateTime.Now;


               var ctx = new UnitOfWork();
                ctx.SocialMediaManager.UpdateWithSave(socialMedia);
                var user = ctx.UserManager.GetById(GetUserId());




                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = socialMedia.Id,
                    TableName = "Social-Media",
                    Action = "Update:Social-Media",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = socialMedia.NameArabic

                });

            
                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }
     
        [HttpPost]
        public ActionResult Create(SocialMedia social, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(social);

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

                    var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);

                    var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                    uploadattachments.SaveAs(path);
                    social.Image = $"/attachments/{fileName}";
                    int userId = GetUserId();
                    social.UserCreateId = userId;
                    social.CreateTime = DateTime.Now;
                    social.UserEditId= userId;
                    social.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();
                    social= ctx.SocialMediaManager.Add(social);
                    var user = ctx.UserManager.GetById(GetUserId());



                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = social.Id,
                        TableName = "Social-Media",
                        Action = "Create:Social-Media",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = social.NameArabic

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
            var entity = ctx.SocialMediaManager.GetBy(id);
            ctx.SocialMediaManager.Delete(entity);
            var user = ctx.UserManager.GetById(GetUserId());



            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Social-Media",
                Action = "Delete:Social-Media",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.NameArabic

            });
          
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}