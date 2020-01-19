using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Api.Models;
using Cinema.Models.Models;
using Cinema.Models.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cinema.Api.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    //[EnableCors("MAGI")]
    [Consumes("application/json", "application/json-path+json", "multipart/form-data", "application/form-data")]

    public class MovieRoomController : ControllerBase
    {
        private readonly IGenericRepo<Seat> seatRepo;
        private readonly IMapper mapper;
        private readonly ILogger<MovieRoomController> logger;
        private readonly IMovieRoomRepo movieRoomRepo;

        public MovieRoomController( IGenericRepo<Seat> seatRepo, 
            IMapper mapper, ILogger<MovieRoomController> logger, IMovieRoomRepo movieRoomRepo)
        {
            this.seatRepo = seatRepo;
            this.mapper = mapper;
            this.logger = logger;
            this.movieRoomRepo = movieRoomRepo;
        }

        //GET: api/movierooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieRoomDTO>>> GetMovieRooms()
        {
            var movieRooms = await movieRoomRepo.GetAllMovieRooms();
            var movieRoomsDTO = mapper.Map<IEnumerable<MovieRoomDTO>>(movieRooms);
            return Ok(movieRoomsDTO);
        }
        
        [HttpGet]
        [Route("/api/[controller]/{movieroomID}")]
        public async Task<ActionResult<MovieRoomDTO>>GetMovieRoomDTO(Guid movieroomID)
        {
            var movieRoom = await movieRoomRepo.GetMovieRoom(movieroomID);
            var movieRoomDTO = mapper.Map<MovieRoomDTO>(movieRoom);
            return Ok(movieRoomDTO);
        }

        //GET: api/{movieRoom}/allseats
        [HttpGet]
        [Route("/api/[controller]/{theMovieRoomID}/allseats")]
        public async Task<ActionResult<IEnumerable<SeatDTO>>> GetSeats(Guid theMovieRoomID)
        {
            var seats = await movieRoomRepo.GetAllSeats(theMovieRoomID);
            var seatsDTO = mapper.Map<IEnumerable<SeatDTO>>(seats);

            return Ok(seatsDTO);

        }

        //GET: api/{movieRoom}/availableSeats
        [HttpGet]
        [Route("/api/[controller]/{theMovieRoomID}/availableSeats")]
        public async Task<ActionResult<IEnumerable<SeatDTO>>> GetAvailableSeats(Guid theMovieRoomID)
        {
            var seats = await movieRoomRepo.GetAvailableSeats(theMovieRoomID);
            var seatsDTO = mapper.Map<IEnumerable<SeatDTO>>(seats);

            return Ok(seatsDTO);

        }

        //GET: api/{movieRoom}/occupiedSeats
        [HttpGet]
        [Route("/api/[controller]/{theMovieRoomID}/occupiedSeats")]
        public async Task<ActionResult<IEnumerable<SeatDTO>>> GetOccupiedSeats(Guid theMovieRoomID)
        {
            var seats = await movieRoomRepo.GetOccupiedSeats(theMovieRoomID);
            Debug.Write(seats);
            var seatsDTO = mapper.Map<IEnumerable<SeatDTO>>(seats);

            return Ok(seatsDTO);

        }

        [HttpPost]
        [Authorize]
        [Route("/api/[controller]/orderReservation")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]

        public async Task<ActionResult> OrderReservation([Bind("MovieRoomID,SeatID,UserID")]
                [FromBody] Reservation reservation, [FromForm] IFormCollection formCollection)
        {

            if (reservation == null)
            {
                return BadRequest("Unsufficient data provided");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            try
            {
                Seat theSeat = new Seat();
                var seat = await seatRepo.GetByExpressionAsync(s => s.SeatID == reservation.SeatID);
                theSeat = seat.First();

                var movieroom = await movieRoomRepo.GetMovieRoom(reservation.MovieRoomID);

                reservation.Price = movieroom.Movie.Price;

                if (theSeat.Special == true)
                {
                    reservation.Price = reservation.Price + 4.5;
                }


                if (movieRoomRepo.ReservationExist(reservation))
                {
                    return NotFound();
                }

                await movieRoomRepo.PostReservation(reservation);
            }
            catch(Exception exc)
            {
                logger.LogError($"Threw exception when adding the reservation {reservation.MovieRoom.Movie.Name} room {reservation.MovieRoom.RoomID}: {exc}");
                throw new Exception($"Creating  the reservation {reservation.MovieRoom.Movie.Name} room {reservation.MovieRoom.RoomID} did not succeed.");
            }

            ReservationDTO reservationDTO = mapper.Map<ReservationDTO>(reservation);

            return Created($"api/{reservation.UserID}", reservationDTO);
        }

        [HttpPost]
        [Route("/api/[controller]/movieroom")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]

        public async Task<ActionResult> AddMovieRoom([Bind("Movie,Room,Date")]
        [FromBody] MovieRoomDTO movieRoomDTO, [FromForm] IFormCollection formCollection)
        {

            if (movieRoomDTO == null)
            {
                return BadRequest("Unsufficient data provided");
            }

            MovieRoom movieRoom = mapper.Map<MovieRoom>(movieRoomDTO);

            Debug.Write("de room id " + movieRoom.Room.RoomID + " de film id" + movieRoom.Movie.MovieID + "de date " + movieRoom.Date);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            if (movieRoomRepo.MovieRoomExist(movieRoom))
            {
                return NotFound();
            }

            try
            {
                logger.LogInformation($"Adding the movieroom {movieRoom.Movie} in room {movieRoom.Room}");
                await movieRoomRepo.AddMovieroomWithMovieAndRoom(movieRoom);
                await movieRoomRepo.SaveAsync();
            }
            catch (Exception exc)
            {
                logger.LogError($"Threw exception when adding the movieroom {movieRoom.Movie} in room {movieRoom.Room}: {exc}");
                throw new Exception($"Creating the movieroom {movieRoom.Movie} in room {movieRoom.Room} did not succeed.");
            }

            movieRoomDTO = mapper.Map<MovieRoomDTO>(movieRoom);

            return Created($"api/{movieRoomDTO.MovieRoomID}",movieRoomDTO);


        }

   

    }
    
}