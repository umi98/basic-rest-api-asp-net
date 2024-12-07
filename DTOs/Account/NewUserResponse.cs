using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_api_c_sharp.DTOs.Account
{
    public class NewUserResponse
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}