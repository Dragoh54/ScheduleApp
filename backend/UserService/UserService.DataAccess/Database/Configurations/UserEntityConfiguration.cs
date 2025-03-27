using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id).IsRequired();
        builder.Property(u=>u.IsConfirmed).IsRequired();
        builder.Property(u => u.Username).HasMaxLength(100).IsRequired(); 
        builder.Property(u => u.Email).HasMaxLength(100).IsRequired();
        builder.Property(u => u.PasswordHash).HasMaxLength(256).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength(256).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(256).IsRequired();
        
        builder.Property(u => u.CreatedAt)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            .IsRequired();
        
        builder.Property(u => u.UpdatedAt)
            .HasConversion(u => u, u => DateTime.SpecifyKind(u, DateTimeKind.Utc))
            .IsRequired();
        
        builder.HasQueryFilter(u => !u.IsDeleted);
        builder.Property(u => u.IsDeleted).IsRequired();
        
        builder.HasMany(u => u.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}