using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eagles.LMS.DTO
{
    public class UserForUpdateProfile
    {
        [Required]
        public string Mobile { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
       
        //public int CityId { get; set; }
        [Required]
        public string FullName { get; set; }

        public string Password { get; set; }

        public string Image { get; set; }
        
    }
}