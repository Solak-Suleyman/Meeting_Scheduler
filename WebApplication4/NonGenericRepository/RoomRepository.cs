using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.Entity;
using WebApplication4.UnitOfWork;

namespace WebApplication4.NonGenericRepository
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(UnitOfWork<MeetingSchedulerContext> unitOfWork) : base(unitOfWork)
        {
        }
        public RoomRepository(MeetingSchedulerContext context) : base(context) { }



        public IEnumerable<Room> GetByName(string name)
        {
            return Context.Roooms.Where(rom=>rom.name == name).ToList();
        }
    }
}
