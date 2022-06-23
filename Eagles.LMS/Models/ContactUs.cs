using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("ContactUs")]
    public class ContactUs
    {
        public int Id { get; set; }

        [Required]
        public string Phone { get; set; }
        //[Required(ErrorMessage = "Address Arabic Is Required")]


        public string AddressArabic { get; set; }
        //[Required(ErrorMessage = "Address English Is Required")]


        public string AddressEnglish { get; set; }
        [Required]


        public string Email { get; set; }


        public string Logo { get; set; }

      
        //[Required(ErrorMessage = "Company Title Arabic Is Required")]



        public string CompanyTitleArabic { get; set; }
        //[Required(ErrorMessage = "Company Title English Is Required")]


        public string CompanyTitleEnglish { get; set; }

        public string CitizenEmail { get;set; }




        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }

    }
}