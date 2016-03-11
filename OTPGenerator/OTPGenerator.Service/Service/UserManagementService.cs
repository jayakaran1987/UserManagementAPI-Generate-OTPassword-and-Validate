using OTPGenerator.Service.Core;
using OTPGenerator.Service.CustomException;
using OTPGenerator.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Service.Service
{
    public class UserManagementService : IUserManagementService
    {
        // This method will return OTPassword for this particular UserId
        public User GetPasswordForUser(User user)
        {
            //Ensure UserId is not Null or Empty
            if (!String.IsNullOrEmpty(user.UserId))
            {
                try
                {
                    user.OTPassword = GenerateOTP.GeneratePassword(user.UserId);

                    // Just Added CreatedTime and IsValid for User
                    user.OTPCreatedDateTime = DateTime.Now;
                    user.IsOTPasswordValid = true;
                    return user;
                }
                catch (Exception e)
                {
                    throw new CustomCodeException(e.Message, ExceptionCode.UN_HANDLED);
                }
            }
            else 
            {
                // CustomCodeException with ExceptionCode
                throw new CustomCodeException("UserId cannot be null or empty", ExceptionCode.USERID_NULL_OR_EMPTY);
            }
            
        }

        // This method will return IsOTPasswordValid for this particular User
        public User ValidatePasswordForUser(User user)
        {
            if (!String.IsNullOrEmpty(user.UserId) && !String.IsNullOrEmpty(user.OTPassword))
            {
                // Concept behind is Generate the OTpassword again and compare the old Password and new Password
                // If this user generate password with in valid time then he will get the same password
                try
                {
                    var currentPassword = GenerateOTP.GeneratePassword(user.UserId);
                    //Compare both passwords
                    if (currentPassword.Equals(user.OTPassword))
                    {
                        user.IsOTPasswordValid = true;
                    }
                    else
                    {
                        //Password is Invalid
                        user.IsOTPasswordValid = false;
                    }

                    return user;
                }
                catch (Exception e)
                {
                    throw new CustomCodeException(e.Message, ExceptionCode.UN_HANDLED);
                }
                
            }
            else 
            {
                throw new CustomCodeException("UserId, OTPassword and OTPCreatedDateTime cannot be null or empty", ExceptionCode.INVALID_USER_DETAILS);
            }  
        }
    }
}
