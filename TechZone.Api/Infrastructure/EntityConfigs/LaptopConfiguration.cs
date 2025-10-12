using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.Laptop;

namespace TechZone.Infrastructure.EntityConfigs
{
    public class LaptopConfiguration : IEntityTypeConfiguration<Laptop>
    {
        public void Configure(EntityTypeBuilder<Laptop> builder)
        {
            builder.ToTable("Laptops");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.ModelName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(l => l.Processor)
                   .HasMaxLength(100);

            builder.Property(l => l.GPU)
                   .HasMaxLength(100);

            builder.Property(l => l.ScreenSize)
                   .HasMaxLength(50);

            builder.Property(l => l.Ports)
                   .HasMaxLength(300);

            builder.Property(l => l.Description)
                   .HasColumnType("text");

            builder.Property(l => l.Notes)
                   .HasColumnType("text");

            builder.Property(l => l.Warranty)
                   .HasMaxLength(200);

            builder.Property(l => l.HasCamera)
                   .IsRequired();

            builder.Property(l => l.HasKeyboard)
                   .IsRequired();

            builder.Property(l => l.HasTouchScreen)
                   .IsRequired();

            // Relations
            builder.HasMany(l => l.Variants)
                   .WithOne(v => v.Laptop)
                   .HasForeignKey(v => v.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Images)
                   .WithOne(i => i.Laptop)
                   .HasForeignKey(i => i.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Ratings)
                   .WithOne(r => r.Laptop)
                   .HasForeignKey(r => r.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.RepairRequests)
                   .WithOne(r => r.Laptop)
                   .HasForeignKey(r => r.LaptopId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.Brand)
                   .WithMany(b => b.Laptops)
                   .HasForeignKey(l => l.BrandId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Category)
                   .WithMany(c => c.Laptops)
                   .HasForeignKey(l => l.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}