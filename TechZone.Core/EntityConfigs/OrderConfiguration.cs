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

            builder.Property(o => o.OrderDate)
                   .IsRequired()
                   .HasDefaultValueSql("now()"); 

            builder.Property(o => o.TotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.HasOne(o => o.ApplicationUser)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId) // غيّرت إلى UserId
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Items)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Payments)
                   .WithOne(p => p.Order)
                   .HasForeignKey(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Shipping)
                   .WithOne(s => s.Order)
                   .HasForeignKey<Shipping>(s => s.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}