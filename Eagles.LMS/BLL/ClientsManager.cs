using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class ClientsManager : Reposatory<Client>
    {
        public ClientsManager(LMS.Data.EmcNewsContext ctx) : base(ctx) { }
    }
}