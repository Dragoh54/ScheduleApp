using MeetingService.DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingService.DataAccess.Configurations;

public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).IsRequired();
        builder.Property(u => u.UserId).IsRequired();
        builder.Property(u => u.MeetingId).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Username).HasMaxLength(100).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength(256).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(256).IsRequired();

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.HasIndex(p => new { p.MeetingId, p.UserId }).IsUnique();
    }
}