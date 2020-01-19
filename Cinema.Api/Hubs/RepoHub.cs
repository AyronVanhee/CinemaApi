using AutoMapper;
using Cinema.Api.Models;
using Cinema.Models.Models;
using Cinema.Models.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cinema.Api.Hubs
{
    public class RepoHub: Hub
    {

        private readonly IMovieRoomRepo movieRoomRepo;
        private readonly ILogger<RepoHub> logger;


        public RepoHub(IMovieRoomRepo movieRoomRepo, ILogger<RepoHub> logger)
        {
            this.movieRoomRepo = movieRoomRepo;
            this.logger = logger;
        }
     
        public async Task OrderedReservation(Reservation reservation)
        {
            try
            {
                Debug.Write("---------------------");
                Debug.Write("zit in de backend");
                Debug.Write(reservation.MovieRoomID);
                var theId = reservation.MovieRoomID;
                await Clients.Others.SendAsync("ReservationOrder", theId);

            }
            catch(Exception exc)
            {
                logger.LogError($"Threw exception when sending clients ReservationOrder: {exc}");
                throw new Exception($"Sending clients ReservationOrder did not succeed.");
            }
        }

    }
}
