using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Api.Models
{
    public class MovieDTO
    {
        public Guid MovieID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; }

        public int Year { get; set; }

        public double Price { get; set; }
        public string GenreName { get; set; }

        public string Image { get; set; }

        public ICollection<MovieRoomDTO> MovieRoom { get; set; }

    }
}
