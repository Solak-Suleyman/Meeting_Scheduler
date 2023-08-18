using AutoMapper;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;

namespace WebApplication4.Models.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile() {
            CreateMap<UsersDTO, User>();
            CreateMap<User,UsersDTO>();
        }
    }
}
