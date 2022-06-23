using Eagles.LMS.BLL;
using Eagles.LMS.Models;
using Eagles.LMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Areas.Admission.Controllers
{
    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]
    public class UsersController : Controller
    {
        private readonly UnitOfWork _ctx = new UnitOfWork();
        // GET: Admission/Users
        public ActionResult Index()
        {

           
            return View(_ctx.UserManager.GetAllAdminUser());
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var user = _ctx.UserManager.GetBy(id);
            if (user == null)
                return HttpNotFound();
            ViewBag.UserId = id;
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = _ctx.UserManager.GetBy(id);
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "User Not Found");

            try
            {

                user.IsDeleted = true;
                _ctx.UserManager.Edit(user);
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Can Not Delete This User");

            }

        }
        [HttpPost]
        public ActionResult ActiveAccount(int id)
        {
            var user = _ctx.UserManager.GetBy(id);
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "User Not Found");

            if (user.AccountState == AccountState.Blocked)
                user.AccountState = AccountState.Approval;
            else
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The Account Already Active");

            _ctx.UserManager.Edit(user);
            return Json(JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult BlockAccount(int id)
        {
            var user = _ctx.UserManager.GetBy(id);
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "User Not Found");


            if (user.AccountState == AccountState.Approval)
                user.AccountState = AccountState.Blocked;
            else
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The Account Already Blocked");
            _ctx.UserManager.Edit(user);
            return Json(JsonRequestBehavior.AllowGet);

        }
    }
}