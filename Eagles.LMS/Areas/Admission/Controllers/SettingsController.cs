using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eagles.LMS.BLL;
using Eagles.LMS.Data;
using Eagles.LMS.Models;
using Eagles.LMS.Security;

namespace Eagles.LMS.Areas.Admission.Controllers
{
    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]


    public class SettingsController : Controller
    {
        private EmcNewsContext db = new EmcNewsContext();

        // GET: Admission/Settings
        public ActionResult Index()
        {
            return View(new UnitOfWork().SettingsManager.GetAll().FirstOrDefault());
        }


        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }


        // GET: Admission/Settings/Create

        // POST: Admission/Settings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Index(Setting seting)
        {
            ActionResult result = View(seting);
            RequestStatus requestStatus;
            seting.EditeTime = DateTime.Now;
            seting.UserEditId = Convert.ToInt32(HttpContext.Session["User_Key"]);
            var ctx = new UnitOfWork();
            if (seting.Id == 0)
                ctx.SettingsManager.Add(seting);
            else
                ctx.SettingsManager.UpdateWithSave(seting);
            ctx.logManager.Add(new log
            {
                UserId = GetUserId(),
                ActionTime = DateTime.Now,
                EntityId = seting.Id,
                TableName = "Setting",
                Action = "Update:Setting"
            });
            requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);
            result = RedirectToAction(nameof(Index));
            return result;



        }

    }
}