using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Api.Models
{
    public class MovieRoomDTO
    {
        public string Movie { get; set; }
        public int Room { get; set; }
        public DateTime Date { get; set; }
        public Guid MovieRoomID { get; set; }

    }
}
