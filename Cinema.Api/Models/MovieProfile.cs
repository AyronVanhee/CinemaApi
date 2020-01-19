using AutoMapper;
using Cinema.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Api.Models
{
    public class MovieProfile: Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(r => r.Genre.GenreName))
                ;

            CreateMap<MovieDTO, Movie>()
              .ForMember(dest => dest.Genre, opt => opt.MapFrom(model => new Genre { GenreName = model.GenreName }))
                ;

            CreateMap<MovieRoom, MovieRoomDTO>()
                .ForMember(dest=> dest.Movie, opt=> opt.MapFrom(mr=> mr.Movie.Name))
                .ForMember(dest=> dest.Room, opt=> opt.MapFrom(mr=> mr.Room.RoomID))             
                ;
            CreateMap<MovieRoomDTO, MovieRoom>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(model => new Movie { Name = model.Movie }))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(model => new Room { RoomID = model.Room }))
                ;

            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();

            CreateMap<Seat, SeatDTO>();
            CreateMap<SeatDTO, Seat>();

            CreateMap<Room, RoomDTO>();
            CreateMap<RoomDTO, Room>();

            CreateMap<UserDTO, CinemaUser>();
            CreateMap<CinemaUser, UserDTO>();

            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(r => r.Seat.SeatNumber))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(r => r.MovieRoom.Movie.Name))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(r => r.MovieRoom.Movie.MovieID))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(r => r.MovieRoom.Room.RoomID))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(r => r.MovieRoom.Date));
                
            CreateMap<ReservationDTO, Reservation>()
                .ForMember(dest=> dest.Seat, opt=> opt.MapFrom(model=> new Seat() { SeatNumber = model.SeatNumber }))
                .ForMember(dest=> dest.MovieRoom, opt=> opt.MapFrom(model=> new Movie() { Name = model.MovieName }))
                .ForMember(dest => dest.MovieRoom, opt => opt.MapFrom(model => new Movie() { MovieID = model.MovieId }))
                .ForMember(dest=> dest.MovieRoom, opt=> opt.MapFrom(model=> new Room() { RoomID = model.Room }))
                .ForMember(dest=> dest.MovieRoom, opt=> opt.MapFrom(model=> new MovieRoom() { Date = model.Date}))
                ;


        }
    }
}
