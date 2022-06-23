using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class PrivilageRouteManager : Reposatory<PrivilageRoute>
    {
        public PrivilageRouteManager(LMS.Data.EmcNewsContext ctx) : base(ctx)
        {

        }
        public PrivilageRoute GetPrivilageRoute(int privilageId)
        {
            return ctx.PrivilageRoutes.FirstOrDefault(s => s.PrivilageId == privilageId);
        }
    }
}