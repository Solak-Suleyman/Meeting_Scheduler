using AutoMapper;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;

namespace WebApplication4.Models.Profiles
{
    public class UserMeetingProfile:Profile
    {
        public UserMeetingProfile()
        {
            {
                CreateMap<UserMeeting, UserMeetingDTO>();
                CreateMap<UserMeetingDTO, UserMeeting>();
            }
        }
    }
}
