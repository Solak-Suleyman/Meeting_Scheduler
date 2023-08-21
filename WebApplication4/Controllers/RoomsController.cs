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
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController:ControllerBase
    {
        private UnitOfWork<MeetingSchedulerContext> unitOfWork =new UnitOfWork<MeetingSchedulerContext>();
        private GenericRepository<Room> genericRepository;
        private RoomRepository roomRepository;
        private readonly IMapper _mapper;

        public RoomsController(IMapper mapper)
        {
            this.genericRepository = new GenericRepository<Room>(unitOfWork);
            this.roomRepository=new RoomRepository(unitOfWork);
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get_rooms()
        {
            var response=genericRepository.GetAll();
            return new JsonResult(response);
        }
        [HttpGet]
        [Route("/api/rooms/getroombyid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get_room([FromQuery]int id)
        {
            var response = genericRepository.GetById(id);
            return new JsonResult(response);
        }
        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var response = roomRepository.GetByName(name);
            return new JsonResult(response);
        }
        [HttpPost("AddRoom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddRoom(RoomDTO roomDTO)
        {
            unitOfWork.CreateTransaction();
            try
            {
                Room room=_mapper.Map<Room>(roomDTO);
                genericRepository.Insert(room);
                unitOfWork.Save();
                unitOfWork.Commit();
                return new JsonResult(room);                
            }
            catch (Exception )
            {
                unitOfWork.Rollback();
                return BadRequest();
            }    
        }





    }
}
