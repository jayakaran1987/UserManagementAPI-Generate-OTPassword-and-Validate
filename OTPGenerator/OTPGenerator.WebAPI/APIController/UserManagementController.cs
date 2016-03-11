using AutoMapper;
using OTPGenerator.Service.Model;
using OTPGenerator.Service.Service;
using OTPGenerator.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OTPGenerator.WebAPI.APIController
{
    public class UserManagementController : ApiBaseController
    {
        private readonly IUserManagementService userManagementService;

        // Used Dependency Injection here to intiate UserManagementService
        // I have used Unity and check UnityConfig 
        public UserManagementController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
        }


        [HttpGet]
        [Route("api/user/getOTPasswordForUser/{userId}")]
        public UserModel GetOTPasswordForUser(string userId)
        {
            try
            {
                //User Id validation handled in UserManagementService
                //If exception happens ErrorResponseView model returns the error code and message
                var returnUserModel = Mapper.Map<User, UserModel>(userManagementService.GetPasswordForUser(new User() { UserId = userId }));
                returnUserModel.Message = "OTPassword: " + returnUserModel.OTPassword + " is valid until 30 seconds";

                return returnUserModel;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.NotAcceptable, new ErrorResponseView(e).message));
            }
        }

        [HttpGet]
        [Route("api/user/validateOTPasswordForUser/{userId}/{password}")]
        public UserModel ValidateOTPasswordForUser(string userId, string password)
        {
            try
            {
                //User Id and OTPassword validation handled in UserManagementService
                var returnUserModel = Mapper.Map<User, UserModel>(userManagementService.ValidatePasswordForUser(new User() { UserId = userId, OTPassword = password }));
                if (!returnUserModel.IsOTPasswordValid)
                {
                    returnUserModel.Message = "OTPassword is Invalid";
                }
                else returnUserModel.Message = "OTPassword is Valid";


                return returnUserModel;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.NotAcceptable, new ErrorResponseView(e).message));
            }
        }
    }
}
