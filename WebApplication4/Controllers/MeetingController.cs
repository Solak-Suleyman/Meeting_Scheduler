using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;

using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;
using WebApplication4.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/meetings")]
    public class MeetingController : ControllerBase
    {
        private UnitOfWork<MeetingSchedulerContext> unitOfWork = new UnitOfWork<MeetingSchedulerContext>();
        private GenericRepository<Meeting> genericRepository;
        private readonly IMapper _mapper;
        private UserMeetingController UserMeetingController { get; set; }
        public MeetingController(IMapper mapper)
        {
            this.genericRepository = new GenericRepository<Meeting>(unitOfWork);
            this._mapper = mapper;
            this.UserMeetingController = new UserMeetingController(mapper);
        }
        [HttpGet("GetMeetingById")]
        [ProducesResponseType(typeof(Meeting), StatusCodes.Status200OK)]
        public IActionResult GetMeetingById(int id)
        {
            try
            {
                //var response =genericRepository.GetById(id);
                var response=genericRepository.Context.Meetings
                    .Include(b=>b.UserMeeting).ThenInclude(b=>b.User)
                    .Include(b=>b.Room).Where(b=>b.id==id)
                    .ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(Meeting), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Meeting), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(Meeting), StatusCodes.Status404NotFound)]
        public IActionResult GetMeetings()
        {
            try
            {
                var response = genericRepository.Context.Meetings
                .Include(b => b.UserMeeting)
                .Include(b => b.Room)
                .ToList();
                //var response = genericRepository.GetAll();
                return Ok(response/*.ToJson()*/);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("addmeeting")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult addMeeting(MeetingDTO meetingDTO)
        {
            try
            {
                if (meetingDTO != null)
                {


                    Meeting meeting = _mapper.Map<Meeting>(meetingDTO);


                    unitOfWork.CreateTransaction();
                    genericRepository.Insert(meeting);
                    unitOfWork.Save();
                    unitOfWork.Commit();
                    Console.WriteLine(meeting.id);
                    UserMeetingController.AddUserMeeting(meetingDTO.UserIds, meeting.id);

                    return Ok(/*usrmntng*/);

                }
                else
                {
                    return BadRequest("meeting voş olamaz");
                }
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                return BadRequest(ex);

            }
            return BadRequest();
        }
        [HttpPatch("editMeetingPartial")]
        public ActionResult EditMeetingPartial(int id, JsonPatchDocument<Meeting> patchDTO)
        {
            try
            {
                unitOfWork.CreateTransaction();

                Meeting meeting = genericRepository.GetById(id);

                if (meeting == null)
                {
                    return BadRequest();
                }
                //apply patch to meeting
                patchDTO.ApplyTo(meeting, ModelState);
                //
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                genericRepository.Update(meeting);
                unitOfWork.Commit();
                unitOfWork.Save();
                return Ok();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                return BadRequest();
            }
        }
        [HttpPost("editMeeting")]
        public ActionResult EditMeeting(MeetingDTO meetingDTO)
        {
            try
            {
                if (meetingDTO == null)
                {
                    return BadRequest("Fields cannot be null");
                }
                unitOfWork.CreateTransaction();
                //Meeting meeting = genericRepository.GetById(meetingDTO.id);
                //if(meeting == null) { return BadRequest("Meeting Cannot Found"); }
                Meeting meeting1 = _mapper.Map<Meeting>(meetingDTO);
                genericRepository.Update(meeting1);
                unitOfWork.Commit();
                unitOfWork.Save();
                UserMeetingController.DeleteByMeetingId(meeting1.id);
                UserMeetingController.AddUserMeeting(meetingDTO.UserIds, meeting1.id);
                return Ok();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                return BadRequest(ex);

            }
        }
        [HttpDelete("deleteMeeting")]
        [Authorize]
        public ActionResult DeleteMeeting(int id)
        {

            try
            {
                unitOfWork.CreateTransaction();
                Meeting meeting = genericRepository.GetById(id);
                if (meeting == null) { return BadRequest(); }
                genericRepository.Delete(meeting);
                unitOfWork.Commit();
                unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }


    }

}
