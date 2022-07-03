using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Eagles.LMS.Models;
using Eagles.LMS.DTO;

namespace Eagles.LMS.Data
{
    public class EmcNewsContext : DbContext
    {
        public EmcNewsContext() : base("EmcNewsConnection")
        {

        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Privilage> Privilages { get; set; }    
        public DbSet<PrivilageRoute> PrivilageRoutes { get; set; }
        public DbSet<GroupPriviage> GroupPriviages { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<SocialMedia> SocialMedia { get; set; }

        public DbSet<NewImages> NewImages { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<AgendaImages> AgendaImages { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<UserForLogin> UserForLogins { get; set; }

        public DbSet<LocationImages> LocationImages { get; set; }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<New> News { get; set; }

        public DbSet<Filmed> Filmed { get; set; }
        public DbSet<FilmedImages> FilmedImages { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }

        public DbSet<Procedures> Procedures { get; set; }
        public DbSet<Documents> Documents { get; set; }

        public DbSet<WhoWeAre> WhoWeAre { get; set; }
        public DbSet<Galary> Galaries { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<ContactUs> ContactUs { get; set; }

        public DbSet<ConatctRequist> ConatctRequist { get; set; }
        public DbSet<RelatedWebSite> RelatedWebSites { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Facilities> Facilities { get; set; }

        public DbSet<WhyFilmed> WhyFilmed { get; set; }
        public DbSet<ServiceImages> ServiceImages { get; set; } 

        public DbSet<Citizens> Citizens { get; set; }

        public DbSet<BookingService> BookingService { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<BookingInServices> BookingInServices { get; set; }

        public DbSet<log> logs { get; set; }

        public DbSet<Client> Clients { get; set; }










    }
}