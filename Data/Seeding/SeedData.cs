using Data.DataContext;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seeding
{
    public static class SeedData
    {
        public static async Task Seed(SportStoreContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    Email = "giahuy575621@gmail.com",
                    UserName = "giahuy1092002"
                };
                await userManager.CreateAsync(user, "Giahuy@1092002");
                await userManager.AddToRoleAsync(user, "Customer");

                var admin = new User
                {
                    Email = "admin@gmail.com",
                    UserName = "admin123456"
                };
                await userManager.CreateAsync(admin, "Admin@123456");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
            await context.SaveChangesAsync();
        }
    }
}
