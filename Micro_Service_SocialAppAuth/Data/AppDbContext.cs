using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialAppAuthentication.Data
{
    public class AppDbContext : IdentityDbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
       
    }
}
