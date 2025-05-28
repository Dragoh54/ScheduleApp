using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Configurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Id).IsRequired();
        builder.Property(r => r.RoleName).IsRequired();
        
        builder.HasMany(r => r.UserRoles)
            .WithOne()
            .HasForeignKey(r => r.RoleId);
    }
}