using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TechZone.Core.Entities;

namespace TechZone.Api.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            // Get RoleManager & UserManager from DI container
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Define application roles
            string[] roles = { "Admin", "User" };

            // Create roles if they don't exist
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create default admin user if not exists
            var adminEmail = "admin@techzone.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FullName = "TechZone Admin",
                    EmailConfirmed = true
                };

                // Default password for admin
                var result = await userManager.CreateAsync(admin, "Admin@123");

                if (result.Succeeded)
                {
                    // Assign Admin role to the default admin user
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
