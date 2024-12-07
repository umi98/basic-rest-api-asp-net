using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace learn_api_c_sharp.DTOs.Account
{
    public class RegisterRequest
    {
        [Required]
        public string? Username {get; set;}
        [Required]
        [EmailAddress]
        public string Email {get; set;} = string.Empty;
        [Required]
        public string Password {get; set;} = "";
    }
}