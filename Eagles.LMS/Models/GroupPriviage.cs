using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eagles.LMS.Models
{

    [Table("GroupPriviages")]
    public class GroupPriviage
    {
        public GroupPriviage()
        {
        }
        public int Id { get; set; }
        [Required]
        public int PrivilageId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public virtual Privilage Privilage { get; set; }
    }
}