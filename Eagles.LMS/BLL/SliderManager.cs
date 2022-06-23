using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class SliderManager : Reposatory<Slider>
    {
        public SliderManager(EmcNewsContext ctx) : base(ctx)
        {

        }
    }
}