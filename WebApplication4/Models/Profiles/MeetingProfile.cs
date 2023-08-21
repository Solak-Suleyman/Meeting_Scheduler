using AutoMapper;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;

namespace WebApplication4.Models.Profiles
{
    public class MeetingProfile:Profile
    {
        public MeetingProfile()
        {
            CreateMap<Meeting, MeetingDTO>();
            CreateMap<MeetingDTO, Meeting>();
        }
    }
}
