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

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.LaptopVariant)
                   .WithMany(v => v.OrderItems)
                   .HasForeignKey(oi => oi.LaptopVariantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}