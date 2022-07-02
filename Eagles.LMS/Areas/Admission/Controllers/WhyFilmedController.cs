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
    public class WhyFilmedController : Controller
    {
        // GET: Admission/WhyFilmed
        public ActionResult Index()
        {
            return View(new UnitOfWork().WhyFilmedManager.GetAll().OrderByDescending(s => s.Id).ToList());
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        public ActionResult Edit(int id)
        {
            var _whyfilmed = new UnitOfWork().WhyFilmedManager.GetBy(id);
            if (_whyfilmed == null)
                return HttpNotFound();
            return View(_whyfilmed);
        }
        [HttpPost]
        public ActionResult Edit(WhyFilmed whyfilmed, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(whyfilmed);
            var ctx = new UnitOfWork();
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
                    //var oldpath = Path.Combine(Server.MapPath(team.Image.ToString()));
                    //System.IO.File.Delete(oldpath);
                    whyfilmed.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                whyfilmed.UserEditId = userId;
                whyfilmed.EditeTime = DateTime.Now;

                ctx.WhyFilmedManager.UpdateWithSave(whyfilmed);

                var user = ctx.UserManager.GetById(GetUserId());

                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = whyfilmed.Id,
                    TableName = "WhyFilmed",
                    Action = "Edit:WhyFilmed",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = whyfilmed.TitleArabic


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
        public ActionResult Create(WhyFilmed whyfilmed, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(whyfilmed);

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
                    whyfilmed.Image = $"/attachments/{fileName}";
                    int userId = GetUserId();
                    whyfilmed.UserCreateId = userId;
                    whyfilmed.CreateTime = DateTime.Now;
                    whyfilmed.UserEditId = userId;
                    whyfilmed.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();
                    whyfilmed = ctx.WhyFilmedManager.Add(whyfilmed);

                    var user = ctx.UserManager.GetById(GetUserId());

                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = whyfilmed.Id,
                        TableName = "WhyFilmed",
                        Action = "Create:WhyFilmed",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = whyfilmed.TitleArabic


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
            var entity = ctx.WhyFilmedManager.GetBy(id);

            var user = ctx.UserManager.GetById(GetUserId());



            ctx.WhyFilmedManager.Delete(entity);
            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "WhyFilmed",
                Action = "Delete:WhyFilmed",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic


            });
            return Json(JsonRequestBehavior.AllowGet);
        }

    }
}