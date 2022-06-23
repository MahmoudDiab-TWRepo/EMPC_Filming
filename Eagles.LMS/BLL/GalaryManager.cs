using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class GalaryManager : Reposatory<Galary>
    {
        public GalaryManager(EmcNewsContext ctx) : base(ctx)
        {

        }
        public List<Galary> GetAllIncluded()
        {
            return ctx.Galaries.Include(s=>s.Album).ToList();
        }
    }
}