using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;
using WebApplication4.NonGenericRepository;
using WebApplication4.UnitOfWork;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/userMeeting")]
    public class UserMeetingController : ControllerBase
    {
        private IUnitOfWork<MeetingSchedulerContext> unitOfWork = new UnitOfWork<MeetingSchedulerContext>();
        private GenericRepository<UserMeeting> genericRepository;
        private readonly IMapper _mapper;
        private UserMeetingRepository _userMeetingRepository;
        public UserMeetingController(IMapper mapper)
        {
            this.genericRepository = new GenericRepository<UserMeeting>(unitOfWork);
            this._mapper = mapper;
            this._userMeetingRepository = new UserMeetingRepository(unitOfWork);
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
        public IActionResult AddUserMeeting(List<int> users, int meetingId)
        {
            try
            {
                foreach (var userId in users)
                {
                    if (meetingId != null && userId != null)
                    {
                        UserMeetingDTO meetingDTO = new UserMeetingDTO() { UserId = userId, MeetingId = meetingId };

                        unitOfWork.CreateTransaction();
                        UserMeeting usrmtng = _mapper.Map<UserMeeting>(meetingDTO);
                        genericRepository.Insert(usrmtng);
                        unitOfWork.Commit();
                        unitOfWork.Save();

                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return Ok();

            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                return BadRequest("Rollbacked");
            }
        }
        [HttpDelete("/deleteUserMeeting")]
        public void DeleteByMeetingId(int meetingId)
        {
            _userMeetingRepository.DeleteByMeetingID(meetingId);
        }
        [HttpGet("/getByMeeting")]

        public ActionResult GetByMeeting(DateTime startDate, DateTime endDate)
        {
            try
            {
                unitOfWork.CreateTransaction();
                var response =_userMeetingRepository.GetUsersBymeeting(startDate, endDate);
                if(response == null) { return BadRequest(); }
                Console.WriteLine(response);
                unitOfWork.Commit();
                unitOfWork.Save();
                return Ok(response);
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                return BadRequest(ex.Message);
                
            }
        }



    }
}
