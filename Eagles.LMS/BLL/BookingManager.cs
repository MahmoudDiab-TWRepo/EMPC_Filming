using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Eagles.LMS.BLL
{
    public class BookingManager : Reposatory<Booking>
    {
        public BookingManager(LMS.Data.EmcNewsContext ctx) : base(ctx) { }

        public List<Booking> GetAllIncluded()
        {
            return ctx.Bookings.Include(s => s.BookingInServices).ToList();
        }
    }
}