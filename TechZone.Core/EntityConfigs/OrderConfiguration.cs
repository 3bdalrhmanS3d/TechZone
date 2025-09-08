using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Table Name
            builder.ToTable("Orders");

            // Primary Key
            builder.HasKey(o => o.Id);

            // Properties
            builder.Property(o => o.OrderDate)
                   .HasDefaultValueSql("GETDATE()") // يحط التاريخ الحالي أوتوماتيك
                   .IsRequired();


            builder.Property(o => o.Status)
                   .HasMaxLength(50)
                   .IsRequired();


            builder.Property(x => x.TotalAmount)
                   .HasColumnType("decimal(18,2)");

            // Relations
            builder.HasOne(o => o.User)
                   .WithMany() // ممكن تعمل WithMany(u => u.Orders) لو عندك Orders في ApplicationUser
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Items)
                   .WithOne(i => i.Order)
                   .HasForeignKey(i => i.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
