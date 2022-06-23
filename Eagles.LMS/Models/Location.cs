using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    [Table("Locations")]
    public class Location
    {
        public int Id { get; set; }

        public string IconImage { get; set; }

        public string MainImage { get; set; }

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

        public List<LocationImages> LocationImages { get; set; }

        public string LocationVideo { get; set; }
       

        public EntityStatus Status { get; set; }



        //[Required(ErrorMessage = "Address Arabic Is Required")]
        public string AddressArabic { get; set; }


        //[Required(ErrorMessage = "Address English Is Required")]
        //[Required]
        public string AddressEnglish { get; set; }
        //[Required]

        //public int Order { get; set; }


        public DateTime CreateTime { get; set; }
        public int UserCreateId { get; set; }


        //[Required(ErrorMessage = "Short Description Arabic Is Required")]
        public string ShortDescriptionArabic { get; set; }

        //[Required(ErrorMessage = "Short Description English Is Required")]
        public string ShortDescriptionEnglish { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }


    }
}