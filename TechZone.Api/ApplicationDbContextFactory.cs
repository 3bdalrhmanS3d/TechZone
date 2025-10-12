using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TechZone.Infrastructure.Application; // تأكد أن هذا هو namespace الصحيح لـ ApplicationDbContext

namespace TechZoneV1
{
    /// <summary>
    /// Factory لـ EF Core Design-Time (يُستخدم عند تشغيل Update-Database أو Add-Migration)
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // استخدم نفس الـ Connection String اللي في appsettings.json للـ Local Development
            var connectionString = "Host=localhost;Port=5432;Database=TechZoneLocal1;Username=postgres;Password=zaq123;SSL Mode=Disable;";

            optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure();
            });

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}