using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Laptop;

namespace TechZone.Core.EntityConfigs
{
    public class LaptopVariantConfiguration : IEntityTypeConfiguration<LaptopVariant>
    {
        public void Configure(EntityTypeBuilder<LaptopVariant> builder)
        {
            builder.ToTable("LaptopVariants");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.SKU)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(v => v.RAM)
                   .IsRequired();

            builder.Property(v => v.StorageCapacityGB)
                   .IsRequired();

            builder.Property(v => v.StorageType)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(v => v.CurrentPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(v => v.StockQuantity)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(v => v.ReservedQuantity)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(v => v.ReorderLevel)
                   .HasDefaultValue(5);

            builder.Property(v => v.IsActive)
                   .HasDefaultValue(true);

            builder.Property(v => v.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(v => v.UpdatedAt)
                   .IsRequired(false);

            builder.Property(v => v.DeletedAt)
                   .IsRequired(false);

            builder.Property(v => v.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(v => v.Laptop)
                   .WithMany(l => l.Variants)
                   .HasForeignKey(v => v.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(v => !v.IsDeleted);
        }
    }
}