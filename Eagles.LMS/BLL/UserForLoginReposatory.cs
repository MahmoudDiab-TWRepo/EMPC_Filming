using Eagles.LMS.Data;
using Eagles.LMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class UserForLoginReposatory:Reposatory<UserForLogin>
    {
        public UserForLoginReposatory(EmcNewsContext ctx):base(ctx)
        {

        }
    }
}