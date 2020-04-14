using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Api.Models;
using Cinema.Models.Models;
using Cinema.Models.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cinema.Api.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    [EnableCors("MAGI")]
    [Consumes("application/json", "application/json-path+json", "multipart/form-data", "application/form-data")]

    public class UserController : ControllerBase
    {
        private readonly IUserRepo userRepo;
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;
        private readonly UserManager<CinemaUser> userManager;
        private readonly SignInManager<CinemaUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(IUserRepo userRepo, IMapper mapper,
            ILogger<UserController> logger, UserManager<CinemaUser> userManager,
            SignInManager<CinemaUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        //GET: api/User
        [HttpGet(Name = "GetUser")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser()
        {
            var user = await userRepo.GetAllAsync();

            var userDTO = mapper.Map<IEnumerable<UserDTO>>(user);

            return Ok(userDTO); //data met Links informatie

        }

        //Get: api/user/{userId}
        [HttpGet]
        [Route("/api/[controller]/{userId}")]
        public async  Task<ActionResult<UserDTO>> GetUserDetails(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var userDTO = mapper.Map<UserDTO>(user);

            return Ok(userDTO); //data met Links informatie

        }

        //Get: api/user/roles
        [HttpGet]
        [Route("/api/[controller]/roles")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
        {
            var roles = roleManager.Roles.ToList();

            return Ok(roles); //data met Links informatie

        }

        //GET: api/{userId}/reservations
        [HttpGet]
        [Route("/api/[controller]/reservations")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]

        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                // or
               Debug.Write(identity.FindFirst("UserId").Value);

            }

            var reservations = await userRepo.GetUserTickets(Guid.Parse(identity.FindFirst("UserId").Value));
            var reservationsDTO = mapper.Map<IEnumerable<ReservationDTO>>(reservations);

            return Ok(reservationsDTO); //data met Links informatie

        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserDTO>> AddUser([Bind("UserName,Email,PasswordHash")]
                [FromBody] UserDTO userDTO, [FromForm] IFormCollection formCollection)
        {

            if (userDTO == null)
            {
                return BadRequest("Unsufficient data provided");
            }

            CinemaUser cinemaUser = mapper.Map<CinemaUser>(userDTO);

            cinemaUser.Id = Guid.NewGuid().ToString();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }
            if (userRepo.UserExist(cinemaUser))
            {
                return Ok(new { error = "User already exists" });
            }


            try
            {
                var result = await userManager.CreateAsync(cinemaUser, cinemaUser.PasswordHash);
                var roleresult =await userManager.AddToRoleAsync(cinemaUser, "User");

                await signInManager.SignInAsync(cinemaUser, false);

                if (result.Succeeded && roleresult.Succeeded)
                {

                    UserDTO userDto = mapper.Map<UserDTO>(cinemaUser);

                    return Created($"api/{cinemaUser.Id}/reservations", userDto);
                }


            }
            catch (Exception exc)
            {
                logger.LogError($"Threw exception when adding the user {cinemaUser.UserName}: {exc}");
                throw new Exception($"Creating  the user {cinemaUser.UserName} did not succeed.");

            }

            return BadRequest(new { message = "bad request" });
        }

    }
}
