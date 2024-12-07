using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace learn_api_c_sharp.DTOs.Account
{
    public class SignInRequest
    {
        [Required]
        public string? Email { get; set;}
        [Required]
        public string Password { get; set;} = "";
    }
}