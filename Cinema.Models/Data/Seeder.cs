using Cinema.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models.Data
{
        public class Seeder
        {
            public Seeder()
            {
            }
            public static IConfiguration _configuration { get; set; }
            public static DBContext _context { get; set; }

            public static string _contentRootPath { get; set; }

            public static ModelBuilder modelBuilder { get; set; }
      
            public  async Task SeedRole(RoleManager<IdentityRole> roleManager)
            {
                if (!roleManager.RoleExistsAsync("User").Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "User";
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                
            }

        }

            public  async Task SeedUser(UserManager<CinemaUser> userManager)
        {
                if (userManager.FindByNameAsync("danny.zuko@howest.be").Result == null)
                {
                    CinemaUser user = new CinemaUser();
                    user.UserName = "danny.zuko@howest.be";
                    user.Email = "danny.zuko@howest.be";

                    IdentityResult result = userManager.CreateAsync(user, "Danny@1").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                        
                    }
                }

            if (userManager.FindByNameAsync("docent@howest.be").Result == null)
            {
                CinemaUser user = new CinemaUser();
                user.UserName = "docent@howest.be";
                user.Email = "docent@howest.be";

                IdentityResult result = userManager.CreateAsync(user, "Docent@1").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();

                }
            }
        }

        }

    }

