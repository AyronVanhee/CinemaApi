using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Models.Models;

namespace Cinema.Models.Repositories
{
    public interface IMovieRoomRepo : IGenericRepo<MovieRoom>
    {
        Task<MovieRoom> AddMovieroomWithMovieAndRoom(MovieRoom movieRoom);
        Task<IEnumerable<MovieRoom>> GetAllMovieRooms();
        Task<IEnumerable<Seat>> GetAllSeats(Guid MovieRoomID);
        Task<IEnumerable<Seat>> GetAvailableSeats(Guid MovieRoomID);
        Task<MovieRoom> GetMovieRoom(Guid movieroom);
        Task<IEnumerable<Room>> GetRoomWithHours(Guid movieID);
        bool MovieRoomExist(MovieRoom movieRoom);
        Task PostReservation(Reservation reservation);
        bool ReservationExist(Reservation reservation);

        Task<IEnumerable<Seat>> GetOccupiedSeats(Guid MovieRoomID);
        Task<IEnumerable<Room>> GetRooms();

    }
}