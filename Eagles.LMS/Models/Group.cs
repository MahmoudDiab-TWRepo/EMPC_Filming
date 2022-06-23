using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eagles.LMS.Models
{
  

    [Table("Groups")]
    public class Group
    {
        public Group()
        {
            this.GroupPriviages = new HashSet<GroupPriviage>();
            this.Users = new HashSet<User>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<GroupPriviage> GroupPriviages { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}