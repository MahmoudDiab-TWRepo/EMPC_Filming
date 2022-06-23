using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class SettingsManager : Reposatory<Setting>
    {

        public SettingsManager(LMS.Data.EmcNewsContext ctx) : base(ctx) { }
    }

}