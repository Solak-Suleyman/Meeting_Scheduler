using WebApplication4.Models.Entity;

namespace WebApplication4.NonGenericRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> ADDUser(User user);

    }
}
