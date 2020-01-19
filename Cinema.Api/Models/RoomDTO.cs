using Cinema.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Api.Models
{
    public class RoomDTO
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int? RoomID { get; set; }

        //relatie leggen
        public IEnumerable<MovieRoomDTO> MovieRoom { get; set; }
    }
}
