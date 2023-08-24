using System.Data.Entity;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.Entity;
using WebApplication4.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.NonGenericRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork<MeetingSchedulerContext> unitOfWork):base(unitOfWork) { }
        public UserRepository(MeetingSchedulerContext context):base(context) { }


        public IEnumerable<User> GetByUserName(string user_name)
        {
            var response= Context.Users.Where(usr => usr.user_name == user_name).ToList();
            if (response.Count() == 0)
            {
                return null;
            }
            return response; 
        }
    }
}
