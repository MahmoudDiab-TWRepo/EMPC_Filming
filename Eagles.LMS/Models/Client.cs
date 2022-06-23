using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    [Table("Clients")]
    public class Client
    {
        public int Id { get; set; }

        public string Image { get; set; }

        //[Required(ErrorMessage = "Name Arabic Is Required")]
        public string NameArabic { get; set; }
        //[Required(ErrorMessage = "Name English Is Required")]
        public string NameEnglish { get; set; }

        public DateTime CreateTime { get; set; }
        public int UserCreateId { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }


    }
}