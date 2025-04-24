using MeetingService.DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingService.DataAccess.Configurations;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id).IsRequired();
        builder.Property(u => u.UserId).IsRequired();
        builder.Property(u => u.Title).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Description).HasMaxLength(256).IsRequired();
        
        builder.Property(u => u.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(u => u.CreatedAt)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .IsRequired();
        
        builder.Property(u => u.StartTime)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .IsRequired();
        
        builder.Property(u => u.EndTime)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .IsRequired();
        
        builder
            .HasMany(u => u.Participants)
            .WithOne(p => p.Meeting)
            .HasForeignKey(p => p.MeetingId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(m => m.UserId);
    }
}