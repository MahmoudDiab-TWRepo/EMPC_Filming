using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Eagles.LMS.BLL
{
    public class BookingInServicesManager : Reposatory<BookingInServices>
    {
        public BookingInServicesManager(LMS.Data.EmcNewsContext ctx) : base(ctx) { }
        public List<BookingInServices> GetAllIncluded(int bookid)
        {
            return ctx.BookingInServices.Include(s => s.BookingService).Where(s => s.BookingId == bookid).ToList();
        }

    }
}