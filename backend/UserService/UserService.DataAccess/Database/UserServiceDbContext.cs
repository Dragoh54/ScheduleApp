using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Models;

using static UserService.DataAccess.Database.DataSeeder.DataSeeder;

namespace UserService.DataAccess.Database;

public class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
    : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<TokenEntity> Tokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserServiceDbContext).Assembly);
        
        var users = GenerateUsers();
        var roles = GenerateRoles();
        var userRoles = GenerateUserRoles(users, roles);
        
        modelBuilder.Entity<UserEntity>().HasData(users);
        modelBuilder.Entity<RoleEntity>().HasData(roles);
        modelBuilder.Entity<UserRoles>().HasData(userRoles);
        
        base.OnModelCreating(modelBuilder);
    }
}