using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class FilmedImagesManager : Reposatory<FilmedImages>
    {
        public FilmedImagesManager(EmcNewsContext ctx) : base(ctx)
        {

        }

        public int MaxOrder()
        {
            try
            {
                return ctx.Filmed.Max(s => s.Order);
            }
            catch
            {
                return 0;
            }

        }
    }
}