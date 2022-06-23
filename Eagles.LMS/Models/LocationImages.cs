using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("LocationImages")]
    public class LocationImages
    {
        public int Id { get; set; }

        public string Path { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }


    }
}