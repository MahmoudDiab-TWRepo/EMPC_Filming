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
    public class LocationsController : Controller
    {
        // GET: Admission/Locations
        public ActionResult Index()
        {
            return View(new UnitOfWork().LocationManager.GetAll().Where(s => s.Status == EntityStatus.Approval).OrderByDescending(s=>s.Id).ToList());
        }
        public ActionResult Pending()
        {
            return View(new UnitOfWork().LocationManager.GetAll().Where(s => s.Status == EntityStatus.Pending).ToList());
        }

        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }

        [HttpPost]
        public ActionResult Edit(Location location, HttpPostedFileBase iconImage, HttpPostedFileBase mainImage, List<HttpPostedFileBase> uploadattachments_multi = null)
        {

            var _ctx = new UnitOfWork();



            RequestStatus requestStatus;
            if (iconImage != null && !
                iconImage.ContentType.CheckImageExtention())
            {

                requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");
            }
            else
            {
                if (mainImage == null || mainImage.ContentLength == 0 || !
                 mainImage.ContentType.CheckImageExtention())
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only In Main Image");
                }

                if (uploadattachments_multi != null && uploadattachments_multi[0] != null && uploadattachments_multi.Any() &&
            uploadattachments_multi.Any(s => !s.ContentType.CheckImageExtention()))
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");

                }
                else
                {
                    string _rendom, fileName, path;
                    if (mainImage != null)
                    {

                        _rendom = new Random().Next(1, 99999999).ToString();

                        //fileName = _rendom + Path.GetFileName(mainImage.FileName);
                        string extention = System.IO.Path.GetExtension(mainImage.FileName);
                         fileName = _rendom + extention;


                        path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        mainImage.SaveAs(path);
                        location.MainImage = $"/attachments/{fileName}";

                    }

                    if (iconImage != null)
                    {

                        _rendom = new Random().Next(1, 99999999).ToString();

                        fileName = _rendom + Path.GetFileName(iconImage.FileName);

                        path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        iconImage.SaveAs(path);
                        location.IconImage = $"/attachments/{fileName}";

                    }

                    int userId = GetUserId();
                    location.UserEditId = userId;
                    location.EditeTime = DateTime.Now;

                    _ctx.LocationManager.UpdateWithSave(location);
                    var user = _ctx.UserManager.GetById(GetUserId());



                    _ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = location.Id,
                        TableName = "Location",
                        Action = "Update:Location",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = location.TitleArabic

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
                            _ctx.LocationImagesManager.Add(new LocationImages
                            {

                                LocationId = location.Id,
                                Path = $"/attachments/{fileName}",

                            });

                        }
                    }


                    requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




                }
            }

            TempData["RequestStatus"] = requestStatus;
            location.LocationImages = _ctx.LocationImagesManager.GetAll().Where(s => s.LocationId == location.Id).ToList();
            return View(location);

        }

        public ActionResult Edit(int id)
        {
            var ctx = new UnitOfWork();
            var _location = ctx.LocationManager.GetBy(id);
            if (_location == null)
                return HttpNotFound();
            _location.LocationImages = ctx.LocationImagesManager.GetAll().Where(s => s.LocationId == id).ToList();
            return View(_location);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Location location, HttpPostedFileBase iconImage, HttpPostedFileBase mainImage, List<HttpPostedFileBase> uploadattachments_multi = null)
        {

            location.Status = EntityStatus.Pending;
            ActionResult result = View(location);

            if (ModelState.IsValid)
            {

                RequestStatus requestStatus;
                if (iconImage == null || iconImage.ContentLength == 0 || !
                    iconImage.ContentType.CheckImageExtention())
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only In Location Icon ");
                }

                else
                {


                    if (mainImage == null || mainImage.ContentLength == 0 || !
                   mainImage.ContentType.CheckImageExtention())
                    {
                        requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only In Main Image");
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

                            // icon
                            string _rendom = new Random().Next(1, 99999999).ToString();

                            var fileName = _rendom + Path.GetFileName(iconImage.FileName);

                            var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                            iconImage.SaveAs(path);
                            location.IconImage = $"/attachments/{fileName}";

                            // main

                            _rendom = new Random().Next(1, 99999999).ToString();
                            //fileName = _rendom + Path.GetFileName(mainImage.FileName);
                            string extention = System.IO.Path.GetExtension(mainImage.FileName);
                            fileName = _rendom + extention;


                            path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                            mainImage.SaveAs(path);

                            location.MainImage = $"/attachments/{fileName}";




                            var _ctx = new UnitOfWork();
                            int userId = GetUserId();
                            location.UserCreateId = userId;
                            location.CreateTime = DateTime.Now;
                            location.UserEditId = userId;
                            location.EditeTime = DateTime.Now;
                            location = _ctx.LocationManager.Add(location);
                            var user = _ctx.UserManager.GetById(GetUserId());



                            _ctx.logManager.Add(new log
                            {
                                UserId = userId,
                                ActionTime = DateTime.Now,
                                EntityId = location.Id,
                                TableName = "Location",
                                Action = "Create:Location",
                                 LoginDate = user.LoginDate,
                                LogoutDate = user.LogoutDate,
                                ActionTitle = location.TitleArabic

                            });

                       


                            requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                            result = RedirectToAction(nameof(Create));


                            if (uploadattachments_multi != null && uploadattachments_multi[0] != null)
                            {
                                foreach (var item in uploadattachments_multi)
                                {
                                    _rendom = new Random().Next(1, 99999999).ToString();

                                    //fileName = _rendom + Path.GetFileName(item.FileName);
                                     extention = System.IO.Path.GetExtension(item.FileName);
                                    //fileName = Guid.NewGuid().ToString()+"" + extention;
                                    fileName = _rendom + extention;
                                    path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                                    item.SaveAs(path);
                                    _ctx.LocationImagesManager.Add(new LocationImages
                                    {

                                        LocationId = location.Id,
                                        Path = $"/attachments/{fileName}",

                                    });

                                }
                            }
                        }

                    }

                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }

        [HttpPost]
        public ActionResult Pending(int id, EntityStatus status)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.LocationManager.GetBy(id);

            entity.Status = status;
            ctx.LocationManager.UpdateWithoutSave(entity);
            var user = ctx.UserManager.GetById(GetUserId());




            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Location",
                Action = "" + status.ToString() + ":Location",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic

            });

            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.LocationManager.GetBy(id);
            ctx.LocationManager.Delete(entity);
            var user = ctx.UserManager.GetById(GetUserId());



            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Location",
                Action = "Delete:Location",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic

            });

        

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}