using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Api.Models
{
    public class ReservationDTO
    {

        public int SeatNumber { get; set; }

        public string MovieName { get; set; }

        public Guid MovieId { get; set; }

        public int Room { get; set; }

        public double Price { get; set; }

        public DateTime Date{get;set;}
    }
}
