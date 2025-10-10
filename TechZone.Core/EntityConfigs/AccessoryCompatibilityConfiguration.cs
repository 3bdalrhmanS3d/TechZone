using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class AccessoryCompatibilityConfiguration : IEntityTypeConfiguration<AccessoryCompatibility>
    {
        public void Configure(EntityTypeBuilder<AccessoryCompatibility> builder)
        {
            builder.ToTable("AccessoryCompatibilities");
            builder.HasKey(ac => ac.Id);

            builder.Property(ac => ac.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ac => ac.UpdatedAt)
                   .IsRequired(false);

            builder.Property(ac => ac.DeletedAt)
                   .IsRequired(false);

            builder.Property(ac => ac.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(ac => ac.Accessory)
                   .WithMany(a => a.CompatibleLaptops)
                   .HasForeignKey(ac => ac.AccessoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ac => ac.Laptop)
                   .WithMany(l => l.CompatibleAccessories)
                   .HasForeignKey(ac => ac.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(ac => !ac.IsDeleted);

            // Unique constraint
            builder.HasIndex(ac => new { ac.AccessoryId, ac.LaptopId })
                   .IsUnique();
        }
    }
}