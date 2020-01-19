using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Models.Models;

namespace Cinema.Models.Repositories
{
    public interface IMovieRepo : IGenericRepo<Movie>
    {
        Movie AddMovieWithGenre(Movie movie);
        Task DeleteMovie(Movie movie);
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<IEnumerable<Movie>> GetAllMoviesWithMovieRoomAsync();
        Task<Movie> GetMovie(Guid TheMovieID);
        Task<IEnumerable<DateTime>> GetMovieDates(Guid TheMovieID);
        Task<Movie> GetMovieLimit(Guid TheMovieID, int i);
        Task<IEnumerable<Movie>> GetMoviesToday();
        bool MovieExist(Movie movie);
        Task<IEnumerable<Room>> GetMovieTimes(Guid TheMovieID, DateTime dateTime);

        Task<IEnumerable<Movie>> GetMoviesByGenre(string genre);
    }
}