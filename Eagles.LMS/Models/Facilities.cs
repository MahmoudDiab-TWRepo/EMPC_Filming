using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    [Table("Facilities")]
    public class Facilities
    {
        public int Id { get; set; }

        public string Image { get; set; }

        //[Required(ErrorMessage = "Name Arabic Is Required")]

        public string TitleArabic { get; set; }
        //[Required(ErrorMessage = "Name English Is Required")]

        public string TitleEnglish { get; set; }

        //[Required(ErrorMessage = "Job Title Arabic Is Required")]

        public string ShortDiscriptionArabic { get; set; }
        //[Required(ErrorMessage = "Job Title English Is Required")]

        public string ShortDiscriptionEnglish { get; set; }

        [AllowHtml]
        public string DiscriptionArabic { get; set; }
        //[Required(ErrorMessage = "Job Title English Is Required")]
        [AllowHtml]
        public string DiscriptionEnglish { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserCreateId { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }


    }
}