using Eagles.LMS.BLL;
using Eagles.LMS.DTO;
using Eagles.LMS.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eagles.LMS.Web_Services
{
    public class UsersController : ApiController
    {
        UnitOfWork ctx = new UnitOfWork();


             [HttpPost]
        [Route("api/web/user/{userID}/ChangeUserStatus")]
        public IHttpActionResult ChangeUserStatus(int userID)
        {
            var user = ctx.UserManager.GetBy(userID);
            if (user == null)
                return NotFound();
            else
            {
                AccountState accountState = user.AccountState == Models.AccountState.Blocked ? Models.AccountState.Approval : Models.AccountState.Blocked;
                     user.AccountState = accountState;
                ctx.UserManager.UpdateWithSave(user);
                return Ok();
            }

        }
    }
}
