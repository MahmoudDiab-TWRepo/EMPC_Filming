using Eagles.LMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Security
{
    
    public class StudentAuthorizeFilterAttribute : ActionFilterAttribute
    {
        private readonly UnitOfWork ctx = new UnitOfWork();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var userFromSesstion = HttpContext.Current.Request.Cookies["User_Key"];
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
                var user = ctx.UserManager.GetBy(Convert.ToInt32(userFromSesstion.Value));
                if (user == null || user.IsDeleted || user.AccountState != Models.AccountState.Approval || user.UserTybe != Models.UserTybe.Studnet)
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();

                }
                else
                {
                    filterContext.HttpContext.Session["User_Key"] = user.Id;

                }

            }
        }

    }
}