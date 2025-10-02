using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
    {
        public void Configure(EntityTypeBuilder<VerificationCode> builder)
        {
            builder.ToTable("VerificationCodes", t =>
            {
                t.HasCheckConstraint("CK_VerificationCodes_Expiry_After_Created", "ExpiryDate > CreatedAt");
                t.HasCheckConstraint("CK_VerificationCodes_Attempt_NonNegative", "AttemptCount >= 0");
                t.HasCheckConstraint("CK_VerificationCodes_MaxAttempts_Positive", "MaxAttempts > 0");
            });


            // PK
            builder.HasKey(vc => vc.Id);

            // Id (DB default)
            builder.Property(vc => vc.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired();


            // FK to AspNetUsers (nvarchar(450))
            builder.Property(vc => vc.UserId)
                   .HasMaxLength(450)
                   .IsRequired();

            // Code
            builder.Property(vc => vc.Code)
                   .IsRequired()
                   .HasMaxLength(10);

            // Enums as strings
            builder.Property(vc => vc.Type)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(vc => vc.Destination)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            // Flags and dates
            builder.Property(vc => vc.IsUsed)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(vc => vc.CreatedAt)
                   .HasDefaultValueSql("now()")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(vc => vc.ExpiryDate)
                    .HasDefaultValueSql("now() + interval '60 minutes'")
                    .ValueGeneratedOnAdd()
                    .IsRequired();


            builder.Property(vc => vc.AttemptCount)
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(vc => vc.MaxAttempts)
                   .HasDefaultValue(3)
                   .IsRequired();

            // Relationship
            builder.HasOne(vc => vc.User)
                   .WithMany(u => u.VerificationCodes)
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            // Indexes
            builder.HasIndex(vc => vc.UserId)
                   .HasDatabaseName("IX_VerificationCodes_UserId");

            builder.HasIndex(vc => vc.Code)
                   .HasDatabaseName("IX_VerificationCodes_Code");

            builder.HasIndex(vc => vc.Type)
                   .HasDatabaseName("IX_VerificationCodes_Type");

            builder.HasIndex(vc => vc.ExpiryDate)
                   .HasDatabaseName("IX_VerificationCodes_ExpiryDate");

            builder.HasIndex(vc => new { vc.UserId, vc.Type, vc.IsUsed })
                   .HasDatabaseName("IX_VerificationCodes_UserId_Type_IsUsed");

            builder.HasIndex(vc => new { vc.Code, vc.Type, vc.IsUsed, vc.ExpiryDate })
                   .HasDatabaseName("IX_VerificationCodes_Verification_Query");

            
        }
    }
}
