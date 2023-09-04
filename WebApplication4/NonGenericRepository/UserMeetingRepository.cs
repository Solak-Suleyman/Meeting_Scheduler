//using System.Data.Entity;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.Entity;
using WebApplication4.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NuGet.Protocol;

namespace WebApplication4.NonGenericRepository
{
    public class UserMeetingRepository : GenericRepository<UserMeeting>, IUserMeetingRepository
    {
        IUnitOfWork<MeetingSchedulerContext> _unitOfWork;
        public UserMeetingRepository(IUnitOfWork<MeetingSchedulerContext> unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteByMeetingID(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    NullReferenceException nullReferenceException = new NullReferenceException();
                    throw nullReferenceException;
                }
                _unitOfWork.CreateTransaction();
                var list = Context.UserMeetings.Where(b => b.MeetingId == id).ToList();
                foreach (var item in list)
                {
                    Context.Remove(item);
                }
                _unitOfWork.Commit();
                _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _unitOfWork.Rollback();
            }

        }

        public IEnumerable<User> GetUsersBymeeting(DateTime startDate, DateTime endDate)
        {
            var response = Context.Users.Include(user => user.UserMeeting.Where(usrmtng => ((usrmtng.Meeting.from_date > startDate && usrmtng.Meeting.from_date > endDate) || (usrmtng.Meeting.to_date < startDate && usrmtng.Meeting.to_date < endDate)))).ToList();//.ThenInclude(userMeeting=>userMeeting.UserId);
            Console.WriteLine(response);
            return response;
        }
    }
}
