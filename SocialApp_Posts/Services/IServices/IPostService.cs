using SocialApp_Posts.Models;

namespace SocialApp_Posts.Services.IServices
{
    public interface IPostService
    {
        // Get all posts
        Task<IEnumerable<Post>> GetAllPostsAsync();
        //Get post by id
        Task<Post> GetPostByIdAsync(Guid postId);
        //Get posts by user id
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId);
        //Post Creation
        Task<string> CreatePostAsync(Post post);
        //Updation
        Task<string> UpdatePostAsync(Post post);
        //Deletion
        Task<string> DeletePostAsync(Post post);
    }
}
