using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Models.Models
{
    public class Seat
    {
        [Key]
        public Guid SeatID { get; set; } = Guid.NewGuid();
        public int SeatNumber { get; set; }

        [Required]
        public bool Special { get; set; }

        [Display(Name = "Room")]
        public int RoomID { get; set; }

        //virtuele relaties leggen
        public virtual Room Room { get; set; }


    }
}
