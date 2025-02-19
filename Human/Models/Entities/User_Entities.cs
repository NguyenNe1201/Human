using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Human.Models.Entities
{
    public class User_Entities
    {
        [Required(ErrorMessage = "Please enter username or email.")]
        public string USERNAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string USERFULLNAME { get; set; }
        public string PASSWORD { get; set; }
        public string OTP { get; set; }
        public int ROLE_ID { get; set; }
        public int DIVISION_ID { get; set; }
    }
}