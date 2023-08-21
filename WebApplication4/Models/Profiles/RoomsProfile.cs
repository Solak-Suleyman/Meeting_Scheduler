using AutoMapper;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;

namespace WebApplication4.Models.Profiles
{
    public class RoomsProfile:Profile
    {
        public RoomsProfile()
        {
            CreateMap<Room,RoomDTO>();
            CreateMap<RoomDTO, Room>();
        }
    }
}
