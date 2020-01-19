using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Api.Hubs;
using Cinema.Api.Models;
using Cinema.Models.Models;
using Cinema.Models.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Cinema.Api.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/{version:apiVersion}/[controller]")]
    //[EnableCors("MAGI")]
    [ApiController]
    [Consumes("application/json", "application/json-path+json", "multipart/form-data", "application/form-data")]

        public class MovieController : ControllerBase
        {
            private readonly IMovieRepo movieRepo;
            private readonly IGenericRepo<Genre> genreRepo;
            private readonly IMapper mapper;
            private readonly ILogger<MovieController> logger;
            private readonly IHubContext<RepoHub> hubContext;

            public MovieController(IMovieRepo movieRepo, 
                IGenericRepo<Genre> genreRepo, IMapper mapper,
                ILogger<MovieController> logger, IHubContext<RepoHub> hubContext)
            {
                this.movieRepo = movieRepo;
                this.genreRepo = genreRepo;
                this.mapper = mapper;
                this.logger = logger;
                this.hubContext = hubContext;
            }

            // GET: api/Movies
            [HttpGet(Name = "GetMovies")]
            //[Produces("application/xml")]
            public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovie()
            {

                var movies = await  movieRepo.GetAllMoviesAsync();

                var moviesDTO = mapper.Map<IEnumerable<MovieDTO>>(movies);
       
                return Ok(moviesDTO);

            }

        // GET: api/Movies
        [HttpGet]
        [Route("/api/[controller]/MoviesWithMovieRoom")]

        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovieWithMovieRoom()
        {

            var movies = await movieRepo.GetAllMoviesWithMovieRoomAsync();

            var moviesDTO = mapper.Map<IEnumerable<MovieDTO>>(movies);

            return Ok(moviesDTO);

        }

        [HttpPost]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status409Conflict)]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
            public async Task<ActionResult> AddMovie(
            [Bind("Name,Description,Duration,Year, Price,GenreName, Image")]
                [FromBody] MovieDTO movieDTO, [FromForm] IFormCollection formCollection)
            {
                if (movieDTO == null)
                {
                    return BadRequest("Unsufficient data provided");
                }

                 Movie movie = mapper.Map<Movie>(movieDTO);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); //400
                }

                if (movieRepo.MovieExist(movie))
                    {
                        return NotFound();
                    }

            try
            {
                logger.LogInformation($"Adding the movie {movie.Name}");
                movieRepo.AddMovieWithGenre(movie);
                await movieRepo.Create(movie);
                await movieRepo.SaveAsync();
                await hubContext.Clients.All.SendAsync("ServerMessage", movieDTO);
            }
            catch(Exception exc)
            {
                Log.Logger.Warning($"Threw exception when adding the movie {movie.Name}: {exc}");

                logger.LogError($"Threw exception when adding the movie {movie.Name}: {exc}");
                throw new Exception($"Creating  the movie {movie.Name} did not succeed.");
            }

            movieDTO = mapper.Map<MovieDTO>(movie);

            return Created($"api/{movieDTO.MovieID}", movieDTO);

            }

            //GET: api/Movies/Today
            [HttpGet]
            [Route("/api/[controller]/Movies/Today")]
            public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovieToday()

            {
                var movies = await movieRepo.GetMoviesToday();
                foreach (Movie r in movies)
                {
                    var genres = await genreRepo.GetByExpressionAsync(c => c.GenreID == r.GenreID);
                    r.Genre = genres.First();
                }

                var moviesDTO = mapper.Map<IEnumerable<MovieDTO>>(movies);

                return Ok(moviesDTO); //data met Links informatie

            }

            //GET: api/{movieId}
            [HttpGet]
            [Route("/api/[controller]/{theMovieID}")]
            public async Task<ActionResult<Movie>> GetMovie(Guid theMovieID)
            {
                var movie = await movieRepo.GetMovie(theMovieID);
                var movieDTO = mapper.Map<MovieDTO>(movie);   

                return Ok(movieDTO);

            }

                //GET: api/{movieId}/{limit}
                [HttpGet]
                [Route("/api/[controller]/{theMovieID}&limit={limit}")]
                public async Task<ActionResult<Movie>> GetMovieWithLimit(Guid theMovieID, int limit)
                {
                    var movie = await movieRepo.GetMovieLimit(theMovieID, limit);
                    var movieDTO = mapper.Map<MovieDTO>(movie);

                    return Ok(movieDTO);


                }

            //GET: api/{movieId}/Dates
            [HttpGet]
            [Route("/api/[controller]/{theMovieID}/Dates")]
            public async Task<ActionResult<IEnumerable<DateTime>>> GetMovieDates(Guid theMovieID)
            {
                var dates = await movieRepo.GetMovieDates(theMovieID);
                return Ok(dates); 

            }

            //GET: api/{movieId}/{date}/Rooms
            [HttpGet]
            [Route("/api/[controller]/{theMovieID}/{date}")]
            public async Task<ActionResult<IEnumerable<RoomDTO>>> GetMovieRooms(Guid theMovieID, DateTime date)
            {
                var rooms = await movieRepo.GetMovieTimes(theMovieID, date);
                var roomsDTO = mapper.Map<IEnumerable<RoomDTO>>(rooms);

                return Ok(roomsDTO);

            }        

            //GET: api/genres
            [HttpGet]
            [Route("/api/[controller]/genres")]
            public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
            {
                var genres = await genreRepo.GetAllAsync();
                var genresDTO = mapper.Map<IEnumerable<GenreDTO>>(genres);

                return Ok(genresDTO);

            }

            //GET: api/genres
            [HttpGet]
            [Route("/api/[controller]/genres/{genreName}")]
            public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMoviesByGenre(string genreName)
            {
                var movies = await movieRepo.GetMoviesByGenre(genreName);
                var moviesDTO = mapper.Map<IEnumerable<MovieDTO>>(movies);

                return Ok(moviesDTO);

            }


        [HttpPut]
            [Route("/api/[controller]/{id}")]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

            public async Task<ActionResult<MovieDTO>> PutMovie(Guid id, MovieDTO movieDTO)
        {
            if(id != movieDTO.MovieID)
            {
                return BadRequest();
            }

            if(movieDTO == null || id == null)
            {
                return BadRequest();
            }

            Movie movie = mapper.Map<Movie>(movieDTO);

        

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                movieRepo.AddMovieWithGenre(movie);
                await movieRepo.Update(movie);
                await movieRepo.SaveAsync();
            }
            catch (Exception exc)
            {
                Log.Logger.Warning($"Threw exception when changing the movie {movie.Name}: {exc}");

                logger.LogError($"Threw exception when changing the movie {movie.Name} room {movie.Name}: {exc}");

                if (!movieRepo.MovieExist(movie))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

            [HttpDelete]
            [Route("/api/[controller]/{id}")]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
            public async Task<ActionResult<MovieDTO>> DeleteMovie(Guid id)
        {
            var movies = await movieRepo.GetByExpressionAsync(m => m.MovieID == id);

               if(movies == null || movies.Count() == 0)
            {
                return NotFound();
            }       
      
            Movie movie = movies.First();

            var genres = await genreRepo.GetByExpressionAsync(c => c.GenreID == movie.GenreID);
                movie.Genre = genres.First();

            await movieRepo.DeleteMovie(movie);

            try
            {
                await movieRepo.SaveAsync();
            }
            catch(Exception exc)
            {
                Log.Logger.Warning($"Threw exception when deleting the movie {movie.Name}: {exc}");

                logger.LogError($"Threw exception when deleting the movie {movie.Name} room {movie.Name}: {exc}");

                throw new Exception($"Deleting {movie.Name} failed");

            }

            var movieDTO = mapper.Map<MovieDTO>(movie);
            return Ok(movieDTO);
        }
    }
}
