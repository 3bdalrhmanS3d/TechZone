using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;

namespace TechZone.Infrastructure.EntityConfigs
{
    public class UserDiscountUsageConfiguration : IEntityTypeConfiguration<UserDiscountUsage>
    {
        public void Configure(EntityTypeBuilder<UserDiscountUsage> builder)
        {
            builder.ToTable("UserDiscountUsages");
            builder.HasKey(udu => udu.Id);

            builder.Property(udu => udu.UsedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(udu => udu.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(udu => udu.UpdatedAt)
                   .IsRequired(false);

            builder.Property(udu => udu.DeletedAt)
                   .IsRequired(false);

            builder.Property(udu => udu.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(udu => udu.User)
                   .WithMany(u => u.UserDiscountUsages)
                   .HasForeignKey(udu => udu.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(udu => udu.Discount)
                   .WithMany(d => d.UserDiscountUsages)
                   .HasForeignKey(udu => udu.DiscountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(udu => udu.Order)
                   .WithMany(o => o.UserDiscountUsages)
                   .HasForeignKey(udu => udu.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(udu => !udu.IsDeleted);

            // Indexes
            builder.HasIndex(udu => new { udu.UserId, udu.DiscountId });
            builder.HasIndex(udu => udu.DiscountId);
            builder.HasIndex(udu => udu.OrderId);
        }
    }
}