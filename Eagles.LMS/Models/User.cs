using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Eagles.LMS.Models
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        [Required, MinLength(9)]
        public string Mobile { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedTime { get; set; }

        public UserTybe UserTybe { get; set; }

        public AccountState AccountState { get; set; }

        public string FireBaseToken { get; set; }
        [Required]
        public string FullName { get; set; }
        public string latitude { get; set; }
        public string altitude { get; set; }
        public string Image { get; set; }
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        public bool IsDeleted { get; set; }
        public string OTP { get; set; }
        public string OTP_Provider { get; set; }
        public DateTime OTPTIME { get; set; }

        public DateTime? LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }





    }
}