using System;
using System.Threading.Tasks;
using Cinema.Api.Models;
using Cinema.Api.Services;
using Cinema.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cinema.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILogger<AuthController> logger;
        private readonly UserManager<CinemaUser> userMgr;
        private readonly IConfiguration configuration;
        private readonly IPasswordHasher<CinemaUser> hasher;

        public AuthController( ILogger<AuthController> logger, UserManager<CinemaUser> userMgr, IConfiguration configuration, IPasswordHasher<CinemaUser> hasher)
        {
            this.logger = logger;
            this.userMgr = userMgr;
            this.configuration = configuration;
            this.hasher = hasher;
        }

        //JWT Authentication -----------------------------------------------------------------
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateJwtToken([FromBody]UserDTO identityModel)
        {
            try
            {
                var jwtsvc = new JWTServices<CinemaUser>(configuration, logger,
                userMgr, hasher);
                var token = await jwtsvc.GenerateJwtToken(identityModel);              
                return Ok(token);
            }
            catch (Exception exc)
            {
                logger.LogError($"Exception thrown when creating JWT: {exc}");
            }
            //Bij niet succesvolle authenticatie wordt een Badrequest (=zo weinig mogelijke info) teruggeven.
            return BadRequest("Failed to generate JWT token");
        }


    }

}