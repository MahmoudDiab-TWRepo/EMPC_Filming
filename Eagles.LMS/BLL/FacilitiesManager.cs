using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class FacilitiesManager : Reposatory<Facilities>
    {
        public FacilitiesManager(EmcNewsContext ctx) : base(ctx)
        {

        }
    }


}