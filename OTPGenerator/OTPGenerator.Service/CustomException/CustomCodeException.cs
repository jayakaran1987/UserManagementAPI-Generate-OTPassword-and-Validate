using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Service.CustomException
{
    public class CustomCodeException: Exception
    {
         /// The error code. 
         /// Comes from ExceptionCode
        public int code { get; set; }

        public CustomCodeException(String message, int code)
            : base(message)
        {
            this.code = code;
        }
    }
}
