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
    public class CustomersController : Controller
    {
        // GET: Admission/Customers
        public ActionResult Index()
        {
            return View(new UnitOfWork().CustomerManager.GetAll().OrderByDescending(s => s.Id).ToList());
        }
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        public ActionResult Edit(int id)
        {
            var _customer = new UnitOfWork().CustomerManager.GetBy(id);
            if (_customer == null)
                return HttpNotFound();
            return View(_customer);
        }
        [HttpPost]
        public ActionResult Edit(Customer customer, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(customer);
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
                    customer.Image = $"/attachments/{fileName}";

                }

                int userId = GetUserId();
                customer.UserEditId = userId;
                customer.EditeTime = DateTime.Now;

                ctx.CustomerManager.UpdateWithSave(customer);

                var user = ctx.UserManager.GetById(GetUserId());

                ctx.logManager.Add(new log
                {
                    UserId = userId,
                    ActionTime = DateTime.Now,
                    EntityId = customer.Id,
                    TableName = "Customer",
                    Action = "Edit:Customer",
                    LoginDate = user.LoginDate,
                    LogoutDate = user.LogoutDate,
                    ActionTitle = customer.NameArabic
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
        public ActionResult Create(Customer customer, HttpPostedFileBase uploadattachments)
        {

            ActionResult result = View(customer);

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
                    customer.Image = $"/attachments/{fileName}";
                    int userId = GetUserId();
                    customer.UserCreateId = userId;
                    customer.CreateTime = DateTime.Now;
                    customer.UserEditId = userId;
                    customer.EditeTime = DateTime.Now;

                    var ctx = new UnitOfWork();
                    customer = ctx.CustomerManager.Add(customer);

                    var user = ctx.UserManager.GetById(GetUserId());

                    ctx.logManager.Add(new log
                    {
                        UserId = userId,
                        ActionTime = DateTime.Now,
                        EntityId = customer.Id,
                        TableName = "Customer",
                        Action = "Create:Customer",
                        LoginDate = user.LoginDate,
                        LogoutDate = user.LogoutDate,
                        ActionTitle = customer.NameArabic


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
            var entity = ctx.CustomerManager.GetBy(id);

            var user = ctx.UserManager.GetById(GetUserId());



            ctx.CustomerManager.Delete(entity);
            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = id,
                TableName = "Customer",
                Action = "Delete:Facilities",
                LoginDate = user.LoginDate,
                LogoutDate = user.LogoutDate,
                ActionTitle = entity.NameArabic


            });
            return Json(JsonRequestBehavior.AllowGet);
        }





















      


    }
}