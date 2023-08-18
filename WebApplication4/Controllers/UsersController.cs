using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using WebApplication4.Models;
using WebApplication4.Models.Context;
using WebApplication4.Models.Entity;
using WebApplication4.Models.DTO;
using WebApplication4.GenericRepository;
using WebApplication4.UnitOfWork;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace WebApplication4.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUnitOfWork<MeetingSchedulerContext>unitOfWork=new UnitOfWork<MeetingSchedulerContext>();
        private GenericRepository<User> genericRepository;
        public UsersController()
        {
            this.genericRepository = new GenericRepository<User>(unitOfWork);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public IActionResult Get_users(int id)
        {
            //try
            //{
            //    IEnumerable<UsersDTO> data = _db.GetUsers();
            //    if (!data.Any())
            //    {
            //        return NotFound();
            //    }
            //    return Ok(data);
            //}
            //catch (Exception)
            //{
            //    return BadRequest();

            //}
            var response = genericRepository.GetAll();
            return new JsonResult(response);
        }
        [HttpGet]
        [Route("/api/users/GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public IActionResult Get(int id)
        {
            var response=genericRepository.GetById(id);
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("/api/user/createUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public ActionResult AddUser(UsersDTO user)
        {
            try
            {

                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    User users = new User
                    {
                        id = user.id,
                        user_name = user.user_name,
                        name = user.name,
                        surname = user.surname,
                        password = user.password,
                        status = user.status,
                        created_time = DateTime.UtcNow,
                        updated_time = DateTime.UtcNow
                    };
                    genericRepository.Insert(users);
                    unitOfWork.Save();
                    unitOfWork.Commit();
                    return Ok(users);
                }
            }
            catch (Exception)
            {

                unitOfWork.Rollback();
            }
            return Ok();
        }

        //    public IActionResult Get(int id) {
        //        try
        //        {
        //            UsersDTO data = _db.GetUserById(id);
        //            if (data==null)
        //            {
        //                return NotFound();
        //            }
        //            return Ok(data);
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest();

        //        }
        //    }
        //    [HttpGet]
        //    [Route("api/[controller]/GetUserByUserName")]
        //    public IActionResult GetByUName(string user_name)
        //    {
        //        try
        //        {
        //            UsersDTO data = _db.GetUserByUserName(user_name);
        //            if (data==null)
        //            {
        //                return NotFound();
        //            }
        //            return Ok(data);
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    [HttpPost]
        //    [Route("api/[controller]/CreateUser")]
        //    public IActionResult CreateUser([FromBody] UsersDTO data)
        //    {
        //        try
        //        {
        //            _db.AddUser(data);
        //            return Ok(data);
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest(data);

        //        }
        //    }
        //}
    }
}
