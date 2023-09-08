using System.ComponentModel.DataAnnotations;

namespace SocialAppAuthentication.Models
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
