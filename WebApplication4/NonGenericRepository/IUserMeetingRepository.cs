using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;

namespace WebApplication4.NonGenericRepository
{
    public interface IUserMeetingRepository
    {
        void DeleteByMeetingID(int id);
       List<User> GetUsersBymeeting(DateTime startDate, DateTime endDate);
    }
}
