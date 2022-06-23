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

    public class CitizensController : Controller
    {

        public ActionResult Index()
        {
            return View(new UnitOfWork().CitizensManager.GetAll().OrderByDescending(s => s.Id).ToList());
        }
        public ActionResult Edit(int id)
        {
            var citizen = new UnitOfWork().CitizensManager.GetBy(id);
            if (citizen == null)
                return HttpNotFound();
            return View(citizen);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Citizens citizens, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(citizens);


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
                    citizens.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                citizens.UserEditId = userId;
                citizens.EditeTime = DateTime.Now;


                var _ctx = new UnitOfWork();
                _ctx.CitizensManager.UpdateWithSave(citizens);

                var user = _ctx.UserManager.GetById(GetUserId());

                _ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = citizens.Id,
                    TableName = "Citizen",
                    Action = "Update:Citizen",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = citizens.TitleArabic


                });
             

                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }

        // GET: Admission/Citizens
        [HttpPost]
        public ActionResult Create(Citizens citizens, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(citizens);

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
                    citizens.Image = $"/attachments/{fileName}";
                    int userId = GetUserId();
                    citizens.UserCreateId = userId;
                    citizens.CreateTime = DateTime.Now;
                    citizens.UserEditId = userId;
                    citizens.EditeTime = DateTime.Now;
                    var _ctx = new UnitOfWork();
                    citizens = _ctx.CitizensManager.Add(citizens);
                    var user = _ctx.UserManager.GetById(GetUserId());

                    _ctx.logManager.Add(new log
                    {

                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = citizens.Id,
                        TableName = "Citizen",
                        Action = "Create:Citizen",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = citizens.TitleArabic


                    });

                   

                    requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                    result = RedirectToAction(nameof(Create));



                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.CitizensManager.GetBy(id);

            var user = ctx.UserManager.GetById(GetUserId());

            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Citizen",
                Action = "Delete:Citizen",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic


            });

          


            ctx.CitizensManager.Delete(entity);
            return Json(JsonRequestBehavior.AllowGet);
        }


    }
}