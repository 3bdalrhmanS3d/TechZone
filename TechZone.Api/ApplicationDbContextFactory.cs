using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using TechZone.Infrastructure.Application;

namespace TechZone
{
    /// <summary>
    /// Factory for EF Core design-time (used for Add-Migration / Update-Database)
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // 1️. Try to read from DATABASE_URL environment variable
            var dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            string connectionString;

            if (!string.IsNullOrEmpty(dbUrl))
            {
                // Convert DATABASE_URL to Npgsql connection string
                var uri = new Uri(dbUrl);
                var userInfo = uri.UserInfo.Split(':');

                connectionString =
                    $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};" +
                    $"Username={userInfo[0]};Password={userInfo[1]};Ssl Mode=Require;Trust Server Certificate=true;";
            }
            else
            {
                // 2️⃣ Fallback to local PostgreSQL for development
                connectionString =
                    "Host=localhost;Port=5432;Database=TechZoneLocal;Username=postgres;Password=root;SSL Mode=Disable;";
            }

            // 3️⃣ Configure EF Core
            optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure();
            });

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
