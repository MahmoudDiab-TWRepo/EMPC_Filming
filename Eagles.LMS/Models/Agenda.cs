using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    public class Agenda
    {

        public int Id { get; set; }

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

        public EntityStatus Status { get; set; }


        public List<AgendaImages> AgendaImages { get; set; }
        [Required]
        public int Order { get; set; }
        public DateTime AgendaDate { get; set; }


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