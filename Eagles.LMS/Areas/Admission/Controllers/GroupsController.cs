using Eagles.LMS.BLL;
using Eagles.LMS.DTO;
using Eagles.LMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Areas.Admission.Controllers
{
    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]
    public class GroupsController : Controller
    {
        private readonly UnitOfWork ctx = new UnitOfWork();
        // GET: Admission/Groups
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Create(GroupForManagerDTO groupForCreateDTO)
        {
            var group = ctx.GroupsManager.GetGroup(groupForCreateDTO.GroupName);
            if (group != null)
            {
                Response.StatusCode = 400;
                return Content("GroupNameIsAlreadyExisting");
            }
            ctx.GroupsManager.Add(groupForCreateDTO);
            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }
        public ActionResult Edit(int id)
        {

            var group = ctx.GroupsManager.GetBy(id);
            if (group == null)
                return HttpNotFound();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(GroupForManagerDTO groupForEditDTO)
        {
            var group = ctx.GroupsManager.GetGroup(groupForEditDTO.GroupName);
            if (group != null && group.Id != groupForEditDTO.GroupId)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The Group Is Already Existing!!");
            ctx.GroupsManager.Edit(groupForEditDTO);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            var group = ctx.GroupsManager.GetBy(id);
            if (group == null)
                return HttpNotFound();
            // check userInGroup 
            var users = ctx.UserManager.GetAll().Where(s => !s.IsDeleted && s.GroupId == id).ToList();
            if (users.Any())
            {

                return Json( new
                {
                    Code = 500,
                    Message = "Can Not Delete Group Contain Users ," +
                    " Plz Change User Group First And Try Again!!"
                }, JsonRequestBehavior.AllowGet);

            }
            try
            {
                ctx.GroupPriviageManager.DeletePrivilageInGroup(id);
                ctx.GroupsManager.Delete(group);
            }
            catch (Exception ex)

            {
                return Json(new
                {
                    Code = 500,
                    Message = "Group Can Not Be Deleted"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                Code = 200,
                Message = "success"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}