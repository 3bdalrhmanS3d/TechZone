using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class EmailQueueConfiguration : IEntityTypeConfiguration<EmailQueue>
    {
        public void Configure(EntityTypeBuilder<EmailQueue> builder)
        {
            builder.ToTable("EmailQueues");
            builder.HasKey(eq => eq.Id);

            // Properties
            builder.Property(eq => eq.ToEmail)
                .IsRequired();

            builder.Property(eq => eq.ToName)
                .IsRequired();

            builder.Property(eq => eq.Subject)
                .IsRequired();

            builder.Property(eq => eq.Body)
                .IsRequired();

            builder.Property(eq => eq.EmailType)
                .IsRequired();

            builder.Property(eq => eq.Status)
                .IsRequired();

            builder.Property(eq => eq.Priority)
                .IsRequired();

            builder.Property(eq => eq.ErrorMessage);


            builder.Property(eq => eq.TemplateData)
                .HasColumnType("nvarchar(max)");

            // Performance Indexes
            builder.HasIndex(eq => eq.Status)
                .HasDatabaseName("IX_EmailQueues_Status");

            builder.HasIndex(eq => eq.EmailType)
                .HasDatabaseName("IX_EmailQueues_EmailType");

            builder.HasIndex(eq => eq.Priority)
                .HasDatabaseName("IX_EmailQueues_Priority");

            builder.HasIndex(eq => eq.CreatedAt)
                .HasDatabaseName("IX_EmailQueues_CreatedAt");

            builder.HasIndex(eq => eq.ScheduledAt)
                .HasDatabaseName("IX_EmailQueues_ScheduledAt");

            builder.HasIndex(eq => eq.NextRetryAt)
                .HasDatabaseName("IX_EmailQueues_NextRetryAt");

            builder.HasIndex(eq => new { eq.Status, eq.Priority, eq.CreatedAt })
                .HasDatabaseName("IX_EmailQueues_Status_Priority_CreatedAt");

            builder.HasIndex(eq => new { eq.Status, eq.NextRetryAt })
                .HasDatabaseName("IX_EmailQueues_Status_NextRetryAt");

            builder.HasOne(eq => eq.User)
                .WithMany(u => u.EmailQueue)
                .HasForeignKey(eq => eq.UserId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}