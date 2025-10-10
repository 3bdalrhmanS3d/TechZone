using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Code)
                   .HasMaxLength(50);

            builder.Property(d => d.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.Description)
                   .HasColumnType("text");

            builder.Property(d => d.DiscountType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(d => d.Value)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(d => d.MinimumPurchase)
                   .HasColumnType("decimal(18,2)");

            builder.Property(d => d.MaxDiscountAmount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(d => d.UsageLimit)
                   .IsRequired(false);

            builder.Property(d => d.UsageCount)
                   .HasDefaultValue(0);

            builder.Property(d => d.StartDate)
                   .IsRequired();

            builder.Property(d => d.EndDate)
                   .IsRequired();

            builder.Property(d => d.IsActive)
                   .HasDefaultValue(true);

            builder.Property(d => d.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(d => d.UpdatedAt)
                   .IsRequired(false);

            builder.Property(d => d.DeletedAt)
                   .IsRequired(false);

            builder.Property(d => d.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasQueryFilter(d => !d.IsDeleted);

            // Indexes
            builder.HasIndex(d => d.Code).IsUnique();
            builder.HasIndex(d => new { d.IsActive, d.StartDate, d.EndDate });
        }
    }
}