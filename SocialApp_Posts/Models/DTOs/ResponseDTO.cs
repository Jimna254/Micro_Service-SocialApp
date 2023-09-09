namespace SocialApp_Posts.Models.DTOs
{
    public class ResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = true;
        public object? Data { get; set; }
    }
}
