using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities.Laptop;

namespace TechZone.Core.EntityConfigs
{
    public class LaptopImageConfiguration : IEntityTypeConfiguration<LaptopImage>
    {
        public void Configure(EntityTypeBuilder<LaptopImage> builder)
        {
            builder.ToTable("LaptopImages");
            builder.HasKey(li => li.Id);

            builder.Property(li => li.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(li => li.IsMain)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(li => li.UploadedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(li => li.Laptop)
                   .WithMany(l => l.Images)
                   .HasForeignKey(li => li.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}