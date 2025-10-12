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

            builder.Property(ci => ci.ProductType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(ci => ci.ProductId)
                   .IsRequired();

            builder.Property(ci => ci.Quantity)
                   .IsRequired();

            builder.Property(ci => ci.AddedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()




            builder.Property(ci => ci.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(ci => ci.UpdatedAt)
                   .IsRequired(false);

            builder.Property(ci => ci.DeletedAt)
                   .IsRequired(false);

            builder.Property(ci => ci.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(ci => ci.User)
                   .WithMany(u => u.CartItems)
                   .HasForeignKey(ci => ci.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(ci => !ci.IsDeleted);

            // Unique constraint - user can only have one cart item per product
            builder.HasIndex(ci => new { ci.UserId, ci.ProductType, ci.ProductId })
                   .IsUnique();
        }
    }
}