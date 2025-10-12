using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;

namespace TechZone.Infrastructure.EntityConfigs
{
    public class LaptopWarrantyConfiguration : IEntityTypeConfiguration<LaptopWarranty>
    {
        public void Configure(EntityTypeBuilder<LaptopWarranty> builder)
        {
            builder.ToTable("LaptopWarranties");
            builder.HasKey(lw => lw.Id);

            builder.Property(lw => lw.DurationMonths)
                   .IsRequired();

            builder.Property(lw => lw.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(lw => lw.Coverage)
                   .HasColumnType("text");

            builder.Property(lw => lw.Provider)
                   .HasMaxLength(100);

            builder.Property(lw => lw.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()




            builder.Property(lw => lw.UpdatedAt)
                   .IsRequired(false);

            builder.Property(lw => lw.DeletedAt)
                   .IsRequired(false);

            builder.Property(lw => lw.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(lw => lw.Laptop)
                   .WithMany(l => l.Warranties)
                   .HasForeignKey(lw => lw.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(lw => !lw.IsDeleted);
        }
    }
}