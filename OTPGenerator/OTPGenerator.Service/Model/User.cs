using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Service.Model
{
    public class User
    {
        // Model - User
        // can extend this model in case of scalability
        public string UserId { get; set; }
        public string OTPassword { get; set; }
        public DateTime? OTPCreatedDateTime { get; set; }
        public bool IsOTPasswordValid { get; set; }
    }
}
