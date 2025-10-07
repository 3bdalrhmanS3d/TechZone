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
            await SeedDiscountsAsync(ctx);

            // 2) Core
            await SeedLaptopsAsync(ctx, total: 50);

            // 3) Children
            await SeedLaptopImagesAsync(ctx);
        }

        // ====== 0) Identity (Roles + Admin) ======
        private static async Task SeedRolesAndAdminAsync(IServiceProvider sp)
        {
            var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Admin", "User" };
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
                    FullName = "TechZone Admin",
                    EmailConfirmed = true
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
                new() { Name = "Lenovo", Description = "Lenovo laptops" },
                new() { Name = "HP", Description = "HP laptops" },
                new() { Name = "Dell", Description = "Dell laptops" },
                new() { Name = "ASUS", Description = "ASUS laptops" },
                new() { Name = "Acer", Description = "Acer laptops" },
                new() { Name = "MSI", Description = "MSI gaming" },
                new() { Name = "Apple", Description = "Mac laptops" },
                new() { Name = "Microsoft", Description = "Surface devices" },
                new() { Name = "Razer", Description = "Gaming laptops" },
                new() { Name = "Toshiba", Description = "Dynabook" },
                new() { Name = "Samsung", Description = "Galaxy Book" },
                new() { Name = "Huawei", Description = "MateBook" },
                new() { Name = "LG", Description = "Gram series" },
                new() { Name = "Xiaomi", Description = "Mi Notebook" },
                new() { Name = "Gigabyte", Description = "AERO/AORUS" },
            };
            await ctx.Brands.AddRangeAsync(brands);
            await ctx.SaveChangesAsync();
        }

        // ====== 1) Categories ======
        private static async Task SeedCategoriesAsync(ApplicationDbContext ctx)
        {
            if (await ctx.Categories.AnyAsync()) return;

            var cats = new List<Category>
            {
                new() { Name = "Ultrabook",   Description = "Thin & light" },
                new() { Name = "Gaming",      Description = "High performance" },
                new() { Name = "Business",    Description = "Office/Enterprise" },
                new() { Name = "Creator",     Description = "Content creation" },
                new() { Name = "Student",     Description = "Study & basics" },
                new() { Name = "2-in-1",      Description = "Convertible" },
                new() { Name = "Workstation", Description = "Heavy-duty" },
                new() { Name = "Budget",      Description = "Entry level value" },
            };
            await ctx.Categories.AddRangeAsync(cats);
            await ctx.SaveChangesAsync();
        }

        private static async Task SeedDiscountsAsync(ApplicationDbContext ctx)
        {
            if (await ctx.Discounts.AnyAsync()) return;

            var now = DateTime.UtcNow;

            var discounts = new[]
            {
                new Discount
                {
                    Title = "Back to School",
                    Description = "خصم العودة للمدارس",
                    Percentage = 10m,            // 0–100
                    StartDate = now.AddDays(-30),
                    EndDate   = now.AddDays(45),
                    IsActive  = true
                },
                new Discount
                {
                    Title = "Clearance",
                    Description = "تصفية مخزون",
                    Percentage = 20m,
                    StartDate = now.AddDays(-10),
                    EndDate   = now.AddDays(10),
                    IsActive  = true
                },
                new Discount
                {
                    Title = "Weekend",
                    Description = "خصم نهاية الأسبوع",
                    Percentage = 5m,
                    StartDate = now.AddDays(-1),
                    EndDate   = now.AddDays(2),
                    IsActive  = true
                }
            };

            // optional: فلترة أي إدخالات خطأ (EndDate <= StartDate)
            discounts = discounts.Where(d => d.EndDate > d.StartDate).ToArray();

            await ctx.Discounts.AddRangeAsync(discounts);
            await ctx.SaveChangesAsync();
        }


        // ====== 2) Laptops (50 ) ======
        private static async Task SeedLaptopsAsync(ApplicationDbContext ctx, int total)
        {
            if (await ctx.Laptops.AnyAsync()) return;

            var brandIds = await ctx.Brands.Select(b => b.Id).ToListAsync();
            var catIds = await ctx.Categories.Select(c => c.Id).ToListAsync();
            if (brandIds.Count == 0 || catIds.Count == 0) return;

            var rng = new Random(42);
            var cpuPool = new[]
            {
                "Intel Core i5-1240P","Intel Core i7-1360P","Intel Core i7-13700H",
                "AMD Ryzen 5 7640U","AMD Ryzen 7 7840HS","Apple M2","Apple M3"
            };
            var gpuPool = new[]
            {
                "Intel Iris Xe","NVIDIA RTX 3050","NVIDIA RTX 4050",
                "NVIDIA RTX 4060","AMD Radeon 780M","Apple Integrated"
            };
            var screenPool = new[] { "13.3\"", "14\"", "15.6\"", "16\"", "17\"" };
            var portsPool = new[]
            {
                "2xUSB-A, 1xUSB-C, HDMI, Audio",
                "2xTB4, 1xUSB-A, HDMI, Audio",
                "USB-C, USB-A, RJ-45, HDMI",
                "2xUSB-C, SD, HDMI"
            };
            var warranties = new[] { "1 Year", "2 Years" };

            var laptops = new List<Laptop>(capacity: total);
            for (int i = 0; i < total; i++)
            {
                var brandId = brandIds[i % brandIds.Count];
                var catId = catIds[i % catIds.Count];
                var cpu = cpuPool[rng.Next(cpuPool.Length)];
                var gpu = gpuPool[rng.Next(gpuPool.Length)];
                var screen = screenPool[rng.Next(screenPool.Length)];
                var ports = portsPool[rng.Next(portsPool.Length)];
                var warranty = warranties[rng.Next(warranties.Length)];
                var brandName = await ctx.Brands.Where(b => b.Id == brandId).Select(b => b.Name).FirstAsync();

                laptops.Add(new Laptop
                {
                    ModelName = $"{brandName} Model {i + 1:00}",
                    Processor = cpu,
                    GPU = gpu,
                    ScreenSize = screen,
                    HasCamera = true,
                    HasKeyboard = true,
                    HasTouchScreen = rng.NextDouble() < 0.25,
                    Ports = ports,
                    Description = "Auto-seeded laptop",
                    Notes = "",
                    Warranty = warranty,
                    BrandId = brandId,
                    CategoryId = catId
                });
            }

            await ctx.Laptops.AddRangeAsync(laptops);
            await ctx.SaveChangesAsync();
        }

        // ====== 3) Laptop Images ======
        private static async Task SeedLaptopImagesAsync(ApplicationDbContext ctx)
        {
            // 🔥 Clear existing images first
            if (await ctx.LaptopImages.AnyAsync())
            {
                ctx.LaptopImages.RemoveRange(ctx.LaptopImages);
                await ctx.SaveChangesAsync();
            }

            var laptopIds = await ctx.Laptops.Select(l => l.Id).ToListAsync();
            if (laptopIds.Count == 0) return;

            var cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            cloudinary.Api.Secure = true;

            string imageFolder = Path.Combine("wwwroot", "PICs", "LapTops");
            var localImages = Directory.GetFiles(imageFolder, "*.webp").ToList();

            var rng = new Random();
            var images = new List<LaptopImage>();

            foreach (var laptopId in laptopIds)
            {
                var imagePath = localImages[rng.Next(localImages.Count)];

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(imagePath),
                    Folder = "techzone/laptops"
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                var url = uploadResult.SecureUrl?.ToString() ?? "";

                images.Add(new LaptopImage
                {
                    LaptopId = laptopId,
                    ImageUrl = url,
                    IsMain = true
                });
            }

            await ctx.LaptopImages.AddRangeAsync(images);
            await ctx.SaveChangesAsync();
        }



    }
}
