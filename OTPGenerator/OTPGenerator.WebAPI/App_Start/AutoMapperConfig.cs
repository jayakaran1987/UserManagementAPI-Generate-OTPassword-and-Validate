using AutoMapper;
using OTPGenerator.Service.Model;
using OTPGenerator.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTPGenerator.WebAPI.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<UserModel, User>();
        }
    }
}