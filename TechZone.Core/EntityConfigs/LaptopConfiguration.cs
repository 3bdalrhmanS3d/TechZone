using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
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
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(l => l.GPU)
                   .HasMaxLength(100);

            builder.Property(l => l.ScreenSize)
                   .HasMaxLength(50);

            builder.Property(l => l.Description)
                   .HasColumnType("text");

            builder.Property(l => l.StoreLocation)
                   .HasMaxLength(200);

            builder.Property(l => l.StoreContact)
                   .HasMaxLength(100);

            builder.Property(l => l.ReleaseYear)
                   .IsRequired(false);

            builder.Property(l => l.IsActive)
                   .HasDefaultValue(true);

            builder.Property(l => l.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(l => l.UpdatedAt)
                   .IsRequired(false);

            builder.Property(l => l.DeletedAt)
                   .IsRequired(false);

            builder.Property(l => l.IsDeleted)
                   .HasDefaultValue(false);

            // Relations
            builder.HasOne(l => l.Brand)
                   .WithMany(b => b.Laptops)
                   .HasForeignKey(l => l.BrandId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Category)
                   .WithMany(c => c.Laptops)
                   .HasForeignKey(l => l.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(l => !l.IsDeleted);
        }
    }
}