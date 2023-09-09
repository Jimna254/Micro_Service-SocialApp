using Microsoft.EntityFrameworkCore;
using SocialApp_Comments.Data.SocialApp_Posts.Data;
using SocialApp_Comments.Models;
using SocialApp_Comments.Services.IServices;

namespace SocialApp_Comments.Services
{
    public class CommentsService : ICommentsService
    {
        // Inject DbContext
        private readonly AppDbContext _context;

        public CommentsService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<string> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return "Comment Created successfully";
        }

        public async Task<string> DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return "Comment Removed Successfully";
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            return await _context.Comments.Where(x => x.CommentId == commentId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            return await _context.Comments.Where(x => x.PostId == postId).ToListAsync();
        }
        public async Task<string> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return "Comment Updated Successfully";
        }
    }
}
