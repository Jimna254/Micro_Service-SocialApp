using AutoMapper;
using SocialApp_Comments.Models;
using SocialApp_Comments.Models.DTOs;

namespace SocialApp_Comments.Profiles
{
    public class CommentProfiles : Profile
    {
        public CommentProfiles()
        {
            CreateMap<CommentsRequestDTO, Comment>().ReverseMap();

        }
       
    }
}
