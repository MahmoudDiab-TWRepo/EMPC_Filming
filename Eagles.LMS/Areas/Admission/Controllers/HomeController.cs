using Eagles.LMS.BLL;
using Eagles.LMS.DTO;
using Eagles.LMS.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Eagles.LMS.DTO.UserForRefused;

namespace Eagles.LMS.Areas.Admission.Controllers
{
    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]

    //[AuthorizeFilterAttribute]
    public class HomeController : Controller
    {
        UnitOfWork ctx = new UnitOfWork();

        // GET: Admission/Home
        public ActionResult Index()
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult AccountApproval(List<UserForApproval> users)
        {
            if (users != null)
            {
                foreach (var _user in users)
                {


                    var user = ctx.UserManager.GetBy(_user.Id);
                    if (user == null)
                        continue;
                    else
                    {
                        if (user.AccountState != Models.AccountState.Approval)
                        {
                            user.AccountState = Models.AccountState.Approval;
                            var jData = new JObject();
                            //jData.Add("Message", "i hope your day is fine isa :=D");
                            ctx.UserManager.UpdateWithSave(user);
                        }


                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AccountApproval(int id)
        {
            var user = ctx.UserManager.GetBy(id);
            if (user == null)
                return HttpNotFound();
            else
            {
                user.AccountState = Models.AccountState.Approval;
                    ctx.UserManager.UpdateWithSave(user);
                return RedirectToAction(nameof(Index));
            }
        }
        //[HttpPost]
        //public ActionResult AccountRefuesd(UserForRefused userForRefused)
        //{
        //    var user = ctx.UserManager.GetBy(userForRefused.UserId);
        //    if (user == null)
        //        return HttpNotFound();
        //    else
        //    {

        //        user.AccountState = Models.AccountState.Rejected;
        //        var jData = new JObject();
        //        string message = "your account has been refused";
        //        if (userForRefused.Comment != null && userForRefused.Comment != "" && userForRefused.Comment != " ")
        //            message += " because " + userForRefused.Comment;
        //        jData.Add("Message", "Hello, " + message + "");
        //        var url = HttpContext.Server.MapPath("~/app.json");
        //        FireBaseTokensDTO fireBaseTokens = FireBaseManager.GetTokens(url);
        //        Notifications.Send(user.FireBaseToken, jData, fireBaseTokens.FireBaseTokens);
        //        // delete studnet first 
        //        var studnt = ctx.StudentManager.GetStudentbyUserId(user.Id);
        //        if (studnt != null)
        //        {
        //            ctx.StudentManager.Delete(studnt);
        //        }
        //        // delete studnet
        //        ctx.UserManager.Delete(user);


        //        return Json(JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult AccountRefuesd(List<UserForRefused> userForRefused)
        {
            if (userForRefused != null)
            {
                foreach (var _user in userForRefused)
                {


                    var user = ctx.UserManager.GetBy(_user.UserId);
                    if (user == null)
                        continue;
                    else
                    {

                        user.AccountState = Models.AccountState.Rejected;
                        var jData = new JObject();
                        string message = "your account has been refused";
                        // delete studnet first 
                        // delete studnet
                        ctx.UserManager.Delete(user);
                    }
                }
            }
            return Json(JsonRequestBehavior.AllowGet);

        }

        public ActionResult constview() => View();

        public ActionResult constviewdatatable() => View();
        public ActionResult mozwedkedmatalep() => View();
        public ActionResult Addmozwedkedmatalep() => View();

        public ActionResult ResetPrintpage() => View();
        public ActionResult A4Printpge() => View();
    }
 
}