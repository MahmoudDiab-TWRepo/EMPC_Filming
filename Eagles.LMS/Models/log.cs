using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("logs")]
    public class log
    {
        public int Id { get; set; }
        public User User { get;set; }
        public int UserId { get; set; }

        public string Action { get; set; }
        public DateTime ActionTime { get; set; }
        public string TableName { get;set; }
        public int? EntityId { get;set; }
        public DateTime? LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public string ActionTitle { get; set; }








    }
}