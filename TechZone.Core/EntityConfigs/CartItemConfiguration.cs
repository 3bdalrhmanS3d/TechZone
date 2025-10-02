using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Quantity)
                   .IsRequired();

            builder.Property(ci => ci.AddedAt)
                   .IsRequired()
                   .HasDefaultValueSql("now()");

            builder.HasOne(ci => ci.User)
                   .WithMany(u => u.CartItems)
                   .HasForeignKey(ci => ci.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.LaptopVariant)
                   .WithMany(v => v.CartItems)
                   .HasForeignKey(ci => ci.LaptopVariantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}