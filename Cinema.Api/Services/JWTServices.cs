using Cinema.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Services
{
    public class JWTServices<CinemaUser> where CinemaUser : IdentityUser
    {
        private readonly ILogger logger;
        private readonly UserManager<CinemaUser> userManager;
        private readonly IPasswordHasher<CinemaUser> hasher;
        private readonly IConfiguration configuration;

        public JWTServices(IConfiguration configuration, ILogger logger, UserManager<CinemaUser> userManager, IPasswordHasher<CinemaUser> hasher)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.hasher = hasher;
            this.configuration = configuration;
        }

        //returnen van een anoniem object
        public async Task<object> GenerateJwtToken(UserDTO identityDTO)
        {
            //0. Geen signin manager -> deze signin manager maakt autorisatie cookie aan 
            //signin manager gebruiker zou kunnen als backup voor een falend token.

            //1. Gebruiker opzoeken in de database met async UserManager en hash vergelijking
            try
            {
                CinemaUser user = await userManager.FindByNameAsync(identityDTO.UserName);
                var roles = await userManager.GetRolesAsync(user);

                if (user != null)
                {
                    //identity control op de hash (=kan zonder signin)
                    if (hasher.VerifyHashedPassword(user, user.PasswordHash, identityDTO.PasswordHash) == PasswordVerificationResult.Success)
                    {
                        //code hier indien paswoord controle.
                        Console.Write("password verified: {0}", user.PasswordHash);
                    }
                    else
                    {
                        return new { error = "Unknown user or password" };

                    }

                }
                else
                {
                    return new { error = "Unknown user or password" };
                }

                //2. claims (key/value) toevoegen
                //2.1 Customised of extra claims, komende van het Identity system:
                var userClaims = await userManager.GetClaimsAsync(user);
                await userManager.RemoveClaimsAsync(user, userClaims);


                //combined string van roles kan niet => ClaimTypes.Role
                foreach (var role in roles)
                {
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
                    await userManager.AddClaimAsync(user, new Claim("Rollen", role));

                }

                userClaims = await userManager.GetClaimsAsync(user);


                //2.2. Noodzakelijke claims komende vd JWD spec
                var claims = new List<Claim>
             {
                //JWT claims zijn ingebouwd in de JWT spec: subscriber, JWT Id
                 new Claim(JwtRegisteredClaimNames.Sub, identityDTO.UserName),  //subscriber
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                 new Claim("UserId", user.Id),
             }.Union(userClaims);   //nog de extra userClaims toevoegen.

                //3. Sigin credentials met de symmetric key & encryptie methode
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //key en protocol

                //4. aanmaken van het token
                var token = new JwtSecurityToken(
                 issuer: configuration["Tokens:Issuer"],  //onze website
                 audience: configuration["Tokens:Audience"],//gebruikers
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Tokens:Expires"])),
                 signingCredentials: creds //controleert token v
                 );

                //5. user info returnen (vervaldatum als additionele info)
                return new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token), //token generator
                    expiration = token.ValidTo
                };
            }
            catch (Exception exc)
            {
                logger.LogError($"Exception thrown when creating JWT: {exc}");
            }
            return new { error = "Failed to generate JWT token" }; //minimale info ->meer in de logger
        }
    }
}