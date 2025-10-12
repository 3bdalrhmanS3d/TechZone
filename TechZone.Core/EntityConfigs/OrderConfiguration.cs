using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(o => o.OrderType)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(OrderType.Reservation);

            builder.Property(o => o.ReservationAmount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.ReservationExpiryDate)
                   .IsRequired(false);

            builder.Property(o => o.IsReservationCompleted)
                   .HasDefaultValue(false);

            builder.Property(o => o.DeliveryAddress)
                   .HasMaxLength(500);

            builder.Property(o => o.DeliveryCost)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(o => o.DeliveryInstructions)
                   .HasColumnType("text");

            builder.Property(o => o.SubtotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.DiscountAmount)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(o => o.ShippingCost)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(o => o.TaxAmount)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(o => o.TotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(OrderStatus.Pending);

            builder.Property(o => o.CancelReason)
                   .HasColumnType("text");

            builder.Property(o => o.OrderDate)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()



            builder.Property(o => o.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()



            builder.Property(o => o.UpdatedAt)
                   .IsRequired(false);

            builder.Property(o => o.DeletedAt)
                   .IsRequired(false);

            builder.Property(o => o.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(o => !o.IsDeleted);

            // Indexes
            builder.HasIndex(o => o.OrderNumber).IsUnique();
            builder.HasIndex(o => o.UserId);
            builder.HasIndex(o => o.Status);
            builder.HasIndex(o => o.OrderDate);
            builder.HasIndex(o => o.OrderType);
            builder.HasIndex(o => o.ReservationExpiryDate);
        }
    }
}