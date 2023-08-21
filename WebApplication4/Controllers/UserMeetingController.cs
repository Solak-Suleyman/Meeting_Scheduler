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
    [Route("api/userMeeting")]
    public class UserMeetingController:ControllerBase
    {
        private IUnitOfWork<MeetingSchedulerContext> unitOfWork=new UnitOfWork<MeetingSchedulerContext>();
        private GenericRepository<UserMeeting> genericRepository;
        private readonly IMapper _mapper;
        public UserMeetingController(IMapper mapper)
        {
            this.genericRepository = new GenericRepository<UserMeeting>(unitOfWork);
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUserMeeting()
        {
            try
            {
                var response = genericRepository.GetAll();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("/addUserMeeting")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddUserMeeting(UserMeetingDTO meetingDTO)
        {
            try
            {
                if (meetingDTO!= null)
                {
                    unitOfWork.CreateTransaction();
                    UserMeeting usrmtng = _mapper.Map<UserMeeting>(meetingDTO);
                    genericRepository.Insert(usrmtng);
                    unitOfWork.Commit();
                    unitOfWork.Save();
                    return Ok(usrmtng);
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




    }
}
