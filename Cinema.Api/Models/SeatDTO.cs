using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Api.Models
{
    public class SeatDTO
    {
        public Guid SeatID { get; set; }
        public int SeatNumber { get; set; }
        public bool Special { get; set; }
        public int RoomID { get; set; }

    
    }
}
