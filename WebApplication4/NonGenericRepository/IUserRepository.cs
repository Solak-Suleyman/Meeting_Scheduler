using WebApplication4.Models.Entity;

namespace WebApplication4.NonGenericRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetByUserName(string user_name);
        IEnumerable<User> GetByMeeting(DateTime to_date,DateTime end_date);

    }
}
