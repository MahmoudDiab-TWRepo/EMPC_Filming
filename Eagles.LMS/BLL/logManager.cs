using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
namespace Eagles.LMS.BLL
{
    public class logManager : Reposatory<log>
    {
        public logManager(LMS.Data.EmcNewsContext ctx) : base(ctx) { }

        public List<log> GetAllIncluded()
        {
            return ctx.logs.Include(l => l.User).OrderByDescending(s=>s.Id).ToList();
        }

    }
}