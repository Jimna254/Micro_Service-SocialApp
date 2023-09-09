using SocialApp_Posts.Data;
using SocialApp_Posts.Models.DTOs;
using SocialApp_Posts.Services.IServices;

namespace SocialApp_Posts.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public Task<IEnumerable<CommentDTO>> GetCommentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
