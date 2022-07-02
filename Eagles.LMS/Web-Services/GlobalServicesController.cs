using Eagles.LMS.BLL;
using Eagles.LMS.Helper;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eagles.LMS.Web_Services
{
    public class GlobalServicesController : ApiController
    {
        [HttpPost]
        [Route("api/GlobalServices/RemoveServicesImg")]
        public async Task<IHttpActionResult> EditUser(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var serviceImg = ctx.ServiceImagesManager.GetBy(id);
            if (serviceImg != null)
            {
                ctx.ServiceImagesManager.Delete(serviceImg);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/GlobalServices/RemoveNewsImg")]
        public async Task<IHttpActionResult> RemoveNewsImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var newsImg = ctx.NewImagesMnager.GetBy(id);
            if (newsImg != null)
            {
                ctx.NewImagesMnager.Delete(newsImg);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/GlobalServices/RemoveNewsMainImg")]
        public async Task<IHttpActionResult> RemoveNewsMainImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var _new = ctx.NewManager.GetBy(id);
            if (_new != null)
            {
                _new.MainImage = "";
                ctx.NewManager.UpdateWithSave(_new);

                return Ok();
            }
            return NotFound();
        }




        [HttpPost]
        [Route("api/GlobalServices/RemoveFilmedImg")]
        public async Task<IHttpActionResult> RemoveFilmdImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var filmedImg = ctx.FilmedImagesManager.GetBy(id);
            if (filmedImg != null)
            {
                ctx.FilmedImagesManager.Delete(filmedImg);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/GlobalServices/RemoveFilmedMainImg")]
        public async Task<IHttpActionResult> RemoveFilmedMainImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var _filmed = ctx.FilmedManager.GetBy(id);
            if (_filmed != null)
            {
                _filmed.MainImage = "";
                ctx.FilmedManager.UpdateWithSave(_filmed);

                return Ok();
            }
            return NotFound();
        }


        [HttpPost]
        [Route("api/GlobalServices/RemoveAgendaMainImg")]
        public async Task<IHttpActionResult> RemoveAgendaMainImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var _new = ctx.AgendaManager.GetBy(id);
            if (_new != null)
            {
                _new.MainImage = "";
                ctx.AgendaManager.UpdateWithSave(_new);

                return Ok();
            }
            return NotFound();
        }






        [HttpPost]
        [Route("api/GlobalServices/RemoveLocationsMainImg")]
        public async Task<IHttpActionResult> RemoveLocationsMainImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var _new = ctx.LocationManager.GetBy(id);
            if (_new != null)
            {
                _new.MainImage = "";
                ctx.LocationManager.UpdateWithSave(_new);

                return Ok();
            }
            return NotFound();
        }





        [HttpPost]
        [Route("api/GlobalServices/RemoveServicesMainImg")]
        public async Task<IHttpActionResult> RemoveServicesMainImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var _new = ctx.ServiceManager.GetBy(id);
            if (_new != null)
            {
                _new.MainImage = "";
                ctx.ServiceManager.UpdateWithSave(_new);
                return Ok();
            }
            return NotFound();
        }



        [HttpPost]
        [Route("api/GlobalServices/RemoveLocationsIconImg")]
        public async Task<IHttpActionResult> RemoveLocationsIconImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var _new = ctx.LocationManager.GetBy(id);
            if (_new != null)
            {
                _new.IconImage = "";
                ctx.LocationManager.UpdateWithSave(_new);

                return Ok();
            }
            return NotFound();
        }








        [HttpPost]
        [Route("api/GlobalServices/RemoveLocationsImg")]
        public async Task<IHttpActionResult> RemoveLocationsImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var locationImage = ctx.LocationImagesManager.GetBy(id);
            if (locationImage != null)
            {
                ctx.LocationImagesManager.Delete(locationImage);
                return Ok();
            }
            return NotFound();
        }
        [HttpPost]
        [Route("api/GlobalServices/RemoveAgendaImg")]
        public async Task<IHttpActionResult> RemoveAgendaImg(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var agendaImg = ctx.AgendaImagesManager.GetBy(id);
            if (agendaImg != null)
            {
                ctx.AgendaImagesManager.Delete(agendaImg);
                return Ok();
            }
            return NotFound();
        }


        [HttpGet]
        [Route("api/GlobalServices/GetAgendaByDate")]
        public async Task<IHttpActionResult> GetAgendaByDate(DateTime _Date)
        {
            return Ok(new UnitOfWork().AgendaManager.GetAll().Where(s => s.AgendaDate.Date == _Date.Date).ToList());
        }



        [HttpPost]
        [Route("api/GlobalServices/AddBooking")]

        public async Task<IHttpActionResult> AddBooking([FromBody] Booking booking)
        {
            var ctx = new UnitOfWork();
            var book = ctx.BookingManager.Add(booking);
            string ServicesNames = "";

            if (book.Services != null && book.Services.Any())
            {
                foreach (var item in book.Services)
                {
                    ServicesNames += ctx.BookingServiceManager.GetAll().FirstOrDefault(s => s.Id == item).Name + "\n";
                    ctx.BookingInServicesManager.Add(new BookingInServices
                    {
                        BookingServiceId = item,
                        BookingId = book.Id

                    });
                }
            }
            //try send email
            try
            {
                new SendEmail().SendMail(new EmailDTO { To = book.CompanyEmail, Message = book.Message + "<br />" + "<b style='font-size:12px; line-height:1.5'>Company Name:</b>" + booking.CompanyName + "<br />" + "<b style='font-size:12px; line-height:1.5'>Booking Start time:</b>" + book.SartTime + "<br />" + "<b style='font-size:12px; line-height:1.5'>Booking End Time :</b>" + book.EndTime + "<br />" + "<b style='font-size:12px; line-height:1.5'>Orgin:</b>" + book.Origin + "<br />" + "<b style='font-size:12px; line-height:1.5'>Booking Services:</b>" + ServicesNames, From = "web@empcnews.com", Subject = "New Booking" });
                //new SendEmail().SendMail(new EmailDTO { To = book.CompanyEmail, Message = book.Message + "\n" + "Company Name:" + booking.CompanyName + "\n" + "Booking Start time:" + book.SartTime + "\n" + "Booking End Time :" + book.EndTime + "\n" + "Orgin:" + book.Origin + "\n" + "Booking Services:" + ServicesNames, From = "web@empcnews.com", Subject = "New Booking" });
            }
            catch
            {

            }
            return Ok();
        }






    }
}