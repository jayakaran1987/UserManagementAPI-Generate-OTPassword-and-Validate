using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTPGenerator.WebAPI.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string OTPassword { get; set; }
        public bool IsOTPasswordValid { get; set; }
        public string Message { get; set; }
    }
}