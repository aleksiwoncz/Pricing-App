using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Aleksandra_Iwończ_Pricing_App.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }

        [Required(ErrorMessage ="Username cannot be empty")]
        [Display(Name = "Username")] // label
        public string username { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")] // label
        public string pass { get; set; }

        //[Required(ErrorMessage = "Email cannot be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid")]
        [Display(Name = "Email")] // label
        public string email { get; set; }

    }
}
