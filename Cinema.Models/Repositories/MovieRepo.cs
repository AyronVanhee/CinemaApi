using Cinema.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models.Repositories
{
    public class MovieRepo : GenericRepo<Movie>, IMovieRepo
    {

        //base geeft de afgeleide class toegang tot eigenschappen van de basis classes
        public MovieRepo(DBContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            var currentDate = DateTime.Now;
            Debug.Write("de date nu " + currentDate);
            var movies = await _context.Movie.Include(rr => rr.Genre)
                .OrderByDescending(m=>m.Year)
                .ThenBy(m=>m.Name)
                .ToListAsync();

            //var movieRooms = await _context.MovieRoom.Where(mr => mr.Date > currentDate).OrderBy(mr => mr.Date).Include(r => r.Room).ToListAsync();

            //List<Movie> themovies = new List<Movie>();

            //foreach (Movie m in movies)
            //{
            //    foreach (MovieRoom mr in movieRooms)
            //    {
            //        if (mr.MovieID == m.MovieID)
            //        {
            //            m.MovieRoom.Add(mr);
            //        }

            //    }

            //    themovies.Add(m);

            //}

            //return themovies;

            return movies;


        }

        public async Task<IEnumerable<Movie>> GetAllMoviesWithMovieRoomAsync()
        {
            var currentDate = DateTime.Now;
            var movies = await _context.Movie.Include(rr => rr.Genre)
                .ToListAsync();

            var movieRooms = await _context.MovieRoom.Where(mr => mr.Date > currentDate).OrderBy(mr => mr.Date).Include(r => r.Room).ToListAsync();

            List<Movie> themovies = new List<Movie>();

            foreach (Movie m in movies)
            {
                foreach (MovieRoom mr in movieRooms)
                {
                    if (mr.MovieID == m.MovieID)
                    {
                        m.MovieRoom.Add(mr);
                    }     

                }

                if(m.MovieRoom != null)
                {
                    themovies.Add(m);

                }

            }


            return themovies;

        }

        public async Task<IEnumerable<Movie>> GetMoviesToday()
        {
            var dateToday = DateTime.Now;
            var movieRooms = await _context.MovieRoom.Where(mr => mr.Date.Date == dateToday.Date && dateToday< mr.Date)
                .OrderBy(mr=> mr.Date)
                .Include(m=> m.Movie)
                .Include(r=> r.Room)
                .ToListAsync();

            List<Movie> movies = new List<Movie>();

            for (int i=0; i< movieRooms.Count(); i++)
            {
                if (!movies.Contains(movieRooms[i].Movie))
                    {
                    movies.Add(movieRooms[i].Movie);
                }

            }
            return movies;

        }

        public async Task<Movie> GetMovie(Guid TheMovieID)
        {
            return await _context.Movie.Where(m => m.MovieID == TheMovieID)
                .Include(g => g.Genre)
                //.Include(mr => mr.MovieRoom)
                //.ThenInclude(r => r.Room)
                .SingleAsync();

        }

        public async Task<Movie> GetMovieLimit(Guid TheMovieID, int i)
        {
            var movie = await _context.Movie.Where(m => m.MovieID == TheMovieID)
                .Include(g => g.Genre)
                .Include(mr => mr.MovieRoom)
                .ThenInclude(r => r.Room)
                .SingleAsync();

            var movierooms = await _context.MovieRoom.Where(m => m.MovieID == TheMovieID)
                .OrderBy(mr => mr.Date)
                .Take(i)
                .ToListAsync();

            movie.MovieRoom = movierooms;

            return movie;

        }

        public async Task<IEnumerable<DateTime>> GetMovieDates(Guid TheMovieID)
        {
            var movie = await _context.Movie.Where(m => m.MovieID == TheMovieID).Include(mr => mr.MovieRoom).SingleAsync();

            var dateToday = DateTime.Now;

            List<DateTime> dates = new List<DateTime>();

            foreach (MovieRoom mr in movie.MovieRoom)
            {
                if (dateToday < mr.Date)
                {

               
                var date = mr.Date.Date;
                if (!dates.Contains(date))
                {
                    dates.Add(date);
                }
                }

            }

            dates.Sort((a, b) => a.CompareTo(b));

            return dates;

        }

        public Movie AddMovieWithGenre(Movie movie)
        {
            //Identity column in acht nemen
            if (movie.Genre.GenreName != null)
            {
                //geen dubbels,  
                var exists = _context.Genre.FirstOrDefault(cat => cat.GenreName == movie.Genre.GenreName);
                if (exists != null)
                {
                    movie.Genre = _context.Genre.First(c => c.GenreName == movie.Genre.GenreName);
                }
                else
                {
                    throw new Exception();
                }

            }
            else
            {
                movie.Genre = _context.Genre.First(c => c.GenreID == movie.GenreID);
            }

            return movie;
        }


        public async Task<IEnumerable<Room>> GetMovieTimes(Guid TheMovieID, DateTime dateTime)
        {
            var dateToday = DateTime.Now;

            var movieRooms = await _context.MovieRoom.Where(m => m.MovieID == TheMovieID && m.Date.Date == dateTime.Date && dateToday < m.Date)
                .OrderBy(mr => mr.RoomID)
                .ThenBy(mr=>mr.Date)
                .Include(m => m.Movie)
                .Include(m => m.Room)
                .ToListAsync();

            List<Room> rooms = new List<Room>();

            for (int i = 0; i < movieRooms.Count(); i++)
            {
                if (!rooms.Contains(movieRooms[i].Room))
                {
                    rooms.Add(movieRooms[i].Room);

                }

            }

            return rooms;
        }

        public bool MovieExist(Movie movie)
        {
            var exists = _context.Movie.Any(m => m.Name == movie.Name);
            return exists;
        }

        public async Task DeleteMovie(Movie movie)
        {
            var movieRooms = await _context.MovieRoom.Where(r => r.MovieID == movie.MovieID).ToListAsync();
            var reservations = await _context.Reservation.Where(r => r.MovieRoom.MovieID == movie.MovieID).ToListAsync();

            foreach (var reservation in reservations)
            {
                Debug.Write("de reservation" + reservation.ReservationID);
                _context.Reservation.Remove(reservation);
            }

            foreach (var movieRoom in movieRooms)
            {
                Debug.Write("de movierrom" + movieRoom.MovieID);
                _context.MovieRoom.Remove(movieRoom);
            }

            _context.Movie.Remove(movie);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(string genre)
        {
            var movies = await _context.Movie.Include(g => g.Genre)
                .Where(m => m.Genre.GenreName == genre)
                .ToListAsync();

            return movies;
        }

    }
}
