using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;

namespace TechZone.Infrastructure.EntityConfigs
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.Description)
                   .HasColumnType("text");

            builder.Property(d => d.Percentage)
                   .IsRequired()
                   .HasColumnType("decimal(5,2)");

            builder.Property(d => d.StartDate)
                   .IsRequired();

            builder.Property(d => d.EndDate)
                   .IsRequired();

            builder.Property(d => d.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);
        }
    }
}