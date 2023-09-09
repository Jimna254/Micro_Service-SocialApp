using Microsoft.EntityFrameworkCore;
using SocialApp_EmailService.Data;
using SocialApp_EmailService.Models;

namespace SocialApp_EmailService.Services
{
    public class EmailService
    {
        private DbContextOptions<AppDbContext> options;
        public EmailService(DbContextOptions<AppDbContext> options)
        {
            this.options = options;
        }

        public async Task SaveData(EmailLoggers emailLoggers)
        {
            //create _context

            var _context = new AppDbContext(this.options);
            _context.EmailLoggers.Add(emailLoggers);
            await _context.SaveChangesAsync();
        }
    }
}
