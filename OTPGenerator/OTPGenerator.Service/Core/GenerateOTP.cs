using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Service.Core
{
    public static class GenerateOTP
    {
        //Note - I have referred following sites to generate OTP
        // Time-based One-time Password Algorithm Ref https://en.wikipedia.org/wiki/Time-based_One-time_Password_Algorithm
        //Ref http://tools.ietf.org/html/rfc4226#section-5.4
        // Concept behind is, there are two factors one is time counter and userId
        // The time counter wil be changed if time is exceeded the timelimit
        // This means that the clock on both the client and the server need to be kept in sync with each other


        private static readonly DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly int digits = 6;
        private static readonly int validTime = 30; //InSeconds

        public static string GeneratePassword(string userID)
        {
            // Calculate the number of slots for the validTime duration
            // Same value will be returned with in this validTime
            // Same password will be generated with in this Time for same user
            long timeDurationNumber = (long)(DateTime.UtcNow - startTime).TotalSeconds / validTime;

            //Here the system converts the timeDurationNumber to a byte[]
            byte[] timeDurationNumberByte = BitConverter.GetBytes(timeDurationNumber);

            //To BigEndian (MSB LSB)
            if (BitConverter.IsLittleEndian) Array.Reverse(timeDurationNumberByte);

           
            
            //Hash the userId by HMAC-SHA-1 (Hashed Message Authentication Code)

            byte[] userIdInByte = Encoding.ASCII.GetBytes(userID);
            HMACSHA1 userIdHMAC = new HMACSHA1(userIdInByte, true);

            //Hashing a message with time Duration byte
            byte[] hash = userIdHMAC.ComputeHash(timeDurationNumberByte);


            //RFC4226 

            int offset = hash[hash.Length - 1] & 0xf; //0xf = 15d

            int binary =

            ((hash[offset] & 0x7f) << 24)      //0x7f = 127d

            | ((hash[offset + 1] & 0xff) << 16) //0xff = 255d

            | ((hash[offset + 2] & 0xff) << 8)

            | (hash[offset + 3] & 0xff);

            // Shrink: 6 digits
            int password = binary % (int)Math.Pow(10, digits); 

            return password.ToString(new string('0', digits));

        }
    }
}
