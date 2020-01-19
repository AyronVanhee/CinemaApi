using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Models.Models
{
    public class Room
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int? RoomID { get; set; }

        //relatie leggen
        public ICollection<MovieRoom> MovieRoom { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}
