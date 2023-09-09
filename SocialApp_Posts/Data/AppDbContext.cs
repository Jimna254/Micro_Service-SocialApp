using Microsoft.EntityFrameworkCore;
using SocialApp_Posts.Models;

namespace SocialApp_Posts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }

    }
}
