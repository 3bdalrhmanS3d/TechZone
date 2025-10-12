using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;

namespace TechZone.Infrastructure.EntityConfigs
{
    public class ProductDiscountConfiguration : IEntityTypeConfiguration<ProductDiscount>
    {
        public void Configure(EntityTypeBuilder<ProductDiscount> builder)
        {
            builder.ToTable("ProductDiscounts");
            builder.HasKey(pd => pd.Id);

            builder.Property(pd => pd.ProductType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(pd => pd.ProductId)
                   .IsRequired();

            builder.Property(pd => pd.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(pd => pd.UpdatedAt)
                   .IsRequired(false);

            builder.Property(pd => pd.DeletedAt)
                   .IsRequired(false);

            builder.Property(pd => pd.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(pd => pd.Discount)
                   .WithMany(d => d.ProductDiscounts)
                   .HasForeignKey(pd => pd.DiscountId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(pd => !pd.IsDeleted);

            // Unique constraint
            builder.HasIndex(pd => new { pd.DiscountId, pd.ProductType, pd.ProductId })
                   .IsUnique();
        }
    }
}