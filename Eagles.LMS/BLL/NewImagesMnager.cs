using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class NewImagesMnager : Reposatory<NewImages>
    {
        public NewImagesMnager(EmcNewsContext ctx) : base(ctx)
        {

        }

        public int MaxOrder()
        {
            try
            {
                return ctx.News.Max(s => s.Order);
            }
            catch
            {
                return 0;
            }

        }
    }
}