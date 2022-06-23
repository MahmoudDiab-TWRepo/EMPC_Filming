using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("Albums")]
    public class Album
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Title Arabic Is Required")]

        public string TitleArabic { get; set; }
        //[Required(ErrorMessage = "Title English Is Required")]

        public string TitleEnglish { get; set; }

        public string Image { get; set; }

        public List<Galary> Galaries { get; set; }

        public int Order { get;set; }

        public DateTime CreateTime { get; set; }
        public int UserCreateId { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }



    }
}