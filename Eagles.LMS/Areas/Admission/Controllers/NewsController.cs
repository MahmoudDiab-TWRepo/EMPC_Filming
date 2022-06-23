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
    public class NewsController : Controller
    {
        // GET: Admission/News
        public ActionResult Index()
        {

            
            return View( new UnitOfWork().NewManager.GetAll().Where(s => s.Status == EntityStatus.Approval).OrderByDescending(s => s.Id).ToList());
        }

        public ActionResult Pending()
        {
            return View(new UnitOfWork().NewManager.GetAll().Where(s => s.Status == EntityStatus.Pending).ToList());
        }
        public ActionResult Edit(int id)
        {

            var ctx = new UnitOfWork();
            var _news = ctx.NewManager.GetBy(id);
            if (_news == null)
                return HttpNotFound();
            _news.NewImages = ctx.NewImagesMnager.GetAll().Where(s => s.NewId == id).ToList();
            var _maxOrder = new UnitOfWork().NewImagesMnager.MaxOrder();
            ViewBag.MaxOrder = _maxOrder;

            return View(_news);
        }
        [HttpPost]
        public ActionResult Edit(New _new, HttpPostedFileBase uploadattachments, List<HttpPostedFileBase> uploadattachments_multi = null)
        {
            var _maxOrder = new UnitOfWork().NewImagesMnager.MaxOrder();
            ViewBag.MaxOrder = _maxOrder;

            //byte[] bytes;
            //using (BinaryReader br = new BinaryReader(uploadattachments.InputStream))
            //{
            //    bytes = br.ReadBytes(uploadattachments.ContentLength);
            //}
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
                        _new.MainImage = $"/attachments/{fileName}";

                    }

                    int userId = GetUserId();
                    _new.UserEditId = userId;
                    _new.EditeTime = DateTime.Now;

                    _ctx.NewManager.UpdateWithSave(_new);
                    var user = _ctx.UserManager.GetById(userId);


                    _ctx.logManager.Add(new log
                    {
                        UserId = GetUserId(),
                        ActionTime = DateTime.Now,
                        EntityId = _new.Id,
                        TableName = "News",
                        Action = "Update:News",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = _new.TitleArabic
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
                            _ctx.NewImagesMnager.Add(new NewImages
                            {

                                NewId = _new.Id,
                                Path = $"/attachments/{fileName}",

                            });

                        }
                    }


                    requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




                }
            }

            TempData["RequestStatus"] = requestStatus;
            _new.NewImages = _ctx.NewImagesMnager.GetAll().Where(s => s.NewId == _new.Id).ToList();
            return View(_new);

        }


        public ActionResult Create()
        {

            var _maxOrder = new UnitOfWork().NewImagesMnager.MaxOrder();
            ViewBag.MaxOrder = _maxOrder;
            return View(new New { Order = 1 + _maxOrder });
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        [HttpPost]
        public ActionResult Create(New _new, HttpPostedFileBase uploadattachments, List<HttpPostedFileBase> uploadattachments_multi = null)
        {
            var _maxOrder = new UnitOfWork().NewImagesMnager.MaxOrder();
            ViewBag.MaxOrder = _maxOrder;

          
            _new.Status = EntityStatus.Approval;
            ActionResult result = View(_new);

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

                        //string _rendom = new Random().Next(1, 99999999).ToString();
                        string _rendom = System.Guid.NewGuid().ToString();
                        string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                        var fileName = _rendom + extention;


                        var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        uploadattachments.SaveAs(path);

                        _new.MainImage = $"/attachments/{fileName}";
                        var _ctx = new UnitOfWork();

                       

                        _new.CreatedTime = DateTime.Now;
                        int userId = GetUserId();
                        _new.UserCreateId = userId;
                        _new.Status = EntityStatus.Pending;
                        _new.UserEditId = userId;
                        _new.EditeTime = DateTime.Now;
                        _new = _ctx.NewManager.Add(_new);
               var user = _ctx.UserManager.GetById(userId);
                      
                        _ctx.logManager.Add(new log
                        {
                            UserId = GetUserId(),
                            ActionTime = DateTime.Now,
                            EntityId = _new.Id,
                            TableName = "News",
                            Action = "Create:News",
                            LoginDate=user.LoginDate,
                            LogoutDate=user.LogoutDate,
                            ActionTitle=_new.TitleArabic

                           
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
                                fileName = _rendom + extention;

                                path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                                item.SaveAs(path);
                                _ctx.NewImagesMnager.Add(new NewImages
                                {

                                    NewId = _new.Id,
                                    Path = $"/attachments/{fileName}",

                                });

                            }
                        }

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
            var entity = ctx.NewManager.GetBy(id);

            ctx.NewManager.Delete(entity);
            var user = ctx.UserManager.GetById(GetUserId());

            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "News",
                Action = "Delete:News",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic


            });
         
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Pending(int id, EntityStatus status)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.NewManager.GetBy(id);
            entity.Status = status;
            ctx.NewManager.UpdateWithoutSave(entity);
            var user = ctx.UserManager.GetById(GetUserId());

            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "News",
                Action = "" + status.ToString() + ":News",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic


            });
          
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}