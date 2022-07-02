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
    public class FilmedController : Controller
    {
        // GET: Admission/Filmed
        public ActionResult Index()
        {


            return View(new UnitOfWork().FilmedManager.GetAll().Where(s => s.Status == EntityStatus.Approval).OrderByDescending(s => s.Id).ToList());
        }

        public ActionResult Pending()
        {
            return View(new UnitOfWork().FilmedManager.GetAll().Where(s => s.Status == EntityStatus.Pending).ToList());
        }
        public ActionResult Edit(int id)
        {

            var ctx = new UnitOfWork();
            var _filmed = ctx.FilmedManager.GetBy(id);
            if (_filmed == null)
                return HttpNotFound();
            _filmed.FilmedImages = ctx.FilmedImagesManager.GetAll().Where(s => s.FilmedId == id).ToList();
            var _maxOrder = new UnitOfWork().FilmedImagesManager.MaxOrder();
            ViewBag.MaxOrder = _maxOrder;

            return View(_filmed);
        }
        [HttpPost]
        public ActionResult Edit(Filmed _filmed, HttpPostedFileBase uploadattachments, List<HttpPostedFileBase> uploadattachments_multi = null)
        {
            var _maxOrder = new UnitOfWork().FilmedImagesManager.MaxOrder();
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
                        _filmed.MainImage = $"/attachments/{fileName}";

                    }

                    int userId = GetUserId();
                    _filmed.UserEditId = userId;
                    _filmed.EditeTime = DateTime.Now;

                    _ctx.FilmedManager.UpdateWithSave(_filmed);
                    var user = _ctx.UserManager.GetById(userId);


                    _ctx.logManager.Add(new log
                    {
                        UserId = GetUserId(),
                        ActionTime = DateTime.Now,
                        EntityId = _filmed.Id,
                        TableName = "Filmed",
                        Action = "Update:Filmed",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = _filmed.TitleArabic
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
                            _ctx.FilmedImagesManager.Add(new FilmedImages
                            {

                                FilmedId = _filmed.Id,
                                Path = $"/attachments/{fileName}",

                            });

                        }
                    }


                    requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




                }
            }

            TempData["RequestStatus"] = requestStatus;
            _filmed.FilmedImages = _ctx.FilmedImagesManager.GetAll().Where(s => s.FilmedId == _filmed.Id).ToList();
            return View(_filmed);

        }


        public ActionResult Create()
        {

            var _maxOrder = new UnitOfWork().FilmedImagesManager.MaxOrder();
            ViewBag.MaxOrder = _maxOrder;
            return View(new Filmed { Order = 1 + _maxOrder });
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        [HttpPost]
        public ActionResult Create(Filmed _filmed, HttpPostedFileBase uploadattachments, List<HttpPostedFileBase> uploadattachments_multi = null)
        {
            var _maxOrder = new UnitOfWork().NewImagesMnager.MaxOrder();
            ViewBag.MaxOrder = _maxOrder;


            _filmed.Status = EntityStatus.Approval;
            ActionResult result = View(_filmed);

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

                        _filmed.MainImage = $"/attachments/{fileName}";
                        var _ctx = new UnitOfWork();



                        _filmed.CreatedTime = DateTime.Now;
                        int userId = GetUserId();
                        _filmed.UserCreateId = userId;
                        _filmed.Status = EntityStatus.Pending;
                        _filmed.UserEditId = userId;
                        _filmed.EditeTime = DateTime.Now;
                        _filmed = _ctx.FilmedManager.Add(_filmed);
                        var user = _ctx.UserManager.GetById(userId);

                        _ctx.logManager.Add(new log
                        {
                            UserId = GetUserId(),
                            ActionTime = DateTime.Now,
                            EntityId = _filmed.Id,
                            TableName = "Filmed",
                            Action = "Create:Filmed",
                            LoginDate = user.LoginDate,
                            LogoutDate = user.LogoutDate,
                            ActionTitle = _filmed.TitleArabic


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
                                _ctx.FilmedImagesManager.Add(new FilmedImages
                                {

                                    FilmedId = _filmed.Id,
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
            var entity = ctx.FilmedManager.GetBy(id);

            ctx.FilmedManager.Delete(entity);
            var user = ctx.UserManager.GetById(GetUserId());

            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Filmed",
                Action = "Delete:Filmed",
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
            var entity = ctx.FilmedManager.GetBy(id);
            entity.Status = status;
            ctx.FilmedManager.UpdateWithoutSave(entity);
            var user = ctx.UserManager.GetById(GetUserId());

            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Filmed",
                Action = "" + status.ToString() + ":Filmed",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic


            });

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}