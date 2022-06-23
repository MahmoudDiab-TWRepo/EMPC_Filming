using Eagles.LMS.Data;
using Eagles.LMS.Models;
using Eagles.LMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.BLL
{
    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]
    public class ContactUsManager:Reposatory<ContactUs>
    {
        public ContactUsManager(EmcNewsContext ctx) : base(ctx)
        {

        }
    }
}