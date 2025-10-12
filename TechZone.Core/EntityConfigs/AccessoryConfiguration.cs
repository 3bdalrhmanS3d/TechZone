using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class AccessoryConfiguration : IEntityTypeConfiguration<Accessory>
    {
        public void Configure(EntityTypeBuilder<Accessory> builder)
        {
            builder.ToTable("Accessories");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.SKU)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(a => a.Description)
                   .HasColumnType("text");

            builder.Property(a => a.CurrentPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(a => a.StockQuantity)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(a => a.ReservedQuantity)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(a => a.ReorderLevel)
                   .HasDefaultValue(5);

            builder.Property(a => a.IsActive)
                   .HasDefaultValue(true);

            builder.Property(a => a.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(a => a.UpdatedAt)
                   .IsRequired(false);

            builder.Property(a => a.DeletedAt)
                   .IsRequired(false);

            builder.Property(a => a.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(a => a.AccessoryType)
                   .WithMany(at => at.Accessories)
                   .HasForeignKey(a => a.AccessoryTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(a => !a.IsDeleted);

            // Indexes
            builder.HasIndex(a => a.SKU).IsUnique();
            builder.HasIndex(a => a.AccessoryTypeId);
        }
    }
}