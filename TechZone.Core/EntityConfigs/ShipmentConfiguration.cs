using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.ToTable("Shipments");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Carrier)
                   .HasMaxLength(100);

            builder.Property(s => s.TrackingNumber)
                   .HasMaxLength(100);

            builder.Property(s => s.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(ShipmentStatus.Preparing);

            builder.Property(s => s.ShippedAt)
                   .IsRequired(false);

            builder.Property(s => s.EstimatedDelivery)
                   .IsRequired(false);

            builder.Property(s => s.DeliveredAt)
                   .IsRequired(false);

            builder.Property(s => s.FailedReason)
                   .HasColumnType("text");

            builder.Property(s => s.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.UpdatedAt)
                   .IsRequired(false);

            builder.Property(s => s.DeletedAt)
                   .IsRequired(false);

            builder.Property(s => s.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(s => s.Order)
                   .WithOne(o => o.Shipment)
                   .HasForeignKey<Shipment>(s => s.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(s => !s.IsDeleted);

            // Indexes
            builder.HasIndex(s => s.OrderId);
            builder.HasIndex(s => s.TrackingNumber).IsUnique();
            builder.HasIndex(s => s.Status);
        }
    }
}