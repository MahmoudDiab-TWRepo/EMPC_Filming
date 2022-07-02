using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    [Table("Filmed")]
    public class Filmed
    {
        public int Id { get; set; }

        public string MainImage { get; set; }
        public string VideoIframe { get; set; }

        public string TitleArabic { get; set; }
        //[Required(ErrorMessage = "Title English Is Required")]

        public string TitleEnglish { get; set; }

        public string TypeArabic { get; set; }
        public string TypeEnglish { get; set; }
        public string ViewsNumber { get; set; }

        [AllowHtml]
        public string DescriptionArabic { get; set; }
        [AllowHtml]
        public string DescriptionEnglish { get; set; }

        public string LanguageArabic { get; set; }
        public string LanguageEnglish { get; set; }

        public string SubtitleArabic { get; set; }
        public string SubtitleEnglish { get; set; }

        public string AudioLanguageArabic { get; set; }
        public string AudioLanguageEnglish { get; set; }

        public string GenreArabic { get; set; }
        public string GenreEnglish { get; set; }

        public string RunTime { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool ShowInHomePage { get; set; }



        public List<FilmedImages> FilmedImages { get; set; }

        public DateTime CreatedTime { get; set; }
        [Required]
        public int Order { get; set; }


        public int UserCreateId { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }
        public EntityStatus Status { get; set; }
    }
}