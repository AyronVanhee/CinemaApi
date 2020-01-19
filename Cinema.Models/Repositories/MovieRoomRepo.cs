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
    public class MovieRoomRepo : GenericRepo<MovieRoom>, IMovieRoomRepo
    {
        //base geeft de afgeleide class toegang tot eigenschappen van de basis classes
        public MovieRoomRepo(DBContext _context) : base(_context)
        {

        }


        public async Task<MovieRoom> GetMovieRoom(Guid movieroom)
        {
            return await _context.MovieRoom.Where(mr => mr.MovieRoomID == movieroom)
                .Include(m => m.Movie)
                .Include(r => r.Room)
                .ThenInclude(s => s.Seats)
                .SingleAsync();

        }

        public async Task<IEnumerable<Seat>> GetAllSeats(Guid MovieRoomID)
        {
            //var seats = await _context.Seat.Where(mr => mr.Room.MovieRoomID == MovieRoomID).ToListAsync();
            var movieRooms = await _context.MovieRoom.Where(mr => mr.MovieRoomID == MovieRoomID).Include(s => s.Room).ThenInclude(s => s.Seats).SingleAsync();

            List<Seat> seats = new List<Seat>();

            foreach (Seat s in movieRooms.Room.Seats)
            {

                seats.Add(s);
            }

            var theSeats = seats.OrderBy(s => s.SeatNumber);

            return theSeats;

        }

        public async Task<IEnumerable<Seat>> GetAvailableSeats(Guid MovieRoomID)
        {
            var movieRooms = await _context.MovieRoom.Where(mr => mr.MovieRoomID == MovieRoomID).Include(s => s.Room).ThenInclude(s => s.Seats)
                .Include(r => r.Reservations).SingleAsync();

            //alle seats
            List<Seat> seats = new List<Seat>();


            foreach (Seat s in movieRooms.Room.Seats)
            {

                seats.Add(s);
            }




            //available seats
            List<Seat> availableSeats = new List<Seat>();

                foreach(Reservation r in movieRooms.Reservations)
                {
                    seats.Remove(r.Seat);
                }
            
       

            var theSeats = seats.OrderBy(s => s.SeatNumber);

            return theSeats;

        }

        public async Task<IEnumerable<Seat>> GetOccupiedSeats(Guid MovieRoomID)
        {

            var movieRooms = await _context.MovieRoom.Where(mr => mr.MovieRoomID == MovieRoomID).Include(s => s.Room).ThenInclude(s => s.Seats)
                 .Include(r => r.Reservations).SingleAsync();

            List<Seat> seats = new List<Seat>();

            
                    foreach (Reservation r in movieRooms.Reservations)

                        {
                            seats.Add(r.Seat);

                        }
          

            
            var theSeats = seats.OrderBy(s => s.SeatNumber);

            return theSeats;

        }
        public async Task PostReservation(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<MovieRoom> AddMovieroomWithMovieAndRoom(MovieRoom movieRoom)
        {
            //Identity column in acht nemen
            if (movieRoom.Movie.Name != null)
            {
                //geen dubbels,  
                var exists = _context.Movie.FirstOrDefault(cat => cat.Name == movieRoom.Movie.Name);
                if (exists != null)
                    movieRoom.Movie = _context.Movie.First(c => c.Name == movieRoom.Movie.Name);

            }
            else
            {
                movieRoom.Movie = _context.Movie.First(c => c.MovieID == movieRoom.MovieID);
            }

            if (movieRoom.Room.RoomID != null)
            {
                //geen dubbels,  
                var exists = _context.Room.FirstOrDefault(cat => cat.RoomID == movieRoom.Room.RoomID);
                if (exists != null)
                    movieRoom.Room = _context.Room.First(c => c.RoomID == movieRoom.Room.RoomID);

            }
            else
            {
                movieRoom.Room = _context.Room.First(c => c.RoomID == movieRoom.RoomID);
            }

            await _context.AddAsync(movieRoom);
            return movieRoom;
        }

        public async Task<IEnumerable<MovieRoom>> GetAllMovieRooms()
        {

            return await _context.MovieRoom
                 .Include(mr => mr.Movie)
                 .Include(mr => mr.Room)
                 .ToListAsync();

        }

        public bool ReservationExist(Reservation reservation)
        {
            var exists = _context.Reservation.Any(r => r.SeatID == reservation.SeatID && r.MovieRoomID == reservation.MovieRoomID);
            return exists;
        }

        public bool MovieRoomExist(MovieRoom movieRoom)
        {
            Debug.Write("de movie id is " + movieRoom.Movie.MovieID + " de room " + movieRoom.Room.RoomID + " de date  " + movieRoom.Date);
            var exists = _context.MovieRoom.Any(m => m.Movie.Name == movieRoom.Movie.Name && m.RoomID == movieRoom.Room.RoomID && m.Date == movieRoom.Date);
            return exists;
        }

        public async Task<IEnumerable<Room>> GetRoomWithHours(Guid movieID)
        {

            var movieRooms = await _context.MovieRoom.Where(mr => mr.MovieID == movieID).Include(r => r.Room).Include(m => m.Movie).OrderBy(mr => mr.Date)
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

        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await _context.Room.ToListAsync();
        }

    }
}
