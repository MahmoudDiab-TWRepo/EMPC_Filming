using Eagles.LMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class UnitOfWork
    {


        private readonly EmcNewsContext ctx = new EmcNewsContext();
        public UserManager UserManager { get { return new UserManager(ctx); } }

        public BookingServiceManager BookingServiceManager { get { return new BookingServiceManager(ctx); } }


        

        public UserForLoginReposatory UserForLoginReposatory { get { return new UserForLoginReposatory(ctx); } }

        public GroupsManager GroupsManager { get { return new GroupsManager(ctx); } }

        public AlbumManager AlbumManager { get { return new AlbumManager(ctx); } }
        public GroupPriviageManager GroupPriviageManager { get { return new GroupPriviageManager(ctx); } }

        public AgendaManager AgendaManager { get { return new AgendaManager(ctx); } }
        public AgendaImagesManager AgendaImagesManager { get { return new AgendaImagesManager(ctx); } }
        public NewImagesMnager NewImagesMnager { get { return new NewImagesMnager(ctx); } }


        public SocialMediaManager SocialMediaManager { get { return new SocialMediaManager(ctx); } }

        public PrivilageManager PrivilageManager { get { return new PrivilageManager(ctx); } }
        public PrivilageRouteManager PrivilageRouteManager { get { return new PrivilageRouteManager(ctx); } }
        public LocationImagesManager LocationImagesManager { get { return new LocationImagesManager(ctx); } }
        public SliderManager SliderManager { get { return new SliderManager(ctx); } }

        public ServiceImagesManager ServiceImagesManager { get { return new ServiceImagesManager(ctx); } }
        public LocationManager LocationManager { get { return new LocationManager(ctx); } }
        public AboutUsManager AboutUsManager { get { return new AboutUsManager(ctx); } }
        public ServiceManager ServiceManager { get { return new ServiceManager(ctx); } }
        public GalaryManager GalaryManager { get { return new GalaryManager(ctx); } }
        public ContactUsManager ContactUsManager { get { return new ContactUsManager(ctx); } }

        public RelatedWebSiteManager RelatedWebSiteManager { get { return new RelatedWebSiteManager(ctx); } }
        public TeamManager TeamManager { get { return new TeamManager(ctx); } }

        public NewManager NewManager { get { return new NewManager(ctx); } }

        public CitizensManager CitizensManager { get { return new CitizensManager(ctx); } }
        public SettingsManager SettingsManager { get { return new SettingsManager(ctx); } }
        public BookingManager BookingManager { get { return new BookingManager(ctx); } }
        public BookingInServicesManager BookingInServicesManager { get { return new BookingInServicesManager(ctx); } }

        public logManager logManager { get { return new logManager(ctx); } }

        public ClientsManager ClientsManager { get { return new ClientsManager(ctx); } }







    }
}