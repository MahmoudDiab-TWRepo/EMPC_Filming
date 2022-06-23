using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("Galaries")]
    public class Galary
    {
        public int Id { get; set; }

        public string Image { get; set; }
        //[Required(ErrorMessage = "Title Arabic Is Required")]

        public string TitleArabic { get; set; }
        //[Required(ErrorMessage = "Title English Is Required")]

        public string TitleEnglish { get; set; }
        [Required]
        public int Order { get; set; }

        public bool IsImage { get;set; }

        public String VideoIframe { get; set; }

        //public String VideoCover { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserCreateId { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }

        public Album Album { get; set; }
        public int? AlbumId { get; set; }


    }
}