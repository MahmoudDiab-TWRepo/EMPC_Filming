using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.DTO
{
    public class GroupForManagerDTO
    {
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public int[] privilages { get; set; }
    }
}