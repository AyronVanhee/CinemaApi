using Cinema.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinema.Models.Models
{
    public class DBContext: IdentityDbContext<CinemaUser>
    {
        private static ModelBuilder _modelBuilder { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieRoom> MovieRoom { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<CinemaUser> CinemaUser { get; set; }
        public DbSet<Reservation> Reservation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            base.OnModelCreating(_modelBuilder);


            //modelBuilder.Entity<MovieRoom>().HasKey(mr => new { mr.MovieID, mr.RoomID});


            //default veel op veel is cascade delete!
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            ModelBuilderExtensions.modelBuilder = _modelBuilder;

            ModelBuilderExtensions.Seed();

        }


    }
    
}
