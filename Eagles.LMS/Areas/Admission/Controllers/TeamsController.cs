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
    public class TeamsController : Controller
    {
        public ActionResult Index()
        {
            return View(new UnitOfWork().TeamManager.GetAll().OrderByDescending(s => s.Id).ToList()); 
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        public ActionResult Edit(int id)
        {
            var _team = new UnitOfWork().TeamManager.GetBy(id);
            if (_team == null)
                return HttpNotFound();
            return View(_team);
        }
        [HttpPost]
        public ActionResult Edit(Team team, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(team);
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
                    team.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                team.UserEditId = userId;
                team.EditeTime = DateTime.Now;

                ctx.TeamManager.UpdateWithSave(team);
              
                var user = ctx.UserManager.GetById(GetUserId());

                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = team.Id,
                    TableName = "Team",
                    Action = "Edit:Team",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = team.NameArabic


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
        public ActionResult Create(Team team, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(team);

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
                    team.Image = $"/attachments/{fileName}";
                    int userId = GetUserId();
                    team.UserCreateId = userId;
                    team.CreateTime = DateTime.Now;
                    team.UserEditId = userId;
                    team.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();
                    team = ctx.TeamManager.Add(team);

                    var user = ctx.UserManager.GetById(GetUserId());

                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = team.Id,
                        TableName = "Team",
                        Action = "Create:Team",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = team.NameArabic


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
            var entity = ctx.TeamManager.GetBy(id);

            var user = ctx.UserManager.GetById(GetUserId());

            
        
            ctx.TeamManager.Delete(entity);
            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Team",
                Action = "Delete:Team",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.NameArabic


            });
            return Json(JsonRequestBehavior.AllowGet);
        }


    }
}