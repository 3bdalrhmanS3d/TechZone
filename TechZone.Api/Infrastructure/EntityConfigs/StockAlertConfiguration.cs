using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;

namespace TechZone.Infrastructure.EntityConfigs
{
    public class StockAlertConfiguration : IEntityTypeConfiguration<StockAlert>
    {
        public void Configure(EntityTypeBuilder<StockAlert> builder)
        {
            builder.ToTable("StockAlerts");
            builder.HasKey(sa => sa.Id);

            builder.Property(sa => sa.ProductType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(sa => sa.ProductId)
                   .IsRequired();

            builder.Property(sa => sa.IsNotified)
                   .HasDefaultValue(false);

            builder.Property(sa => sa.NotifiedAt)
                   .IsRequired(false);

            builder.Property(sa => sa.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(sa => sa.UpdatedAt)
                   .IsRequired(false);

            builder.Property(sa => sa.DeletedAt)
                   .IsRequired(false);

            builder.Property(sa => sa.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(sa => sa.User)
                   .WithMany(u => u.StockAlerts)
                   .HasForeignKey(sa => sa.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(sa => !sa.IsDeleted);

            // Unique constraint - user can only have one alert per product
            builder.HasIndex(sa => new { sa.UserId, sa.ProductType, sa.ProductId })
                   .IsUnique();

            // Index
            builder.HasIndex(sa => sa.IsNotified);
        }
    }
}