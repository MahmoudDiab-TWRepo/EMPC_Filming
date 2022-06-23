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
    public class ClientsController : Controller
    {
        public ActionResult Index()
        {
            var data = (new UnitOfWork().ClientsManager.GetAll().OrderByDescending(s => s.Id).ToList());
            return View(data);
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        public ActionResult Edit(int id)
        {
            var client = new UnitOfWork().ClientsManager.GetBy(id);
            if (client == null)
                return HttpNotFound();
            return View(client);
        }
        [HttpPost]
        public ActionResult Edit(Client client, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(client);


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
                    client.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                client.UserEditId = userId;
                client.EditeTime = DateTime.Now;


                var ctx = new UnitOfWork();
                ctx.ClientsManager.UpdateWithSave(client);
                var user = ctx.UserManager.GetById(GetUserId());

              

                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = client.Id,
                    TableName = "Client",
                    Action = "Edit:Client",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = client.NameArabic


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
        public ActionResult Create(Client client, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(client);

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
                    client.Image = $"/attachments/{fileName}";
                    int userId = GetUserId();
                    client.UserCreateId = userId;
                    client.CreateTime = DateTime.Now;
                    client.UserEditId = userId;
                    client.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();

                    var user = ctx.UserManager.GetById(GetUserId());

                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = client.Id,
                        TableName = "Client",
                        Action = "Create:Client",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = client.NameArabic


                    });
                    client = ctx.ClientsManager.Add(client);
                   
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
            var entity = ctx.ClientsManager.GetBy(id);
            var user = ctx.UserManager.GetById(GetUserId());



            ctx.logManager.Add(new log
            {
                UserId = id,
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Client",
                Action = "Delete:Client",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.NameArabic


            });

         
            ctx.ClientsManager.Delete(entity);
            return Json(JsonRequestBehavior.AllowGet);
        }


    }
}