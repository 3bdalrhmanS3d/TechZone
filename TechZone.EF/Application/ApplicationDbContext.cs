using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechZone.Core.Entities;
using TechZone.Core.EntityConfigs;

namespace TechZone.EF.Application
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Core Entities
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<LaptopVariant> LaptopVariants { get; set; }
        public DbSet<LaptopImage> LaptopImages { get; set; }
        public DbSet<LaptopPort> LaptopPorts { get; set; }
        public DbSet<LaptopWarranty> LaptopWarranties { get; set; }

        // Accessory System
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<AccessoryType> AccessoryTypes { get; set; }
        public DbSet<AccessoryImage> AccessoryImages { get; set; }
        public DbSet<AccessoryAttribute> AccessoryAttributes { get; set; }
        public DbSet<AccessoryCompatibility> AccessoryCompatibilities { get; set; }

        // Pricing & Discounts
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<UserDiscountUsage> UserDiscountUsages { get; set; }

        // Orders & Shopping
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        // Payments
        public DbSet<Payment> Payments { get; set; }

        // Ratings & Reviews
        public DbSet<Rating> Ratings { get; set; }

        // Repair Services
        public DbSet<RepairServiceItem> RepairServiceItems { get; set; }
        public DbSet<RepairRequest> RepairRequests { get; set; }

        // Analytics & Tracking
        public DbSet<ProductView> ProductViews { get; set; }
        public DbSet<StockAlert> StockAlerts { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        // Authentication & Communication
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply individual configurations
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

            // Core Product Configurations
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new LaptopConfiguration());
            modelBuilder.ApplyConfiguration(new LaptopVariantConfiguration());
            modelBuilder.ApplyConfiguration(new LaptopImageConfiguration());
            modelBuilder.ApplyConfiguration(new LaptopPortConfiguration());
            modelBuilder.ApplyConfiguration(new LaptopWarrantyConfiguration());

            // Accessory System Configurations
            modelBuilder.ApplyConfiguration(new AccessoryConfiguration());
            modelBuilder.ApplyConfiguration(new AccessoryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccessoryImageConfiguration());
            modelBuilder.ApplyConfiguration(new AccessoryAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new AccessoryCompatibilityConfiguration());

            // Pricing & Discounts Configurations
            modelBuilder.ApplyConfiguration(new PriceHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDiscountConfiguration());
            modelBuilder.ApplyConfiguration(new UserDiscountUsageConfiguration());

            // Orders & Shopping Configurations
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new ShipmentConfiguration());

            // Payment Configuration
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());

            // Ratings Configuration
            modelBuilder.ApplyConfiguration(new RatingConfiguration());

            // Repair Services Configurations
            modelBuilder.ApplyConfiguration(new RepairServiceItemConfiguration());
            modelBuilder.ApplyConfiguration(new RepairRequestConfiguration());

            // Analytics & Tracking Configurations
            modelBuilder.ApplyConfiguration(new ProductViewConfiguration());
            modelBuilder.ApplyConfiguration(new StockAlertConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());

            // Authentication & Communication Configurations
            modelBuilder.ApplyConfiguration(new VerificationCodeConfiguration());
            modelBuilder.ApplyConfiguration(new EmailQueueConfiguration());

            // Apply any additional configurations from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }
    }
}