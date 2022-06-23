using Eagles.LMS.BLL;
using Eagles.LMS.Models;
using Eagles.LMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Security
{
    public class MixAuthorizeFilterAttribute : ActionFilterAttribute
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
                if (user == null || user.IsDeleted || user.AccountState != Models.AccountState.Approval)
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();
                }

            }

        }
    }

}