using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Table Name
            builder.ToTable("OrderItems");

            // Primary Key
            builder.HasKey(oi => oi.Id);

            // Properties
            builder.Property(oi => oi.Quantity)
                   .IsRequired();


            builder.Property(x => x.UnitPrice)
                   .HasColumnType("decimal(18,2)");
            // Relations
            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.LaptopVariant)
                   .WithMany() // مفيش ICollection<OrderItem> في LaptopVariant
                   .HasForeignKey(oi => oi.LaptopVariantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
