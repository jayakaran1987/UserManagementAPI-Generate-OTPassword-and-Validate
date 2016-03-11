using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Service.CustomException
{
    public static class ExceptionCode
    {
        //UserId cannot be Null or Empty
        public static int USERID_NULL_OR_EMPTY = 100;

        //OTPassword or UserId Null or Empty
        public static int INVALID_USER_DETAILS = 101;

        //UN_HANDLED
        public static int UN_HANDLED = 102;
    }
}
