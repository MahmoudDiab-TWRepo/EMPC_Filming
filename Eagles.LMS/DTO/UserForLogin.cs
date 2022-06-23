using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eagles.LMS.DTO
{
    public class UserForOTP
    {
        public string Code { get; set; }
        public int UserId { get; set; }
    }
    public class UserForLogin
    {
        public int Id { get; set; }
        [Required]
        public string LoginName { get; set; }
        [Required]

        public string Password { get; set; }

        public string FireBaseToken { get; set; }
        public bool Rememberme { get; set; }

        public DateTime CurrentTime { get; set; }

        public UserForLogin()
        {
            this.CurrentTime = DateTime.Now;
        }
    }
}