using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Api.Models;
using Cinema.Models.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cinema.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("MAGI")]
    [Consumes("application/json", "application/json-path+json", "multipart/form-data", "application/form-data")]

    public class RoomController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<RoomController> logger;
        private readonly IMovieRoomRepo movieRoomRepo;

        public RoomController( IMapper mapper, 
            ILogger<RoomController> logger, IMovieRoomRepo movieRoomRepo)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.movieRoomRepo = movieRoomRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            var rooms = await movieRoomRepo.GetRooms();
            var roomsDTO = mapper.Map<IEnumerable<RoomDTO>>(rooms);

            return Ok(roomsDTO);
        }

        [HttpGet]
        [Route("/api/[controller]/{movieID}")]
        public async Task<ActionResult<IQueryable<RoomDTO>>> GetRoomsWithDatesOfMovie(Guid movieID)
        {
            var rooms = await movieRoomRepo.GetRoomWithHours(movieID);
            var roomsDTO = mapper.Map<IEnumerable<RoomDTO>>(rooms);

            return Ok(roomsDTO);
        }
    }
}