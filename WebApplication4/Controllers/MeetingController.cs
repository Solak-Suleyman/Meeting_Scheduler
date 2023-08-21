using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.Data.Entity;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;
using WebApplication4.UnitOfWork;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/meetings")]
    public class MeetingController : ControllerBase
    {
        private IUnitOfWork<MeetingSchedulerContext> unitOfWork = new UnitOfWork<MeetingSchedulerContext>();
        private GenericRepository<Meeting> genericRepository;
        private readonly IMapper _mapper;
        public MeetingController(IMapper mapper)
        {
            this.genericRepository = new GenericRepository<Meeting>(unitOfWork);
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(Meeting), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Meeting), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(Meeting), StatusCodes.Status404NotFound)]
        public IActionResult GetMeetings()
        {
            try
            {
                var response = genericRepository.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("addmeeting")]
        [ProducesResponseType(typeof(MeetingDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult addMeeting(MeetingDTO meetingDTO)
        {
            try
            {
                if (meetingDTO != null)
                {

                    unitOfWork.CreateTransaction();
                    Meeting meeting = _mapper.Map<Meeting>(meetingDTO);
                    genericRepository.Insert(meeting);
                    unitOfWork.Save();
                    unitOfWork.Commit();
                    return Ok();

                }
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                return BadRequest(ex);

            }
            return BadRequest();
        }
        [HttpPatch("/editMeeting")]
        public ActionResult EditMeeting(int id, JsonPatchDocument<Meeting> patchDTO) {
            try
            {
                unitOfWork.CreateTransaction();

                Meeting meeting = genericRepository.GetById(id);

                if (meeting == null)
                {
                    return BadRequest();
                }
                patchDTO.ApplyTo(meeting, ModelState);
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
        [HttpDelete("/deleteMeeting")]
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
                throw;
            }
        }


    }

}
