using Cinema.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models.Repositories
{
    public class UserRepo : GenericRepo<CinemaUser>, IUserRepo
    {

        private readonly UserManager<CinemaUser> userManager;

        //base geeft de afgeleide class toegang tot eigenschappen van de basis classes
        public UserRepo(DBContext _context, UserManager<CinemaUser> userManager) : base(_context)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<Reservation>> GetUserTickets(Guid userId)
        {

            var reservations = await _context.Reservation.Where(t => t.UserID == userId)
                .Include(mr => mr.MovieRoom)
                .ThenInclude(r => r.Room)
                .ThenInclude(s => s.Seats)
                .Include(mr => mr.MovieRoom)
                .ThenInclude(m => m.Movie)
                .Include(u => u.CinemaUser)
                .OrderBy(mr => mr.MovieRoom.Date)
                .ThenBy(s=>s.Seat.SeatNumber)

                .ToListAsync();

            return reservations;

        }

        public bool UserExist(CinemaUser user)
        {
            var exists = _context.CinemaUser.Any(m => m.UserName == user.UserName || m.Email == user.Email);
            return exists;
            }

    }
}
