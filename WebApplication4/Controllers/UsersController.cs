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
using Microsoft.AspNetCore.JsonPatch;
using NuGet.Protocol;

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

        //get user table
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
            return Ok(response.ToJson());
        }
        [HttpGet]
        [Route("/api/users/GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        //get by username
        public IActionResult Get([FromQuery] int id)
        {
            var response=genericRepository.GetById(id);
            return Ok(response);
        }
        //get by username
        [HttpGet]
        [Route("/api/users/GetByUserName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Getuser([FromQuery] string user_name)
        {
            if (user_name == null)
            {
                return BadRequest("UserName cannot be null");
            }
            var response = userRepository.GetByUserName(user_name);
            if(response != null) {
                return new JsonResult(response);    
            }
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        //add new user
        [HttpPost]
        [Route("/api/user/createUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public ActionResult AddUser(UsersDTO user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("user boş");
                }
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
            catch (Exception err)
            {

                unitOfWork.Rollback();
                return BadRequest(err);
            }
            return BadRequest("Girmedi");
        }
        //update existing user
        //[HttpPost]
        //[Route("/api/user/editUser")]
        //[ProducesResponseType(StatusCodes.Status200OK,Type = typeof(User))]
        //public ActionResult EditUser(UsersDTO user)
        //{
        //    unitOfWork.CreateTransaction();
        //    User userr= genericRepository.GetById(user.id);
        //    if (userr == null)
        //    {
        //        return BadRequest("Boş");      
        //    }
        //    User userr1 = _mapper.Map<User>(userr);
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            genericRepository.Update(userr1);
        //            unitOfWork.Commit();
        //            unitOfWork.Save();
        //            return Ok(userr1);
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        unitOfWork.Rollback();
        //        return BadRequest(ex);
        //    }
            
        //}
        [HttpPatch]
        [Route("/api/user/editUserpartial")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public IActionResult EditUserPartial(int id, [FromBody]JsonPatchDocument<User> patchDTO)
        {

            try
            {
                if (patchDTO != null || id != 0)
                {

                    User user = genericRepository.GetById(id);
                    if (user == null)
                    {
                        return BadRequest();
                    }


                    patchDTO.ApplyTo(user, ModelState);
                    if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                    unitOfWork.CreateTransaction();
                    genericRepository.Update(user);
                    unitOfWork.Commit();
                    unitOfWork.Save();
                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                return BadRequest();
            }
            



            }        
        [HttpDelete]
        [Route("/api/user/banUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void DeleteUser(int id)
        {

            unitOfWork.CreateTransaction();
            User user = genericRepository.GetById(id);
            try
            {
                if (ModelState.IsValid)
                {
                    genericRepository.Delete(user);
                    unitOfWork.Commit();
                    unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                unitOfWork.Rollback();
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
