using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("BookingInServices")]
    public class BookingInServices
    {
        public int Id { get; set; }
        public BookingService BookingService { get; set; }

        public Booking Booking { get; set; }

        public int BookingServiceId { get; set; }

        public int BookingId { get; set; }


    }
}