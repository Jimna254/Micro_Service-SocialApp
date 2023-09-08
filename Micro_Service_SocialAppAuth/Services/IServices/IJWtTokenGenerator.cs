using Microsoft.AspNetCore.Identity;

namespace SocialAppAuthentication.Services.IServices
{
    public interface IJWtTokenGenerator
    {
        string GenerateToken( IdentityUser user , IEnumerable<string> roles);
    }
}
