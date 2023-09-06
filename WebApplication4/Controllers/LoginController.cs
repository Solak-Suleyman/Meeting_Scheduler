using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.Entity;
using WebApplication4.NonGenericRepository;
using WebApplication4.UnitOfWork;
using System.Text.Json;
using NuGet.Protocol;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("/api")]
    public class LoginController:ControllerBase
    {
        
        private IUnitOfWork<MeetingSchedulerContext> unitOfWork=new UnitOfWork<MeetingSchedulerContext>();
        private IUserRepository userRepository;
        public LoginController()
        {

            this.userRepository = new UserRepository(unitOfWork);
        }
        [HttpPost("/api/login")]
        public IActionResult Login( string user_name, string password)
        {
            try
            {
                if (user_name == null   || password == null) { return BadRequest("user_name cannot be null"); }

                var user = userRepository.GetByUserName(user_name);

                if (user == null) { return BadRequest("kullanıcı Bulunamadı"); }
                User response =user;
                //User user1 = JsonSerializer.Deserialize<User>(user);
                if(response != null) {
                    if (response.password.Equals(password))
                    {
                        return Ok("Kullanıcı Bulundu");
                    }
                    else
                    {
                        return BadRequest("Şifre veya Kullanıcı adı yanlış");
                    }
                }
                return BadRequest("Kullanıcı bulunamadı");
            
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
            
        }

    }
}
