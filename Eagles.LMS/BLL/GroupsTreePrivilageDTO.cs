using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class GroupsTreePrivilageDTO
    {
        public Group Group { get; set; }
        public List<PrivilageTree> PrivilageTree { get; set; }
        public bool IsDataLevelOnly { get; set; }
    }
    public class PrivilageTree
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public string ParentName { get; set; }
        public List<Privilage> Privilages { get; set; }
    }
    public class PrivilageTreeCustome
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public string ParentName { get; set; }
        public List<ChildPrivilageTree> Privilages { get; set; }
    }
    public class ChildPrivilageTree
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public bool IsRoute { get; set; }
        public int OrderId { get; set; }
        public string MenueName { get; set; }
        public int? ParentId { get; set; }
        public bool ShowInMenue { get; set; }




    }
}