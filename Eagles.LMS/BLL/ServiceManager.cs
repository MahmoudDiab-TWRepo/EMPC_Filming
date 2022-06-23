using Eagles.LMS.BLL;
using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class ServiceManager : Reposatory<Service>
    {
        public ServiceManager(EmcNewsContext ctx) : base(ctx)
        {

        }
    }
}