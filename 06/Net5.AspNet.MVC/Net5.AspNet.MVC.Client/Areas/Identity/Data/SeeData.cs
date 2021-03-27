using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.MVC.Client.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Client.Areas.Identity.Data
{
    public static class SeeData
    {
        public static void Initialize(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            EnsureRolesAsync(roleManager).Wait();

            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            EnsureUsersAsync(userManager).Wait();
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            bool alreadyExists = await roleManager.RoleExistsAsync(Roles.Administrator);
            if (!alreadyExists)
            {
                var role = new IdentityRole(Roles.Administrator);
                await roleManager.CreateAsync(role);
                await roleManager.AddClaimAsync(role, new Claim("GrantAccess", GrantAccess.Delete));
                await roleManager.AddClaimAsync(role, new Claim("GrantAccess", GrantAccess.Edit));
            }

            alreadyExists = await roleManager.RoleExistsAsync(Roles.PowerUser);
            if (!alreadyExists)
            {
                var role = new IdentityRole(Roles.PowerUser);
                await roleManager.CreateAsync(role);
            }

            alreadyExists = await roleManager.RoleExistsAsync(Roles.User);
            if (!alreadyExists)
            {
                var role = new IdentityRole(Roles.User);
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task EnsureUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = userManager.Users.Where(x => x.UserName == "admin@todo.local").SingleOrDefault();
            if(admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin@todo.local",
                    Email = "admin@todo.local",
                    FirstName = "Admin",
                    LastName = "Todo",
                    SurName = "Local"
                };

                await userManager.CreateAsync(admin, "P@ssword1234");
                string emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                await userManager.ConfirmEmailAsync(admin, emailConfirmationToken);
                await userManager.AddToRoleAsync(admin, Roles.Administrator);
                await userManager.AddToRoleAsync(admin, Roles.User);
            }

            var powerUser = userManager.Users.Where(x => x.UserName == "power.user@todo.local").SingleOrDefault();
            if (powerUser == null)
            {
                powerUser = new ApplicationUser
                {
                    UserName = "power.user@todo.local",
                    Email = "power.user@todo.local",
                    FirstName = "Power",
                    LastName = "Todo",
                    SurName = "Local"
                };

                await userManager.CreateAsync(powerUser, "P@ssword1234");
                string emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(powerUser);
                await userManager.ConfirmEmailAsync(powerUser, emailConfirmationToken);
                await userManager.AddToRoleAsync(powerUser, Roles.PowerUser);
                await userManager.AddToRoleAsync(powerUser, Roles.User);
            }

            var user = userManager.Users.Where(x => x.UserName == "user@todo.local").SingleOrDefault();
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "user@todo.local",
                    Email = "user@todo.local",
                    FirstName = "User",
                    LastName = "Todo",
                    SurName = "Local"
                };

                await userManager.CreateAsync(user, "P@ssword1234");
                string emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await userManager.ConfirmEmailAsync(user, emailConfirmationToken);                
                await userManager.AddToRoleAsync(user, Roles.User);
            }
        }
    }
}
