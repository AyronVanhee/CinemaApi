using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;


namespace Cinema.Models.Models
{
    public class CinemaUser: IdentityUser
    {
        public ICollection<Reservation> Reservations { get; set; }
    }
}
