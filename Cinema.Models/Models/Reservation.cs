using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Models.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid ReservationID { get; set; } = Guid.NewGuid();

        [Required]
        public Guid SeatID {get;set;}
        [Required]
        public Guid MovieRoomID { get; set; }
        [Required]
        public Guid UserID { get; set; }

        [Required]
        public double Price { get; set; }

        //relaties leggen
        public virtual Seat Seat { get; set; }
        public virtual MovieRoom MovieRoom { get; set; }
        public virtual CinemaUser CinemaUser { get; set; }
    }
}
