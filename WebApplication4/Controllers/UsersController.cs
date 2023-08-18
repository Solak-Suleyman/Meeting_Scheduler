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
using AutoMapper;
using WebApplication4.NonGenericRepository;

namespace WebApplication4.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUnitOfWork<MeetingSchedulerContext>unitOfWork=new UnitOfWork<MeetingSchedulerContext>();
        private GenericRepository<User> genericRepository;
        private IUserRepository userRepository;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper)
        {
            this.genericRepository = new GenericRepository<User>(unitOfWork);
            this.userRepository=new UserRepository(unitOfWork);
            this._mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public IActionResult Get_users()
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
        public IActionResult Get([FromBody] int id)
        {
            var response=genericRepository.GetById(id);
            return new JsonResult(response);
        }

        [HttpGet]
        [Route("/api/users/GetByUserName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public IActionResult Getuser([FromQuery] string user_name)
        {
            var response = userRepository.GetByUserName(user_name);
            return Ok(response);
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
                    User users=_mapper.Map<User>(user);
                    //User users = new User
                    //{
                    //    id = user.id,
                    //    user_name = user.user_name,
                    //    name = user.name,
                    //    surname = user.surname,
                    //    password = user.password,
                    //    status = user.status,
                    //    created_time = DateTime.UtcNow,
                    //    updated_time = DateTime.UtcNow
                    //};
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
        [HttpPost]
        [Route("/api/user/editUser")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(User))]
        public ActionResult EditUser(UsersDTO user)
        {
            User userr= genericRepository.GetById(user.id);
            User user1 = _mapper.Map<User>(userr);
            if (ModelState.IsValid)
            {
                genericRepository.Update(user1);
                unitOfWork.Commit();
                unitOfWork.Save();
                return Ok(user1);
            }
            else
            {
                return BadRequest();
            }
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
