using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace Eagles.LMS.Models
{
    [Table("Privilages")]
    public class Privilage
    {

        public Privilage()
        {
            this.GroupPriviages = new HashSet<GroupPriviage>();
            this.PrivilageRoutes = new HashSet<PrivilageRoute>();
        }

        public int Id { get; set; }
        public string MenueName { get; set; }
        public Nullable<int> ParentId { get; set; }
        public bool IsRoute { get; set; }
        public int OrderId { get; set; }
        public bool ShowInMenue { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }
        public virtual ICollection<GroupPriviage> GroupPriviages { get; set; } = new List<GroupPriviage>();

        public virtual ICollection<PrivilageRoute> PrivilageRoutes { get; set; } = new List<PrivilageRoute>();
    }
}