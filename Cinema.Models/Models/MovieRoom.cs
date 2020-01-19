using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Models.Models
{
    public class MovieRoom
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid MovieRoomID { get; set; } = Guid.NewGuid();

        [ForeignKey("Movie")]
        [Display(Name = "Movie")]
        public Guid MovieID { get; set; }

        [ForeignKey("Room")]
        [Display(Name = "Room")]
        public int RoomID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        //virtuele relaties leggen
        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }

        public ICollection<Reservation> Reservations { get; set; }


    }
}
