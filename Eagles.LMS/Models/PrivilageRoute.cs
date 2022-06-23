using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{

    [Table("PrivilageRoutes")]
    public class PrivilageRoute
    {

        public PrivilageRoute()
        {
        }
        public int Id { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        [Required]
        public int PrivilageId { get; set; }
        public virtual Privilage Privilage { get; set; }
    }
}