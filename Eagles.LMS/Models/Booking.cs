using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Eagles.LMS.Models
{

    [Table("Bookings")]

    public class Booking
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }

        public string DataServicees { get; set; }

        public string SartTime { get; set; }

        public string EndTime { get; set; }

        public string Origin { get; set; }

        public string Message { get; set; }

        public List<BookingInServices> BookingInServices { get; set; }
       
        [NotMapped]
        public int[] Services { get; set; }

    }
   
}