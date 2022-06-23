using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("ServiceImages")]
    public class ServiceImages
    
    {
        public int Id { get; set; }

       public string Path { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }


    }

}