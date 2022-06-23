using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("SocialsMedia")]
    public class SocialMedia
    {
        public int Id { get; set; }

        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string Image { get; set; }
        //[Required]

        //public int Order { get; set; }
        [Required]

        public string Link { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserCreateId { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }

    }
}