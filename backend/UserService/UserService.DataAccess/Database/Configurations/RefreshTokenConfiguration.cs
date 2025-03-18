using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id); 
        
        builder.Property(rt => rt.UserId).IsRequired();
        builder.Property(rt => rt.ExpiresAt).IsRequired()
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        builder.HasQueryFilter(u => !u.IsUsed);
        builder.Property(rt => rt.IsUsed).HasDefaultValue(false);
    }
}