using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.Entity;
using WebApplication4.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace WebApplication4.NonGenericRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork<MeetingSchedulerContext> unitOfWork) : base(unitOfWork) { }
        public UserRepository(MeetingSchedulerContext context) : base(context) { }

        public IEnumerable<User> GetByMeeting(DateTime to_date, DateTime end_date)
        {
            return null;
        }

        public IEnumerable<User> GetByUserName(string user_name)
        {
            var response = Context.Users.Where(b => b.user_name == user_name)
                .Include(b => b.UserMeeting)
                .ToList();
            if (response.Count() == 0)
            {
                return null;
            }
            return response;
        }
    }
}
