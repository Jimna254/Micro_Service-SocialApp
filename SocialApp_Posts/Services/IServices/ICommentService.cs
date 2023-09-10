using SocialApp_Posts.Models.DTOs;

namespace SocialApp_Posts.Services.IServices
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetCommentsAsync(string PostID);
    }
}
