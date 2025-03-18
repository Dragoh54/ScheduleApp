using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database;

public class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
    : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserServiceDbContext).Assembly);
    }
}