using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;
using WebApplication4.UnitOfWork;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("/api/register")]

    public class RegisterController : ControllerBase
    {
        private IUnitOfWork<MeetingSchedulerContext> unitOfWork = new UnitOfWork<MeetingSchedulerContext>();
        private GenericRepository<User> genericRepository;
        private IMapper _mapper;
        UsersController usersController;
        public RegisterController(IMapper mapper)
        {
            this.genericRepository = new GenericRepository<User>(unitOfWork);
            this._mapper = mapper;
        }
        [HttpPost]
        public ActionResult Register([FromBody] UsersDTO user)
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
                    User users = _mapper.Map<User>(user);
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
    }
}
