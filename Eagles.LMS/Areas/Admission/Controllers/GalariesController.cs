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
    public class GalariesController : Controller
    {
        // GET: Admission/Galaries
        public ActionResult Index()
        {
            return View(new UnitOfWork().GalaryManager.GetAllIncluded().Where(s => s.IsImage).OrderByDescending(s => s.Id).ToList());
        }
        public ActionResult Videos()
        {
            return View(new UnitOfWork().GalaryManager.GetAllIncluded().Where(s => !s.IsImage).OrderByDescending(s => s.Id).ToList());
        }
        public ActionResult Edit(int id)
        {
            var entity = new UnitOfWork().GalaryManager.GetBy(id);
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }
        [HttpPost]
        public ActionResult Edit(Galary galary, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(galary);


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
                    galary.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                galary.UserEditId = userId;
                galary.EditeTime = DateTime.Now;


                var ctx = new UnitOfWork();
                ctx.GalaryManager.UpdateWithSave(galary);
                var user = ctx.UserManager.GetById(GetUserId());




                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = galary.Id,
                    TableName = "Galary",
                    Action = "Update:Galary",
                      LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = galary.TitleArabic

                });
              

                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }


        [HttpPost]
        public ActionResult EditVideo(Galary galary, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(galary);


            RequestStatus requestStatus;

            //if (uploadattachments != null && !
            //    uploadattachments.ContentType.CheckVideoExtention())
            if (uploadattachments != null && (!
                uploadattachments.ContentType.CheckImageExtention()))
            {

                requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Video Only ssww");
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
                    galary.Image = $"/attachments/{fileName}";

                }
                galary.VideoIframe = galary.VideoIframe;
                int userId = GetUserId();
                galary.UserEditId = userId;
                galary.EditeTime = DateTime.Now;


                var ctx = new UnitOfWork();
                ctx.GalaryManager.UpdateWithSave(galary);
                var user = ctx.UserManager.GetById(GetUserId());




                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = galary.Id,
                    TableName = "Video",
                    Action = "Update:Video",
                       LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = galary.TitleArabic

                });


            

                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }

        [HttpPost]
        public ActionResult EditAlbum(Album album, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(album);


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
                    album.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                album.UserEditId = userId;
                album.EditeTime = DateTime.Now;


                var ctx = new UnitOfWork();
                ctx.AlbumManager.UpdateWithSave(album);
                var user = ctx.UserManager.GetById(GetUserId());




                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = album.Id,
                    TableName = "Album",
                    Action = "Update:Album",
                     LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = album.TitleArabic

                });

             

                requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




            }
            TempData["RequestStatus"] = requestStatus;




            return result;

        }
        public ActionResult EditVideo(int id)
        {
            var entity = new UnitOfWork().GalaryManager.GetBy(id);
            if (entity == null)
                return HttpNotFound();
            return View(entity);
        }
        public ActionResult EditAlbum(int id)
        {
            var entity = new UnitOfWork().AlbumManager.GetBy(id);
            if (entity == null)
                return HttpNotFound();
            entity.Galaries = new UnitOfWork().GalaryManager.GetAll().Where(s => s.AlbumId == id).ToList();
            return View(entity);
        }

        public ActionResult Albums()
        {
            return View(new UnitOfWork().AlbumManager.GetAll().OrderByDescending(s => s.Id).ToList());

        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }


        public ActionResult CreateAlbum()
        {
            return View();

        }

        [HttpPost]
        public ActionResult CreateAlbum(Album album, HttpPostedFileBase uploadattachments, List<HttpPostedFileBase> uploadattachments_multi = null)
        {

            ActionResult result = View(album);



            RequestStatus requestStatus;
            if (uploadattachments == null || uploadattachments.ContentLength == 0 || !
               uploadattachments.ContentType.CheckImageExtention())
            {
                requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only In Album");
            }

            else
            {
                if (uploadattachments_multi != null && uploadattachments_multi[0] != null && uploadattachments_multi.Any() &&
           uploadattachments_multi.Any(s => !s.ContentType.CheckImageExtention()))
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only In Galary Images");

                }
                else
                {

                    string _rendom = new Random().Next(1, 99999999).ToString();

                    //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                    string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                    var fileName = _rendom + extention;

                    var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                    uploadattachments.SaveAs(path);
                    album.Image = $"/attachments/{fileName}";
                    int userId = GetUserId();
                    album.UserCreateId = userId;
                    album.CreateTime = DateTime.Now;
                    album.UserEditId = userId;
                    album.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();
                    album = ctx.AlbumManager.Add(album);
                    var user = ctx.UserManager.GetById(GetUserId());




                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = album.Id,
                        TableName = "Album",
                        Action = "Create:Album",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = album.TitleArabic

                    });


                    requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                    result = RedirectToAction(nameof(CreateAlbum));
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
                            var _maxOrder = ctx.GalaryManager.GetAll().Max(s => s.Order) + 1;
                            ctx.GalaryManager.Add(new Galary
                            {
                                Image = $"/attachments/{fileName}",
                                UserCreateId = userId,
                                TitleArabic = "",
                                IsImage = true,
                                TitleEnglish = "",
                                CreateTime = DateTime.Now,
                                UserEditId = userId,
                                EditeTime = DateTime.Now,
                                Order = _maxOrder,
                                AlbumId = album.Id

                            });

                        }
                    }
                }
            }
            TempData["RequestStatus"] = requestStatus;

            return result;

        }

        public ActionResult CreateVideo()
        {
            return View();
        }



        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Galary galary, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(galary);

            if (ModelState.IsValid)
            {

                RequestStatus requestStatus;
                if (uploadattachments == null || uploadattachments.ContentLength == 0 || !
               uploadattachments.ContentType.CheckImageExtention())
                {


                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image  Only");
                }
                else
                {





                    string _rendom = new Random().Next(1, 99999999).ToString();

                    //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                    string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                    var fileName = _rendom + extention;

                    var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                    uploadattachments.SaveAs(path);
                    galary.Image = $"/attachments/{fileName}";
                    galary.IsImage = true;

                    int userId = GetUserId();
                    galary.UserCreateId = userId;
                    galary.CreateTime = DateTime.Now;
                    galary.UserEditId = userId;
                    galary.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();
                    galary = ctx.GalaryManager.Add(galary);
                    var user = ctx.UserManager.GetById(GetUserId());




                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = galary.Id,
                        TableName = "Galary",
                        Action = "Create:Galary",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = galary.TitleArabic

                    });


                 
                    requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                    result = RedirectToAction(nameof(Create));



                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }

        [HttpPost]
        public ActionResult CreateVideo(Galary galary, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(galary);

            if (ModelState.IsValid)
            {

                RequestStatus requestStatus;

                // if ((string.IsNullOrEmpty(galary.VideoIframe)) && (uploadattachments == null || uploadattachments.ContentLength == 0 || !
                //uploadattachments.ContentType.CheckVideoExtention()))

                if (uploadattachments == null || uploadattachments.ContentLength == 0 || !
           uploadattachments.ContentType.CheckImageExtention())
                {


                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Video Only");
                }
                else
                {




                    string _rendom = new Random().Next(1, 99999999).ToString();

                    //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                    string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                    var fileName = _rendom + extention;

                    var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                    uploadattachments.SaveAs(path);
                    galary.Image = $"/attachments/{fileName}";
                    galary.IsImage = false;


                    //slider.Iframe = slider.Iframe.Split('&')[0].Split('=')[1].ToString();
                    galary.VideoIframe = galary.VideoIframe;

                    int userId = GetUserId();
                    galary.UserCreateId = userId;
                    galary.CreateTime = DateTime.Now;
                    galary.UserEditId = userId;
                    galary.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();
                    galary = ctx.GalaryManager.Add(galary);
                    var user = ctx.UserManager.GetById(GetUserId());




                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = galary.Id,
                        TableName = "Video",
                        Action = "Create:Video",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = galary.TitleArabic

                    });
                 

                    requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                    result = RedirectToAction(nameof(CreateVideo));



                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.GalaryManager.GetBy(id);
            ctx.GalaryManager.Delete(entity);
            var user = ctx.UserManager.GetById(GetUserId());




            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Galary",
                Action = "Delete:Galary",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic

            });
         
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveAlbum(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.AlbumManager.GetBy(id);
            var allGalary = ctx.GalaryManager.GetAllBind().Where(s => s.AlbumId == id).ToList();
            bool DeleteAlbum = false;
            if (allGalary.Count == 0)
            {
                ctx.GalaryManager.Delete(allGalary);
                ctx.AlbumManager.Delete(entity);
                var user = ctx.UserManager.GetById(GetUserId());




                ctx.logManager.Add(new log
                {
                    UserId = GetUserId(),
                    ActionTime = DateTime.Now,
                    EntityId = id,
                    TableName = "Album",
                    Action = "Delete:Album",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = entity.TitleArabic

                });
        
                DeleteAlbum = true;
                
            }
            return Json(DeleteAlbum, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveVideo(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.GalaryManager.GetBy(id);
            ctx.GalaryManager.Delete(entity);
            var user = ctx.UserManager.GetById(GetUserId());




            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Video",
                Action = "Delete:Video",
                        LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic

            });
        

            return Json(JsonRequestBehavior.AllowGet);
        }


    }
}