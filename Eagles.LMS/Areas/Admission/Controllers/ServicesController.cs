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
    public class ServicesController : Controller
    {
        // GET: Admission/Services
        public ActionResult Index()
        {
            return View(new UnitOfWork().ServiceManager.GetAll().Where(s => s.Status == EntityStatus.Approval).OrderByDescending(s => s.Id).ToList());
        }

        public ActionResult Pending()
        {
            return View(new UnitOfWork().ServiceManager.GetAll().Where(s => s.Status == EntityStatus.Pending).ToList());
        }
        [HttpPost]
        public ActionResult Pending(int id, EntityStatus status)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.ServiceManager.GetBy(id);

            entity.Status = status;
            ctx.ServiceManager.UpdateWithoutSave(entity);
            var user = ctx.UserManager.GetById(GetUserId());



            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Service",
                Action = "" + status.ToString() + ":Service",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic

            });

        
           
            return Json(JsonRequestBehavior.AllowGet);
        }

        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        public ActionResult Edit(int id)
        {
            var ctx = new UnitOfWork();
            var _services = ctx.ServiceManager.GetBy(id);
            if (_services == null)
                return HttpNotFound();
            _services.ServiceImages = ctx.ServiceImagesManager.GetAll().Where(s => s.ServiceId == id).ToList();
            return View(_services);
        }

        [HttpPost]
        public ActionResult Edit(Service service, HttpPostedFileBase uploadattachments, List<HttpPostedFileBase> uploadattachments_multi = null)
        {

            var _ctx = new UnitOfWork();



            RequestStatus requestStatus;
            if (uploadattachments != null && !
                uploadattachments.ContentType.CheckImageExtention())
            {

                requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");
            }
            else
            {
                if (uploadattachments_multi != null && uploadattachments_multi[0] != null && uploadattachments_multi.Any() &&
            uploadattachments_multi.Any(s => !s.ContentType.CheckImageExtention()))
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");

                }
                else
                {
                    string _rendom, fileName, path;
                    if (uploadattachments != null)
                    {

                        _rendom = new Random().Next(1, 99999999).ToString();

                        //fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                        string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                        fileName = _rendom + extention;

                        path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        uploadattachments.SaveAs(path);
                        service.MainImage = $"/attachments/{fileName}";

                    }

                    int userId = GetUserId();
                    service.UserEditId = userId;
                    service.EditeTime = DateTime.Now;

                    _ctx.ServiceManager.UpdateWithSave(service);
                    var user = _ctx.UserManager.GetById(GetUserId());



                    _ctx.logManager.Add(new log
                    {
                        UserId = GetUserId(),
                        ActionTime = DateTime.Now,
                        EntityId = service.Id,
                        TableName = "Service",
                        Action = "Update:Service",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = service.TitleArabic

                    });

                   

                    if (uploadattachments_multi != null && uploadattachments_multi[0] != null)
                    {
                        foreach (var item in uploadattachments_multi)
                        {
                            _rendom = new Random().Next(1, 99999999).ToString();

                            //fileName = _rendom + Path.GetFileName(item.FileName);
                            string extention = System.IO.Path.GetExtension(item.FileName);
                            fileName = _rendom + extention;

                            path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                            item.SaveAs(path);
                            _ctx.ServiceImagesManager.Add(new ServiceImages
                            {

                                ServiceId = service.Id,
                                Path = $"/attachments/{fileName}",

                            });

                        }
                    }


                    requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




                }
            }

            TempData["RequestStatus"] = requestStatus;
            service.ServiceImages = _ctx.ServiceImagesManager.GetAll().Where(s => s.ServiceId == service.Id).ToList();
            return View(service);

        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Service service, HttpPostedFileBase uploadattachments, List<HttpPostedFileBase> uploadattachments_multi = null)
        {
            service.Status = EntityStatus.Pending;

            ActionResult result = View(service);

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


                    if (uploadattachments_multi != null && uploadattachments_multi[0] != null && uploadattachments_multi.Any() &&
                 uploadattachments_multi.Any(s => !s.ContentType.CheckImageExtention()))
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
                        service.MainImage = $"/attachments/{fileName}";

                        var _ctx = new UnitOfWork();

                        int userId = GetUserId();
                        service.UserCreateId = userId;
                        service.CreateTime = DateTime.Now;
                        service.UserEditId = userId;
                        service.EditeTime = DateTime.Now;
                        service = _ctx.ServiceManager.Add(service);
                        var user = _ctx.UserManager.GetById(GetUserId());



                       
                       

                        _ctx.logManager.Add(new log
                        {
                            UserId = userId,
                            ActionTime = DateTime.Now,
                            EntityId = service.Id,
                            TableName = "Service",
                            Action = "Create:Service",
                            LoginDate = user.LoginDate,
                            LogoutDate = user.LogoutDate,
                            ActionTitle = service.TitleArabic

                        });
                      

                        if (uploadattachments_multi != null && uploadattachments_multi[0] != null)
                        {
                            foreach (var item in uploadattachments_multi)
                            {
                                _rendom = new Random().Next(1, 99999999).ToString();

                                //fileName = _rendom + Path.GetFileName(item.FileName);
                                 extention = System.IO.Path.GetExtension(item.FileName);
                                 fileName = _rendom + extention;

                                path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                                item.SaveAs(path);
                                _ctx.ServiceImagesManager.Add(new ServiceImages
                                {

                                    ServiceId = service.Id,
                                    Path = $"/attachments/{fileName}",

                                });

                            }
                        }

                        requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                        result = RedirectToAction(nameof(Create));

                    }

                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.ServiceManager.GetBy(id);
            var user = ctx.UserManager.GetById(GetUserId());

            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Service",
                Action = "Delete:Service",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic
            });

           
            ctx.ServiceManager.Delete(entity);
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}