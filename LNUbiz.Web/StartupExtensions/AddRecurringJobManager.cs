using LNUbiz.BLL.Interfaces;
using Hangfire;
using LNUbiz.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using LNUbiz.DAL.Entities;

namespace LNUbiz.Web.StartupExtensions
{
    public static class AddRecurringJobManager
    {
        public static void AddRecurringJobs(this IServiceProvider serviceProvider,
                                                 IRecurringJobManager recurringJobManager,
                                                 IConfiguration Configuration)
        {
            recurringJobManager.AddOrUpdate("New LNUbiz members greeting",
                                            () => serviceProvider.GetService<IEmailContentService>()
                                                .GetAuthGreetingEmail(),
                                            Cron.Daily(), TimeZoneInfo.Local);
        }

        private static async Task CreateRolesAsync(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roles = new[]
            {
                Roles.Admin,
                Roles.User
            };
            foreach (var role in roles)
            {
                if (!(await roleManager.RoleExistsAsync(role)))
                {
                    var idRole = new IdentityRole { Name = role };
                    await roleManager.CreateAsync(idRole);
                }
            }
            var admin = Configuration.GetSection(Roles.Admin);
            var profile = new User
            {
                Email = admin["Email"],
                UserName = admin["Email"],
                FirstName = Roles.Admin,
                LastName = Roles.Admin,
                EmailConfirmed = true,
                ImagePath = "default_user_image.png",
                RegistredOn = DateTime.Now
            };
            if (await userManager.FindByEmailAsync(admin["Email"]) == null)
            {
                var idenResCreateAdmin = await userManager.CreateAsync(profile, admin["Password"]);
                if (idenResCreateAdmin.Succeeded)
                    await userManager.AddToRoleAsync(profile, Roles.Admin);
            }
            else if (!await userManager.IsInRoleAsync(userManager.Users.First(item => item.Email == profile.Email), Roles.Admin))
            {
                var user = userManager.Users.First(item => item.Email == profile.Email);
                await userManager.AddToRoleAsync(user, Roles.Admin);
            }
        }
    }
}
