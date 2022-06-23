using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}