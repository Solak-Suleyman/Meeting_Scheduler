using WebApplication4.Models.DTO;

namespace WebApplication4.NonGenericRepository
{
    public interface IAuthorizationRepository
    {
        AuthorizationResponse GenerateAuthorizationToken(string userId, string userName);
    }
}
