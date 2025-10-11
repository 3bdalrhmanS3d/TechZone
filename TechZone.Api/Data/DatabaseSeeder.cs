using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechZone.Core.Entities;
using TechZone.EF.Application;


namespace TechZone.Api.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider sp)
        {
            // 0) Roles & Admin
            await SeedRolesAndAdminAsync(sp);

            // DbContext
            var ctx = sp.GetRequiredService<ApplicationDbContext>();

            // 1) Parents / Lookups
            await SeedBrandsAsync(ctx);
            await SeedCategoriesAsync(ctx);
            await SeedAccessoryTypesAsync(ctx);
            await SeedDiscountsAsync(ctx);
            await SeedRepairServiceItemsAsync(ctx);

            // 2) Core Products
            await SeedLaptopsAsync(ctx, total: 50);
            await SeedAccessoriesAsync(ctx, total: 20);

            // 3) Product Details
            await SeedLaptopVariantsAsync(ctx);
            await SeedLaptopImagesAsync(ctx);
            await SeedLaptopPortsAsync(ctx);
            await SeedLaptopWarrantiesAsync(ctx);
            await SeedAccessoryImagesAsync(ctx);
            await SeedAccessoryAttributesAsync(ctx);
            await SeedAccessoryCompatibilitiesAsync(ctx);

            // 4) Pricing & Discounts
            await SeedPriceHistoriesAsync(ctx);
            await SeedProductDiscountsAsync(ctx);

            // 5) Demo Data (Optional)
            await SeedDemoOrdersAndCartItemsAsync(ctx);
            await SeedDemoRatingsAsync(ctx);
        }

        // ====== 0) Identity (Roles + Admin) ======
        private static async Task SeedRolesAndAdminAsync(IServiceProvider sp)
        {
            var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Admin", "User", "Moderator" };
            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

            var adminEmail = "admin@techzone.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FullName = "Admin TechZone",
                    PhoneNumber = "+201234567890",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        // ====== 1) Brands ======
        private static async Task SeedBrandsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.Brands.AnyAsync()) return;

            var brands = new List<Brand>
            {
                new() {
                    Name = "Lenovo",
                    Country = "China",
                    Description = "Innovative computing solutions and technology",
                    LogoUrl = "/images/brands/lenovo.png",
                    IsActive = true
                },
                new() {
                    Name = "HP",
                    Country = "USA",
                    Description = "HP laptops and computing devices",
                    LogoUrl = "/images/brands/hp.png",
                    IsActive = true
                },
                new() {
                    Name = "Dell",
                    Country = "USA",
                    Description = "Dell laptops and workstations",
                    LogoUrl = "/images/brands/dell.png",
                    IsActive = true
                },
                new() {
                    Name = "ASUS",
                    Country = "Taiwan",
                    Description = "ASUS gaming and consumer laptops",
                    LogoUrl = "/images/brands/asus.png",
                    IsActive = true
                },
                new() {
                    Name = "Acer",
                    Country = "Taiwan",
                    Description = "Acer laptops and computing devices",
                    LogoUrl = "/images/brands/acer.png",
                    IsActive = true
                },
                new() {
                    Name = "MSI",
                    Country = "Taiwan",
                    Description = "MSI gaming laptops and hardware",
                    LogoUrl = "/images/brands/msi.png",
                    IsActive = true
                },
                new() {
                    Name = "Apple",
                    Country = "USA",
                    Description = "MacBook laptops and Apple devices",
                    LogoUrl = "/images/brands/apple.png",
                    IsActive = true
                }
            };
            await ctx.Brands.AddRangeAsync(brands);
            await ctx.SaveChangesAsync();
        }

        // ====== 1) Categories ======
        private static async Task SeedCategoriesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.Categories.AnyAsync()) return;

            var categories = new List<Category>
            {
                new() {
                    Name = "Ultrabook",
                    Description = "Thin and light laptops for portability",
                    ImageUrl = "/images/categories/ultrabook.jpg",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new() {
                    Name = "Gaming",
                    Description = "High performance laptops for gaming",
                    ImageUrl = "/images/categories/gaming.jpg",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new() {
                    Name = "Business",
                    Description = "Professional laptops for business use",
                    ImageUrl = "/images/categories/business.jpg",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new() {
                    Name = "Creator",
                    Description = "Laptops optimized for content creation",
                    ImageUrl = "/images/categories/creator.jpg",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new() {
                    Name = "Student",
                    Description = "Affordable laptops for students",
                    ImageUrl = "/images/categories/student.jpg",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new() {
                    Name = "2-in-1",
                    Description = "Convertible laptops and tablets",
                    ImageUrl = "/images/categories/2in1.jpg",
                    DisplayOrder = 6,
                    IsActive = true
                }
            };
            await ctx.Categories.AddRangeAsync(categories);
            await ctx.SaveChangesAsync();
        }

        // ====== 1) Accessory Types ======
        private static async Task SeedAccessoryTypesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.AccessoryTypes.AnyAsync()) return;

            var accessoryTypes = new List<AccessoryType>
            {
                new() { Name = "Laptop Bags", Description = "Bags and cases for laptops" },
                new() { Name = "Chargers", Description = "Power adapters and chargers" },
                new() { Name = "Docking Stations", Description = "Port replicators and docks" },
                new() { Name = "External Storage", Description = "External hard drives and SSDs" },
                new() { Name = "Peripherals", Description = "Mice, keyboards, and other peripherals" },
                new() { Name = "Cooling Pads", Description = "Laptop cooling solutions" },
                new() { Name = "Privacy Screens", Description = "Privacy filters for laptops" }
            };
            await ctx.AccessoryTypes.AddRangeAsync(accessoryTypes);
            await ctx.SaveChangesAsync();
        }

        // ====== 1) Discounts ======
        private static async Task SeedDiscountsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.Discounts.AnyAsync()) return;

            var now = DateTime.UtcNow;

            var discounts = new List<Discount>
            {
                new Discount
                {
                    Code = "BACKTOSCHOOL20",
                    Title = "Back to School 2024",
                    Description = "Special discount for students and educators",
                    DiscountType = DiscountType.Percentage,
                    Value = 20m,
                    MinimumPurchase = 1000m,
                    MaxDiscountAmount = 500m,
                    UsageLimit = 100,
                    StartDate = now.AddDays(-30),
                    EndDate = now.AddDays(45),
                    IsActive = true
                },
                new Discount
                {
                    Code = "SUMMER2024",
                    Title = "Summer Sale",
                    Description = "Summer clearance sale",
                    DiscountType = DiscountType.Percentage,
                    Value = 15m,
                    MinimumPurchase = 500m,
                    MaxDiscountAmount = 300m,
                    UsageLimit = 200,
                    StartDate = now.AddDays(-10),
                    EndDate = now.AddDays(30),
                    IsActive = true
                },
                new Discount
                {
                    Code = "WELCOME10",
                    Title = "Welcome Discount",
                    Description = "First purchase discount for new customers",
                    DiscountType = DiscountType.FixedAmount,
                    Value = 100m,
                    MinimumPurchase = 800m,
                    MaxDiscountAmount = 100m,
                    UsageLimit = null, // Unlimited
                    StartDate = now.AddDays(-100),
                    EndDate = now.AddDays(365),
                    IsActive = true
                }
            };

            await ctx.Discounts.AddRangeAsync(discounts);
            await ctx.SaveChangesAsync();
        }

        // ====== 1) Repair Service Items ======
        private static async Task SeedRepairServiceItemsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.RepairServiceItems.AnyAsync()) return;

            var repairServices = new List<RepairServiceItem>
            {
                new() {
                    Name = "Screen Replacement",
                    RepairType = RepairType.Hardware,
                    Description = "Replace broken or damaged laptop screen",
                    BasePrice = 150m,
                    EstimatedDays = 2,
                    IsActive = true
                },
                new() {
                    Name = "Battery Replacement",
                    RepairType = RepairType.Hardware,
                    Description = "Replace old or faulty laptop battery",
                    BasePrice = 80m,
                    EstimatedDays = 1,
                    IsActive = true
                },
                new() {
                    Name = "Keyboard Repair",
                    RepairType = RepairType.Hardware,
                    Description = "Fix or replace damaged keyboard",
                    BasePrice = 60m,
                    EstimatedDays = 1,
                    IsActive = true
                },
                new() {
                    Name = "Virus Removal",
                    RepairType = RepairType.Software,
                    Description = "Remove viruses and malware",
                    BasePrice = 40m,
                    EstimatedDays = 1,
                    IsActive = true
                },
                new() {
                    Name = "OS Installation",
                    RepairType = RepairType.Software,
                    Description = "Clean operating system installation",
                    BasePrice = 30m,
                    EstimatedDays = 1,
                    IsActive = true
                },
                new() {
                    Name = "Hardware Diagnostic",
                    RepairType = RepairType.Diagnostic,
                    Description = "Comprehensive hardware diagnostic test",
                    BasePrice = 25m,
                    EstimatedDays = 1,
                    IsActive = true
                }
            };
            await ctx.RepairServiceItems.AddRangeAsync(repairServices);
            await ctx.SaveChangesAsync();
        }

        // ====== 2) Laptops ======
        private static async Task SeedLaptopsAsync(ApplicationDbContext ctx, int total)
        {
            if (await ctx.Laptops.AnyAsync()) return;

            var brandIds = await ctx.Brands.Select(b => b.Id).ToListAsync();
            var catIds = await ctx.Categories.Select(c => c.Id).ToListAsync();
            if (brandIds.Count == 0 || catIds.Count == 0) return;

            var rng = new Random(42);
            var cpuPool = new[]
            {
                "Intel Core i5-1240P", "Intel Core i7-1360P", "Intel Core i7-13700H",
                "AMD Ryzen 5 7640U", "AMD Ryzen 7 7840HS", "Apple M2", "Apple M3",
                "Intel Core i9-13900H", "AMD Ryzen 9 7940HS"
            };
            var gpuPool = new[]
            {
                "Intel Iris Xe", "NVIDIA RTX 3050", "NVIDIA RTX 4050",
                "NVIDIA RTX 4060", "NVIDIA RTX 4070", "AMD Radeon 780M",
                "Apple Integrated 10-core GPU"
            };
            var screenPool = new[] { "13.3\" FHD", "14\" QHD", "15.6\" FHD", "16\" QHD+", "17.3\" FHD" };

            var laptops = new List<Laptop>(capacity: total);
            for (int i = 0; i < total; i++)
            {
                var brandId = brandIds[i % brandIds.Count];
                var catId = catIds[i % catIds.Count];
                var cpu = cpuPool[rng.Next(cpuPool.Length)];
                var gpu = gpuPool[rng.Next(gpuPool.Length)];
                var screen = screenPool[rng.Next(screenPool.Length)];
                var brandName = await ctx.Brands.Where(b => b.Id == brandId).Select(b => b.Name).FirstAsync();

                laptops.Add(new Laptop
                {
                    ModelName = $"{brandName} Model {i + 1:00}",
                    Processor = cpu,
                    GPU = gpu,
                    ScreenSize = screen,
                    HasCamera = true,
                    HasKeyboard = true,
                    HasTouchScreen = rng.NextDouble() < 0.3, // 30% chance
                    Description = $"High-performance {brandName} laptop with {cpu} and {gpu}. Perfect for {ctx.Categories.Find(catId)?.Name.ToLower()} use.",
                    StoreLocation = "TechZone Main Store, Cairo",
                    StoreContact = "+20123456789",
                    ReleaseYear = rng.Next(2021, 2025),
                    BrandId = brandId,
                    CategoryId = catId,
                    IsActive = true
                });
            }

            await ctx.Laptops.AddRangeAsync(laptops);
            await ctx.SaveChangesAsync();
        }

        // ====== 2) Accessories ======
        private static async Task SeedAccessoriesAsync(ApplicationDbContext ctx, int total)
        {
            if (await ctx.Accessories.AnyAsync()) return;

            var accessoryTypeIds = await ctx.AccessoryTypes.Select(at => at.Id).ToListAsync();
            if (accessoryTypeIds.Count == 0) return;

            var rng = new Random(42);
            var accessoryNames = new[]
            {
                "Professional Laptop Bag", "USB-C Universal Charger", "Thunderbolt 4 Dock",
                "External SSD 1TB", "Wireless Mouse", "Laptop Cooling Pad",
                "Privacy Screen Filter", "Laptop Sleeve", "Multi-port Adapter",
                "Laptop Stand", "External Monitor", "Webcam"
            };

            var accessories = new List<Accessory>();
            for (int i = 0; i < total && i < accessoryNames.Length; i++)
            {
                var typeId = accessoryTypeIds[rng.Next(accessoryTypeIds.Count)];

                accessories.Add(new Accessory
                {
                    SKU = $"ACC{i + 1:000}",
                    Name = accessoryNames[i],
                    Description = $"High-quality {accessoryNames[i].ToLower()} for all your computing needs.",
                    AccessoryTypeId = typeId,
                    CurrentPrice = rng.Next(20, 300),
                    StockQuantity = rng.Next(10, 100),
                    ReservedQuantity = 0,
                    ReorderLevel = 5,
                    IsActive = true
                });
            }

            await ctx.Accessories.AddRangeAsync(accessories);
            await ctx.SaveChangesAsync();
        }

        // ====== 3) Laptop Variants ======
        private static async Task SeedLaptopVariantsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.LaptopVariants.AnyAsync()) return;

            var laptopIds = await ctx.Laptops.Select(l => l.Id).ToListAsync();
            var rng = new Random(42);

            var variants = new List<LaptopVariant>();
            foreach (var laptopId in laptopIds)
            {
                // Create 1-3 variants per laptop
                var variantCount = rng.Next(1, 4);
                for (int i = 0; i < variantCount; i++)
                {
                    var ram = new[] { 8, 16, 32, 64 }[rng.Next(4)];
                    var storage = new[] { 256, 512, 1024, 2048 }[rng.Next(4)];
                    var storageType = new[] { "SSD", "NVMe SSD" }[rng.Next(2)];

                    variants.Add(new LaptopVariant
                    {
                        LaptopId = laptopId,
                        SKU = $"LAP{laptopId:000}V{i + 1}",
                        RAM = ram,
                        StorageCapacityGB = storage,
                        StorageType = storageType,
                        CurrentPrice = 800 + (ram / 8 * 100) + (storage / 256 * 50) + rng.Next(-50, 100),
                        StockQuantity = rng.Next(5, 25),
                        ReservedQuantity = 0,
                        ReorderLevel = 3,
                        IsActive = true
                    });
                }
            }

            await ctx.LaptopVariants.AddRangeAsync(variants);
            await ctx.SaveChangesAsync();
        }

        // ====== 3) Laptop Images ======
        private static async Task SeedLaptopImagesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.LaptopImages.AnyAsync()) return;

            var laptopIds = await ctx.Laptops.Select(l => l.Id).ToListAsync();
            var images = new List<LaptopImage>();

            foreach (var laptopId in laptopIds)
            {
                images.Add(new LaptopImage
                {
                    LaptopId = laptopId,
                    ImageUrl = "/images/laptops/default-laptop.jpg",
                    IsMain = true,
                    DisplayOrder = 1,
                    UploadedAt = DateTime.UtcNow
                });

                // Add 1-2 additional images
                var rng = new Random(laptopId);
                var extraImages = rng.Next(1, 3);
                for (int i = 0; i < extraImages; i++)
                {
                    images.Add(new LaptopImage
                    {
                        LaptopId = laptopId,
                        ImageUrl = $"/images/laptops/laptop-{laptopId}-{i + 1}.jpg",
                        IsMain = false,
                        DisplayOrder = i + 2,
                        UploadedAt = DateTime.UtcNow
                    });
                }
            }

            await ctx.LaptopImages.AddRangeAsync(images);
            await ctx.SaveChangesAsync();
        }

        // ====== 3) Laptop Ports ======
        private static async Task SeedLaptopPortsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.LaptopPorts.AnyAsync()) return;

            var laptopIds = await ctx.Laptops.Select(l => l.Id).ToListAsync();
            var rng = new Random(42);

            var ports = new List<LaptopPort>();
            var portTypes = new[] { "USB-C", "USB-A", "HDMI", "Thunderbolt 4", "Audio Jack", "SD Card Reader" };

            foreach (var laptopId in laptopIds)
            {
                foreach (var portType in portTypes)
                {
                    if (rng.NextDouble() > 0.3) // 70% chance to include each port type
                    {
                        ports.Add(new LaptopPort
                        {
                            LaptopId = laptopId,
                            PortType = portType,
                            Quantity = portType == "USB-C" || portType == "USB-A" ? rng.Next(2, 4) : 1
                        });
                    }
                }
            }

            await ctx.LaptopPorts.AddRangeAsync(ports);
            await ctx.SaveChangesAsync();
        }

        // ====== 3) Laptop Warranties ======
        private static async Task SeedLaptopWarrantiesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.LaptopWarranties.AnyAsync()) return;

            var laptopIds = await ctx.Laptops.Select(l => l.Id).ToListAsync();
            var rng = new Random(42);

            var warranties = laptopIds.Select(laptopId => new LaptopWarranty
            {
                LaptopId = laptopId,
                DurationMonths = new[] { 12, 24, 36 }[rng.Next(3)],
                Type = "International Warranty",
                Coverage = "Hardware defects and manufacturing issues",
                Provider = "TechZone Warranty Services"
            }).ToList();

            await ctx.LaptopWarranties.AddRangeAsync(warranties);
            await ctx.SaveChangesAsync();
        }

        // ====== 3) Accessory Images ======
        private static async Task SeedAccessoryImagesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.AccessoryImages.AnyAsync()) return;

            var accessoryIds = await ctx.Accessories.Select(a => a.Id).ToListAsync();
            var images = accessoryIds.Select(accessoryId => new AccessoryImage
            {
                AccessoryId = accessoryId,
                ImageUrl = "/images/accessories/default-accessory.jpg",
                IsMain = true,
                DisplayOrder = 1,
                UploadedAt = DateTime.UtcNow
            }).ToList();

            await ctx.AccessoryImages.AddRangeAsync(images);
            await ctx.SaveChangesAsync();
        }

        // ====== 3) Accessory Attributes ======
        private static async Task SeedAccessoryAttributesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.AccessoryAttributes.AnyAsync()) return;

            var accessoryIds = await ctx.Accessories.Select(a => a.Id).ToListAsync();
            var rng = new Random(42);

            var attributes = new List<AccessoryAttribute>();
            foreach (var accessoryId in accessoryIds)
            {
                var accessory = await ctx.Accessories.FindAsync(accessoryId);
                if (accessory?.Name.Contains("Bag") == true || accessory?.Name.Contains("Sleeve") == true)
                {
                    attributes.AddRange(new[]
                    {
                        new AccessoryAttribute { AccessoryId = accessoryId, AttributeKey = "Material", AttributeValue = "Nylon" },
                        new AccessoryAttribute { AccessoryId = accessoryId, AttributeKey = "Compatibility", AttributeValue = "Up to 15.6\" laptops" },
                        new AccessoryAttribute { AccessoryId = accessoryId, AttributeKey = "Color", AttributeValue = "Black" }
                    });
                }
                else if (accessory?.Name.Contains("Charger") == true)
                {
                    attributes.AddRange(new[]
                    {
                        new AccessoryAttribute { AccessoryId = accessoryId, AttributeKey = "Wattage", AttributeValue = "65W" },
                        new AccessoryAttribute { AccessoryId = accessoryId, AttributeKey = "Connector", AttributeValue = "USB-C" },
                        new AccessoryAttribute { AccessoryId = accessoryId, AttributeKey = "Cable Length", AttributeValue = "1.8m" }
                    });
                }
            }

            await ctx.AccessoryAttributes.AddRangeAsync(attributes);
            await ctx.SaveChangesAsync();
        }

        // ====== 3) Accessory Compatibilities ======
        private static async Task SeedAccessoryCompatibilitiesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.AccessoryCompatibilities.AnyAsync()) return;

            var accessoryIds = await ctx.Accessories.Select(a => a.Id).ToListAsync();
            var laptopIds = await ctx.Laptops.Select(l => l.Id).Take(10).ToListAsync(); // Limit for demo
            var rng = new Random(42);

            var compatibilities = new List<AccessoryCompatibility>();
            foreach (var accessoryId in accessoryIds)
            {
                // Each accessory is compatible with 3-8 random laptops
                var compatibleLaptops = laptopIds.OrderBy(x => rng.Next()).Take(rng.Next(3, 9)).ToList();
                foreach (var laptopId in compatibleLaptops)
                {
                    compatibilities.Add(new AccessoryCompatibility
                    {
                        AccessoryId = accessoryId,
                        LaptopId = laptopId
                    });
                }
            }

            await ctx.AccessoryCompatibilities.AddRangeAsync(compatibilities);
            await ctx.SaveChangesAsync();
        }

        // ====== 4) Price Histories ======
        private static async Task SeedPriceHistoriesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.PriceHistories.AnyAsync()) return;

            var laptopVariants = await ctx.LaptopVariants.Take(20).ToListAsync();
            var accessories = await ctx.Accessories.Take(10).ToListAsync();
            var rng = new Random(42);

            var priceHistories = new List<PriceHistory>();
            var adminUserId = (await ctx.Users.FirstAsync(u => u.UserName == "admin")).Id;

            // Laptop variant price histories
            foreach (var variant in laptopVariants)
            {
                var oldPrice = variant.CurrentPrice * (1 + (decimal)(rng.NextDouble() * 0.2)); // 0-20% higher
                priceHistories.Add(new PriceHistory
                {
                    ProductType = ProductType.LaptopVariant,
                    ProductId = variant.Id,
                    OldPrice = oldPrice,
                    NewPrice = variant.CurrentPrice,
                    ChangeReason = "Regular price adjustment",
                    EffectiveFrom = DateTime.UtcNow.AddDays(-rng.Next(1, 30)),
                    ChangedByUserId = adminUserId
                });
            }

            // Accessory price histories
            foreach (var accessory in accessories)
            {
                var oldPrice = accessory.CurrentPrice * (1 + (decimal)(rng.NextDouble() * 0.15)); // 0-15% higher
                priceHistories.Add(new PriceHistory
                {
                    ProductType = ProductType.Accessory,
                    ProductId = accessory.Id,
                    OldPrice = oldPrice,
                    NewPrice = accessory.CurrentPrice,
                    ChangeReason = "Seasonal discount",
                    EffectiveFrom = DateTime.UtcNow.AddDays(-rng.Next(1, 15)),
                    ChangedByUserId = adminUserId
                });
            }

            await ctx.PriceHistories.AddRangeAsync(priceHistories);
            await ctx.SaveChangesAsync();
        }

        // ====== 4) Product Discounts ======
        private static async Task SeedProductDiscountsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.ProductDiscounts.AnyAsync()) return;

            var discounts = await ctx.Discounts.ToListAsync();
            var laptopVariants = await ctx.LaptopVariants.Take(15).ToListAsync();
            var accessories = await ctx.Accessories.Take(10).ToListAsync();
            var rng = new Random(42);

            var productDiscounts = new List<ProductDiscount>();

            // Apply discounts to specific products
            foreach (var discount in discounts)
            {
                // Apply to 3-5 laptop variants
                var discountedLaptops = laptopVariants.OrderBy(x => rng.Next()).Take(rng.Next(3, 6)).ToList();
                foreach (var variant in discountedLaptops)
                {
                    productDiscounts.Add(new ProductDiscount
                    {
                        DiscountId = discount.Id,
                        ProductType = ProductType.LaptopVariant,
                        ProductId = variant.Id
                    });
                }

                // Apply to 2-4 accessories
                var discountedAccessories = accessories.OrderBy(x => rng.Next()).Take(rng.Next(2, 5)).ToList();
                foreach (var accessory in discountedAccessories)
                {
                    productDiscounts.Add(new ProductDiscount
                    {
                        DiscountId = discount.Id,
                        ProductType = ProductType.Accessory,
                        ProductId = accessory.Id
                    });
                }
            }

            await ctx.ProductDiscounts.AddRangeAsync(productDiscounts);
            await ctx.SaveChangesAsync();
        }

        // ====== 5) Demo Orders and Cart Items ======
        private static async Task SeedDemoOrdersAndCartItemsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.Orders.AnyAsync()) return;

            var adminUser = await ctx.Users.FirstAsync(u => u.UserName == "admin");
            var laptopVariants = await ctx.LaptopVariants.Take(5).ToListAsync();
            var accessories = await ctx.Accessories.Take(3).ToListAsync();
            var rng = new Random(42);

            // Create a demo order
            var order = new Order
            {
                OrderNumber = $"ORD{DateTime.UtcNow:yyyyMMddHHmmss}",
                UserId = adminUser.Id,
                OrderType = OrderType.Reservation,
                ReservationAmount = 500m,
                ReservationExpiryDate = DateTime.UtcNow.AddDays(30),
                SubtotalAmount = 2500m,
                DiscountAmount = 250m,
                ShippingCost = 0m,
                TaxAmount = 225m,
                TotalAmount = 2475m,
                Status = OrderStatus.Reserved,
                OrderDate = DateTime.UtcNow.AddDays(-1)
            };

            ctx.Orders.Add(order);
            await ctx.SaveChangesAsync();

            // Add order items
            var orderItems = new List<OrderItem>();
            foreach (var variant in laptopVariants.Take(2))
            {
                orderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductType = ProductType.LaptopVariant,
                    ProductId = variant.Id,
                    ProductName = $"Laptop - {variant.SKU}",
                    SKU = variant.SKU,
                    Quantity = 1,
                    UnitPrice = variant.CurrentPrice,
                    DiscountAmount = variant.CurrentPrice * 0.1m, // 10% discount
                    TotalPrice = variant.CurrentPrice * 0.9m
                });
            }

            foreach (var accessory in accessories.Take(1))
            {
                orderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductType = ProductType.Accessory,
                    ProductId = accessory.Id,
                    ProductName = accessory.Name,
                    SKU = accessory.SKU,
                    Quantity = 2,
                    UnitPrice = accessory.CurrentPrice,
                    DiscountAmount = 0,
                    TotalPrice = accessory.CurrentPrice * 2
                });
            }

            ctx.OrderItems.AddRange(orderItems);
            await ctx.SaveChangesAsync();

            // Add some cart items
            var cartItems = new List<CartItem>
            {
                new() {
                    UserId = adminUser.Id,
                    ProductType = ProductType.LaptopVariant,
                    ProductId = laptopVariants[2].Id,
                    Quantity = 1,
                    AddedAt = DateTime.UtcNow
                },
                new() {
                    UserId = adminUser.Id,
                    ProductType = ProductType.Accessory,
                    ProductId = accessories[1].Id,
                    Quantity = 1,
                    AddedAt = DateTime.UtcNow.AddHours(-2)
                }
            };

            ctx.CartItems.AddRange(cartItems);
            await ctx.SaveChangesAsync();
        }

        // ====== 5) Demo Ratings ======
        private static async Task SeedDemoRatingsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.Ratings.AnyAsync()) return;

            var adminUser = await ctx.Users.FirstAsync(u => u.UserName == "admin");
            var laptopVariants = await ctx.LaptopVariants.Take(10).ToListAsync();
            var accessories = await ctx.Accessories.Take(5).ToListAsync();
            var rng = new Random(42);

            var ratings = new List<Rating>();

            // Rate some laptop variants
            foreach (var variant in laptopVariants)
            {
                ratings.Add(new Rating
                {
                    UserId = adminUser.Id,
                    ProductType = ProductType.LaptopVariant,
                    ProductId = variant.Id,
                    Stars = rng.Next(4, 6), // 4 or 5 stars
                    Comment = "Great product! Excellent performance and build quality.",
                    IsVerifiedPurchase = true
                });
            }

            // Rate some accessories
            foreach (var accessory in accessories)
            {
                ratings.Add(new Rating
                {
                    UserId = adminUser.Id,
                    ProductType = ProductType.Accessory,
                    ProductId = accessory.Id,
                    Stars = rng.Next(3, 6), // 3-5 stars
                    Comment = "Good accessory, works as expected.",
                    IsVerifiedPurchase = true
                });
            }

            ctx.Ratings.AddRange(ratings);
            await ctx.SaveChangesAsync();
        }
    }
}