using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Configurations;

public class TokenModelConfiguration : IEntityTypeConfiguration<TokenModel>
{
    public void Configure(EntityTypeBuilder<TokenModel> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.Property(t => t.TokenType).IsRequired();
        builder.Property(rt => rt.UserId).IsRequired();
        builder.Property(rt => rt.ExpiresAt).IsRequired()
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(rt => rt.CreatedAt).IsRequired()
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(rt => rt.UpdatedAt).IsRequired()
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        builder.HasQueryFilter(u => !u.IsUsed);
        builder.Property(rt => rt.IsUsed).HasDefaultValue(false);
        
        builder.HasQueryFilter(x => !x.User.IsDeleted);
        builder.HasQueryFilter(x => !x.IsUsed);
    }
}