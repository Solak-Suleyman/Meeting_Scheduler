using WebApplication4.Models.Entity;

namespace WebApplication4.NonGenericRepository
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetByName(string name);
    }
}
