using Eagles.LMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eagles.LMS.Web_Services
{
    public class GroupsController : ApiController
    {
        private readonly UnitOfWork ctx = new UnitOfWork();
        [HttpGet]
        [Route("api/web/Groups/GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(ctx.GroupsManager.GetAll().Select(s => new { s.Id, s.Name }).ToList());// JsonRequestBehavior.AllowGet);
        }
    }
}
