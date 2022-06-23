using Eagles.LMS.BLL;
using Eagles.LMS.Data;
using Eagles.LMS.DTO;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Controllers
{

    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(string redirectUrl)
        {
            Session["USER_OTP_CODE_Mobile"] = null;
            if (Session["User_Key"] != null)
                return RedirectToAction("index", "home", new { area = "Admission" });

            ViewBag.RedirectUrl = redirectUrl;
            return View();
        }



        private void CreateUserCookie_Id(User user)
        {
            int exp = 3;
            HttpCookie userCookies_ID = new HttpCookie("User_Key");
            userCookies_ID.Value = user.Id.ToString();
            userCookies_ID.Expires = DateTime.Now.AddDays(exp);
            Response.Cookies.Add(userCookies_ID);
        }

        [HttpPost]
        public ActionResult ValidateOTP(UserForOTP userForOTP)
        {
            UnitOfWork ctx = new UnitOfWork();

            var user = ctx.UserManager.GetBy(userForOTP.UserId);
            if (user == null)
                return new HttpUnauthorizedResult();
            if (user.AccountState == AccountState.Rejected)
                return Json(new { Status = 401, Message = "Not allowed" }, JsonRequestBehavior.AllowGet);

            if (user.AccountState == AccountState.Pending)
                return Json(new { Status = 401, Message = "This account is pending .. Please wait for the account to be activated" }, JsonRequestBehavior.AllowGet);

            if (user.AccountState == AccountState.Blocked)
                return Json(new { Status = 401, Message = "This account is blocked .." }, JsonRequestBehavior.AllowGet);
            if (user.OTP != userForOTP.Code)

                return Json(new { Status = 500, Message = "Invalid Confirmation Code" }, JsonRequestBehavior.AllowGet);

            Session["USER_OTP_CODE_Mobile"] = user.Mobile;
            return Json(new { Status = 200 }, JsonRequestBehavior.AllowGet);

        }


  






 


 
        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        public ActionResult Logout()
        {
            UnitOfWork ctx = new UnitOfWork();
            var userid = GetUserId();
            var user = ctx.UserManager.GetById(userid);

            Session["User_Key"] = null;
            user.LogoutDate = DateTime.Now;
            ctx.UserManager.UpdateWithSave(user);

            var logout =  ctx.logManager.GetAllBind().Where(s => s.UserId == userid &&(s.LogoutDate == null||s.ActionTime>=s.LoginDate));
            foreach (var item in logout)
            {
                item.LogoutDate = DateTime.Now;
                ctx.logManager.UpdateWithSave(item);

            }
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        public ActionResult Login(UserForLogin userForLogin)
        {
            if (ModelState.IsValid)
            {

                UnitOfWork ctx = new UnitOfWork();

                var user = ctx.UserManager.Login(userForLogin.LoginName, userForLogin.Password);
                if (user == null)
                    return new HttpUnauthorizedResult();
                
                CreateUserCookie_Id(user);
                Session["User_Key"] = user.Id;
                user.LoginDate = DateTime.Now;
                ctx.UserManager.UpdateWithSave(user);
                return Json(new { Status = 200, AccountType = user.UserTybe.ToString(), Message = "Login-Success" }, JsonRequestBehavior.AllowGet);


            }
            else
                return new HttpUnauthorizedResult();
        }

        public ActionResult PublicLogOut()
        {
            if (Request.Cookies["UserId"] != null)
            {
                Response.Cookies["UserId"].Expires = Shared.GetDateTime().AddDays(-1);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}