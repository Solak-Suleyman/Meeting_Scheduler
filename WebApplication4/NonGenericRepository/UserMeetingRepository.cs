//using System.Data.Entity;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.Entity;
using WebApplication4.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NuGet.Protocol;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        public List<User> GetUsersBymeeting(DateTime startDate, DateTime endDate)
        {
            //var response = Context.Users.Include(user => user.UserMeeting.Where(usrmtng => ((usrmtng.Meeting.from_date > startDate && usrmtng.Meeting.from_date > endDate) || (usrmtng.Meeting.to_date < startDate && usrmtng.Meeting.to_date < endDate)))).ToList();//.ThenInclude(userMeeting=>userMeeting.UserId);
            //var responsee= from user in Context.Users
            //              join meeting in Context.UserMeetings
            //                on new {Id=Meeting.UserMeeting}
            //                equals new {Id=User.UserMeeting}
            //                select new {user,meeting};
            //Console.WriteLine("noluyo");
            var response1 = Context.UserMeetings.Include(usrmeeting => usrmeeting.Meeting).Where(usrmtng => ((usrmtng.Meeting.from_date > startDate && usrmtng.Meeting.from_date > endDate) || (usrmtng.Meeting.to_date < startDate && usrmtng.Meeting.to_date < endDate))).ToList();
            var q = (from user in Context.Users 
                     join um in Context.UserMeetings on user.id equals um.UserId
                     join me in Context.Meetings on um.MeetingId equals me.id 
                     where ((me.from_date > startDate && me.from_date > endDate) || (me.to_date < startDate && me.to_date < endDate))

                     group user by user.id into groupedUsers // Group by user.id
                     orderby groupedUsers.Key
                     select new User()
                     {
                         id = groupedUsers.Key, // The key is the user.id
                         user_name = groupedUsers.First().user_name, // You can pick any user_name from the group
                         name = groupedUsers.First().name, // Similarly, pick name from the group
                         surname = groupedUsers.First().surname, // Pick surname from the group
                         created_time = groupedUsers.First().created_time, // Pick created_time from the group
                         updated_time = groupedUsers.First().updated_time // Pick updated_time from the group
                     }
    ).ToList();
            //var z=Context.Users.Join(Context.UserMeetings,
            //    usr=>usr.id,
            //    cusrmtng=>cusrmtng.UserId,
            //    (usr, usrmtng) => new {usr,usrmtng}).Join(Context.Meetings,
            //    usrmtng=>usrmtng.usrmtng.MeetingId,
            //    mtng=>mtng.id,
            //    (cusrmtng, mtng) => new User{ id=cusrmtng.usr.id}
            //    ).Where(full=>full.mtng.from_date>endDate).ToList();

            return q ;
        }
    }
}
