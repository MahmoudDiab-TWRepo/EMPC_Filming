using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    [Table("Sliders")]
    public class Slider
    {
        public int Id { get; set; }
        //[Required]

        public string Image { get; set; }
        //[Required(ErrorMessage = "Title Arabic Is Required")]

        [AllowHtml]
        public string TitleArabic { get; set; }

        //[Required(ErrorMessage = "Title English Is Required")]
        [AllowHtml]
        public string TitleEnglish { get; set; }
        
        //[Required(ErrorMessage = "Description Arabic Is Required")]


        public string DescriptionArabic { get; set; }
        //[Required(ErrorMessage = "Description English Is Required")]


        public string DescriptionEnglish { get; set; }
        [Required]

        public int Order { get; set; }

        public bool IsImage { get; set; }

        public String Iframe { get; set; }

        public DateTime CreateTime { get; set; }
        public int UserCreateId { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }

        public string Link { get; set; }

    }
}