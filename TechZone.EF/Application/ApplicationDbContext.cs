using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Laptop;
using TechZone.Core.Entities.Logging;
using TechZone.Core.Entities.Order;
using TechZone.Core.Entities.Repair;
using TechZone.Core.Entities.User;
using TechZone.Core.EntityConfigs;

namespace TechZone.EF.Application
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<LaptopVariant> LaptopVariants { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<RepairServiceItem> RepairServiceItems { get; set; }
        public DbSet<RepairRequest> RepairRequests { get; set; }
        public DbSet<LaptopImage> LaptopImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToLower());
                }
            }
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new LaptopConfiguration());
            modelBuilder.ApplyConfiguration(new LaptopVariantConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ShippingConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
            modelBuilder.ApplyConfiguration(new RepairServiceItemConfiguration());
            modelBuilder.ApplyConfiguration(new RepairRequestConfiguration());
            modelBuilder.ApplyConfiguration(new LaptopImageConfiguration());
            modelBuilder.ApplyConfiguration(new EmailQueueConfiguration());
            modelBuilder.ApplyConfiguration(new VerificationCodeConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}