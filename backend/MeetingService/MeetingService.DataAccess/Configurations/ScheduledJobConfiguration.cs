using MeetingService.DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingService.DataAccess.Configurations;

public class ScheduledJobConfiguration : IEntityTypeConfiguration<ScheduledJob>
{
    public void Configure(EntityTypeBuilder<ScheduledJob> builder)
    {
        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Id).IsRequired();
        builder.Property(sc => sc.MeetingId).IsRequired();
        builder.Property(sc => sc.JobId).IsRequired();
        
        builder.Property(sc => sc.ExecuteAt)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .IsRequired();
        
        builder.HasIndex(m => m.MeetingId);
    }
}