using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    [Table("WhoWeAre")]
    public class WhoWeAre
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



        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }


    }
}