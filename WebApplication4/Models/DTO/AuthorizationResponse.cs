namespace WebApplication4.Models.DTO
{
    public class AuthorizationResponse
    {
        public string UserId { get; set; }

        public string AuthorizationToken { get; set; }

        public string RefreshToken { get; set; }
    }
}