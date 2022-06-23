using Eagles.LMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Security
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        private readonly UnitOfWork ctx = new UnitOfWork();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var userFromSesstion = filterContext.HttpContext.Session["User_Key"];
            string url = filterContext.HttpContext.Request.Path;
            if (userFromSesstion == null)
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Area","" },
                    {"controller","Account" },
                    {"action","Login" },
                    {"redirectUrl", url},

                });
            else
            {
                var user = ctx.UserManager.GetBy(Convert.ToInt32(userFromSesstion));
                if (user == null || user.IsDeleted || user.AccountState != Models.AccountState.Approval || user.Group == null)
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();

                }
                else
                {

                    string controller = filterContext.RouteData.Values["Controller"].ToString();
                    string action = filterContext.RouteData.Values["Action"].ToString();
                    string area = filterContext.RouteData.DataTokens["area"].ToString();

                    if ((controller.ToLower() == "Home".ToLower() && action.ToLower() == "index".ToLower()) || action == "GetStudentAttendances" || action == "GetGroupAttendances")
                        return;
                    if (!ctx.GroupPriviageManager.IsGroupHavePermission((int)user.GroupId, controller, action, area))
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();
                    }
                }
            }
        }

    }
}