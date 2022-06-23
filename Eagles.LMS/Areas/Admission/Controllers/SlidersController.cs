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
    public class SlidersController : Controller
    {
        // GET: Admission/Slider
        public ActionResult Index()
        {

            return View(new UnitOfWork().SliderManager.GetAll().OrderByDescending(s => s.Order).ToList()); 
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var slider = new UnitOfWork().SliderManager.GetBy(id);
            if (slider == null)
                return HttpNotFound();
            return View(slider);
        }
        [HttpPost]
        public ActionResult Edit(Slider slider, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(slider);


            RequestStatus requestStatus;
            if ((string.IsNullOrEmpty(slider.Iframe)) && (uploadattachments != null && (!

                    uploadattachments.ContentType.CheckImageExtention() && !
                    uploadattachments.ContentType.CheckVideoExtention())))
            {

                requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");
            }
            else
            {
                if (string.IsNullOrEmpty(slider.Iframe))
                {



                    if (uploadattachments != null)
                    {

                        string _rendom = new Random().Next(1, 99999999).ToString();

                        //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                        string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                        var fileName = _rendom + extention;

                        var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        uploadattachments.SaveAs(path);
                        slider.Image = $"/attachments/{fileName}";
                        slider.IsImage = uploadattachments.ContentType.CheckImageExtention();

                    }

                }
                else
                {
                    slider.Image = "";
                    slider.IsImage = false;
                    //slider.Iframe = slider.Iframe.Split('&')[0].Split('=')[1].ToString();

                    try
                    {
                        slider.Iframe = slider.Iframe.Split('/').Last().ToString();
                    }
                    catch
                    {
                        requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError,
                              "Uncompatible IFrame Formtaing");
                        return result;
                    }
                    slider.Iframe = slider.Iframe.Split('/').Last().ToString();
                }


                int userId = GetUserId();
                slider.UserEditId = userId;
                slider.EditeTime = DateTime.Now;

                var ctx = new UnitOfWork();
                ctx.SliderManager.UpdateWithSave(slider);
                var user = ctx.UserManager.GetById(GetUserId());

                ctx.logManager.Add(new log
                {
                    UserId = GetUserId(),
                    ActionTime = DateTime.Now,
                    EntityId = slider.Id,
                    TableName = "Slider",
                    Action = "Update:Slider",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = slider.TitleArabic


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
        public ActionResult Create(Slider slider, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(slider);

            if (ModelState.IsValid)
            {

                RequestStatus requestStatus;
                if ((string.IsNullOrEmpty(slider.Iframe)) && (uploadattachments == null || uploadattachments.ContentLength == 0 || (!

                    //if (uploadattachments == null || uploadattachments.ContentLength == 0 || (!
                    uploadattachments.ContentType.CheckImageExtention() && !
                    uploadattachments.ContentType.CheckVideoExtention())))
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Or Video Only");
                }
                else
                {

                    if (string.IsNullOrEmpty(slider.Iframe))
                    {




                        string _rendom = new Random().Next(1, 99999999).ToString();

                        //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                        string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                        var fileName = _rendom + extention;

                        var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        uploadattachments.SaveAs(path);
                        slider.Image = $"/attachments/{fileName}";
                        slider.IsImage = uploadattachments.ContentType.CheckImageExtention();
                    }
                    else
                    {
                        slider.Image = "";
                        slider.IsImage = false;
                        //slider.Iframe = slider.Iframe.Split('&')[0].Split('=')[1].ToString();
                        try
                        {
                            slider.Iframe = slider.Iframe.Split('/').Last().ToString();
                        }
                        catch
                        {
                            requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError,
                                "Uncompatible IFrame Formtaing");
                            return result;

                        }
                    }

                    int userId = GetUserId();
                    slider.UserCreateId = userId;
                    slider.CreateTime = DateTime.Now;
                    slider.UserEditId = userId;
                    slider.EditeTime = DateTime.Now;
                    var ctx = new UnitOfWork();
                    slider = ctx.SliderManager.Add(slider);
                    var user = ctx.UserManager.GetById(GetUserId());

                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = slider.Id,
                        TableName = "Slider",
                        Action = "Create:Slider",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = slider.TitleArabic


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
            var entity = ctx.SliderManager.GetBy(id);

            ctx.SliderManager.Delete(entity);
            var user = ctx.UserManager.GetById(GetUserId());

            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Slider",
                Action = "Delete:Slider",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.TitleArabic


            });

           
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}