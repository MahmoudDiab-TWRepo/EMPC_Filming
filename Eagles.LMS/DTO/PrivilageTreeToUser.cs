using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.DTO
{
    public class UsersTreePrivilage
    {
        public List<PrivilageTreeToUser> PrivilageTreeToUser { get; set; }
    }
    public class PrivilageTreeToUser
    {
        public ParentPrivilageRouting ParentPrivilageRouting_Lev1 { get; set; }
        public IEnumerable<PartentPrivilgaeLevel2> PartentPrivilgaeLevel2 { get; set; }
    }
    public class PartentPrivilgaeLevel2
    {
        public ParentPrivilageRouting ParentPrivilageRouting_Lev2 { get; set; }
        public IEnumerable<PartentPrivilgaeLevel3> PartentPrivilgaeLevel3 { get; set; }

    }
    public class PartentPrivilgaeLevel3
    {

        public Privilage Privilage { get; set; }
        public PrivilageRoute PrivilageRoute { get; set; }
    }
    public class ParentPrivilageRouting
    {
        public Privilage ParentPrivilage { get; set; }
        public PrivilageRoute PrivilageRoute { get; set; }
    }
}