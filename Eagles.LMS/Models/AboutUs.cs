using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    [Table("AboutUs")]
    public class AboutUs
    {
        public int Id { get; set; }

        public string Image { get; set; }

        //[Required(ErrorMessage = "Title Arabic Is Required")]


        public string TitleArabic { get; set; }
        //[Required(ErrorMessage = "Title English Is Required")]


        public string TitleEnglish { get; set; }


        //[Required(ErrorMessage = "Description Arabic Is Required")]
        [AllowHtml]
        public string DescriptionArabic { get; set; }

        //[Required(ErrorMessage = "Description English Is Required")]
        [AllowHtml]
        public string DescriptionEnglish { get; set; }

        [Required(ErrorMessage = "YouTube Frame Is Required")]

        public string YouTubeFrame { get; set; }


        //[Required(ErrorMessage = "Home Tap Description Arabic Is Required")]
        [AllowHtml]
        public string WHoWeDescriptionArabic { get; set; }
        //[Required(ErrorMessage = "Home Tap Description English Is Required")]
        [AllowHtml]

        public string WhoWeDescriptionEnglish { get; set; }
        //[Required(ErrorMessage = "Profile Description Arabic Is Required")]

        public string ImageWhoWe { get; set; }

        [AllowHtml]
        public string ProfileDescriptionArabic { get; set; }
        //[Required(ErrorMessage = "Profile Description English Is Required")]
        [AllowHtml]

        public string ProfileDescriptionEnglish { get; set; }

        //[Required(ErrorMessage = "Message Tap Description Arabic Is Required")]
        [AllowHtml]

        public string MessageTapDescriptionArabic { get; set; }
        //[Required(ErrorMessage = "Message Tap Description English Is Required")]
        [AllowHtml]

        public string MessageTapDescriptionEnglish { get; set; }

        [Required]
        public int Order { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }


    }
}