using Eagles.LMS.BLL;
using Eagles.LMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Areas.Admission.Controllers
{
    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]
    public class LogsController : Controller
    {
        // GET: Admission/Logs
        public ActionResult Index()
        {
            UnitOfWork ctx = new UnitOfWork();
            var Logs = ctx.logManager.GetAllIncluded();
            return View(Logs);
        }
    }
}