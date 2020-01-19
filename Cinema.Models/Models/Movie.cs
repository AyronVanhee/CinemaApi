using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Models.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid MovieID { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Year { get; set; }

        public int GenreID { get; set; }

        //virtuele relaties leggen
        public  Genre Genre { get; set; }
        public ICollection<MovieRoom> MovieRoom { get; set; }

    }
}
