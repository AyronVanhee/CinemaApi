using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Models.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [Required]
        public string GenreName { get; set; }

        public ICollection<Movie> Movie { get; set; }
    }
}
