﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialAppAuthentication.Models;

namespace SocialAppAuthentication.Auth_Profiles
{
    public class Auth_Profiles : Profile
    {
        public Auth_Profiles()
        {
            CreateMap<RegisterRequestDTO, IdentityUser>().ForMember(dest => dest.UserName, u => u.MapFrom(reg => reg.Email));
            CreateMap<IdentityUser, UserDTO>().ReverseMap();
        }

 
    }
}
