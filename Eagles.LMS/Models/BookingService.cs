using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{

    [Table("BookingServices")]
    public class BookingService
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<BookingInServices> BookingInServices { get; set; }


    }
}