using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.DTO
{
    public class UserForRefused
    {
        public int UserId { get; set; }
        public string Comment { get; set; }

        public class UserForApproval
        {
            public int Id { get; set; }
        }
     

    }
}