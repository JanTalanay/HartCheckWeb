﻿using System.ComponentModel.DataAnnotations;

namespace HartCheck_Admin.Models
{
    public class LoginViewModel

    {
        public LoginViewModel()
        {
            EmailAddress = "";
            Password = "";
            ReturnUrl = "";
        }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
