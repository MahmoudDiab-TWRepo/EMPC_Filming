using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class ProceduresManager : Reposatory<Procedures>
    {
        public ProceduresManager(EmcNewsContext ctx) : base(ctx)
        {

        }
    }
}