using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("Settings")]
    public class Setting
    {
        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }

        public int Id { get; set; }
        public bool ShowAboutUs { get; set; }
        public bool ShowAgenda { get; set; }
        public bool ShowCitizen { get; set; }
        public bool ShowContactUs { get; set; }
        public bool ShowGalaryPhotos { get; set; }
        public bool ShowGalarVideos { get; set; }

        public bool ShowLocations { get; set; }
        public bool ShowNews { get; set; }
        public bool ShowRelatedWebSites { get; set; }
        public bool ShowServices { get; set; }
        public bool ShowSliders { get; set; }
        public bool ShowSocialMedias { get; set; }
        public bool ShowTeams { get; set; }

        public string MegaTags { get; set; }


    }
}