using Eagles.LMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Areas.Admission
{
    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]

    public class AccountsController : Controller
    {
        // GET: Admission/Accounts
        public ActionResult ServiceProvider()
        {
            return View();
        }
        public ActionResult CreateServiceProvider()
        {
            return View();
        }
        public ActionResult Resetpaper()
        {
            return View();
        }
        
        public ActionResult A4paper()
        {
            return View();
        }

        public ActionResult Pendingstudentpayments()
        {
            return View();
        }
        public ActionResult StudentPayment()
        {
            return View();
        }

    }
}