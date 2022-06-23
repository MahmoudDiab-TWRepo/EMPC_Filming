using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public enum AccountState
    {
        Pending, Approval, Rejected, Blocked
    }

    public enum EntityStatus { Pending, Approval, Rejected }
}