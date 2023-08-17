using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using WebApplication4.Models;
using WebApplication4.Models.Context;
using WebApplication4.Models.Entity;
using WebApplication4.Models.DTO;


namespace WebApplication4.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly DBhelper _db;
        public UsersController(MeetingSchedulerContext context)
        {
            _db = new DBhelper(context);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public IActionResult Get_users()
        { 
            try
            {
                IEnumerable<UsersDTO> data = _db.GetUsers();
                if (!data.Any())
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();

            }
        }
        [HttpGet]
        [Route("api/[controller]/GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public IActionResult Get(int id) {
            try
            {
                UsersDTO data = _db.GetUserById(id);
                if (data==null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
                
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetUserByUserName")]
        public IActionResult GetByUName(string user_name)
        {
            try
            {
                UsersDTO data = _db.GetUserByUserName(user_name);
                if (data==null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/[controller]/CreateUser")]
        public IActionResult CreateUser([FromBody] UsersDTO data)
        {
            try
            {
                _db.AddUser(data);
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest(data);
                
            }
        }
    }
}
