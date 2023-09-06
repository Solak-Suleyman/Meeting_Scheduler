namespace WebApplication4.Models.DTO
{
    public class GetTokenRequest
    {
        public string UserName { get; set; } = Consts.UserName;
        public string Password { get; set; } = Consts.Password;
    }
}
