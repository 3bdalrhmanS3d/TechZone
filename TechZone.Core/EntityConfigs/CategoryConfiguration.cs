using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Laptop;

namespace TechZone.Core.EntityConfigs
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Description)
                   .HasColumnType("text");

            builder.Property(c => c.ImageUrl)
                   .HasMaxLength(500);

            builder.Property(c => c.ParentCategoryId)
                   .IsRequired(false);

            builder.Property(c => c.DisplayOrder)
                   .HasDefaultValue(0);

            builder.Property(c => c.IsActive)
                   .HasDefaultValue(true);

            builder.Property(c => c.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.UpdatedAt)
                   .IsRequired(false);

            builder.Property(c => c.DeletedAt)
                   .IsRequired(false);

            builder.Property(c => c.IsDeleted)
                   .HasDefaultValue(false);

            // Self-referencing relationship for parent category
            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.SubCategories)
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}