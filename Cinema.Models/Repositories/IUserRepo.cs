using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Models.Repositories
{
    public interface IUserRepo : IGenericRepo<CinemaUser>
    {
        Task<IEnumerable<Reservation>> GetUserTickets(Guid userId);
        bool UserExist(CinemaUser user);
    }
}