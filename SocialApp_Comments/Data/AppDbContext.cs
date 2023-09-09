using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialApp_Comments.Models;

namespace SocialApp_Comments.Data
{
    namespace SocialApp_Posts.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }
           public DbSet<Comment> Comments { get; set; }         

        }
    }
}
