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
    public class FacilitiesController : Controller
    {
        // GET: Admission/Facilities
        public ActionResult Index()
        {
            return View(new UnitOfWork().FacilitiesManager.GetAll().OrderByDescending(s => s.Id).ToList());
        }
        public ActionResult Edit(int id)
        {
            var _facilities = new UnitOfWork().FacilitiesManager.GetBy(id);
            if (_facilities == null)
                return HttpNotFound();
            return View(_facilities);
        }
        [HttpPost]
        public ActionResult Edit(Facilities facilities, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(facilities);
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
                    facilities.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                facilities.UserEditId = userId;
                facilities.EditeTime = DateTime.Now;

                ctx.FacilitiesManager.UpdateWithSave(facilities);

                var user = ctx.UserManager.GetById(GetUserId());

                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = facilities.Id,
                    TableName = "Facilities",
                    Action = "Edit:Facilities",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = facilities.TitleArabic
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
        public ActionResult Create(Facilities facilities, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(facilities);

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
                    facilities.Image = $"/attachments/{fileName}";
                    int userId = GetUserId();
                    facilities.UserCreateId = userId;
                    facilities.CreateTime = DateTime.Now;
                    facilities.UserEditId = userId;
                    facilities.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();
                    facilities = ctx.FacilitiesManager.Add(facilities);

                    var user = ctx.UserManager.GetById(GetUserId());

                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = facilities.Id,
                        TableName = "Facilities",
                        Action = "Create:Facilities",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = facilities.TitleArabic


                    });

                    requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                    result = RedirectToAction(nameof(Create));



                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }
        [HttpPost]

        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        public ActionResult Delete(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.FacilitiesManager.GetBy(id);

            var user = ctx.UserManager.GetById(GetUserId());



            ctx.FacilitiesManager.Delete(entity);
            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Customer",
                Action = "Delete:Facilities",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic


            });
            return Json(JsonRequestBehavior.AllowGet);
        }


    }
}