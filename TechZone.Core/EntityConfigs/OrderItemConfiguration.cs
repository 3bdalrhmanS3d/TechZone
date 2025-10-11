using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Order;

namespace TechZone.Core.EntityConfigs
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.ProductType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(oi => oi.ProductId)
                   .IsRequired();

            builder.Property(oi => oi.ProductName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(oi => oi.SKU)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.DiscountAmount)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(oi => oi.TotalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(oi => oi.UpdatedAt)
                   .IsRequired(false);

            builder.Property(oi => oi.DeletedAt)
                   .IsRequired(false);

            builder.Property(oi => oi.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(oi => !oi.IsDeleted);

            // Indexes
            builder.HasIndex(oi => oi.OrderId);
            builder.HasIndex(oi => new { oi.ProductType, oi.ProductId });
        }
    }
}