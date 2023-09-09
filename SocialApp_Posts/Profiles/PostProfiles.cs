using AutoMapper;
using SocialApp_Posts.Models;
using SocialApp_Posts.Models.DTOs;

namespace SocialApp_Posts.Profiles
{
    public class PostProfiles : Profile
    {

        public PostProfiles()
        {
            CreateMap<ResponseDTO, Post>().ReverseMap();
            CreateMap<PostRequestDTO, Post>().ReverseMap();    
        }
    }
}
