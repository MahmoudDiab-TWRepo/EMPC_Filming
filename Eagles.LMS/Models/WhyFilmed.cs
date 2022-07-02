using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    [Table("WhyFilmed")]
    public class WhyFilmed
    {
        public int Id { get; set; }

        public string Image { get; set; }

        //[Required(ErrorMessage = "Name Arabic Is Required")]

        public string TitleArabic { get; set; }
        //[Required(ErrorMessage = "Name English Is Required")]

        public string TitleEnglish { get; set; }

        //[Required(ErrorMessage = "Job Title Arabic Is Required")]


        public string DescriptionArabic { get; set; }
        //[Required(ErrorMessage = "Job Title English Is Required")]


        public string DescriptionEnglish { get; set; }


        public DateTime CreateTime { get; set; }
        public int UserCreateId { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }


    }
}