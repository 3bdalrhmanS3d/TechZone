using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.EntityConfigs;
using TechZone.Core.Entities;

namespace TechZone.EF.Application
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<LaptopVariant> LaptopVariants { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly apply configurations
            modelBuilder.ApplyConfiguration(new LaptopVariantConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new EmailQueueConfiguration());
            modelBuilder.ApplyConfiguration(new VerificationCodeConfiguration());
            modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());

            // Optionally keep this to apply any additional configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}
