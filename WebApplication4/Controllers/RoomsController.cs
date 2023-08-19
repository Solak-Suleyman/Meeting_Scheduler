using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.GenericRepository;
using WebApplication4.Models.Context;
using WebApplication4.Models.Entity;
using WebApplication4.NonGenericRepository;
using WebApplication4.UnitOfWork;

namespace WebApplication4.Controllers
{
    public class RoomsController
    {
        private IUnitOfWork<MeetingSchedulerContext> unitOfWork =new UnitOfWork<MeetingSchedulerContext>();
        private GenericRepository<Room> genericRepository;
        private readonly IMapper _mapper;

        public RoomsController(IMapper mapper)
        {
            this.genericRepository = new GenericRepository<Room>(unitOfWork);
            this._mapper = mapper;
        }
       

    }
}
