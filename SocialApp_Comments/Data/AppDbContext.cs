using Microsoft.EntityFrameworkCore;
using SocialApp_Comments.Models;

namespace SocialApp_Comments.Data
{
    
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
           public DbSet<Comment> Comments { get; set; }         
        }
}
