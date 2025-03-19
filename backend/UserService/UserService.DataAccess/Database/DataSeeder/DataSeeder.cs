using UserService.DataAccess.Enums;
using UserService.DataAccess.Handlers.JwtUtilities;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.DataSeeder;

public static class DataSeeder
{
    public static List<UserEntity> GenerateUsers()
    {
        var passwordHasher = new PasswordHasher();
        
        return
        [
            new UserEntity
            {
                //Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = passwordHasher.Generate("admin", new CancellationToken()),
                FirstName = "Admin",
                LastName = "User",
                //CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                //UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                IsDeleted = false,
                UserRoles = new List<UserRoles>()
            },

            new UserEntity
            {
                //Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Id = Guid.NewGuid(),
                Username = "user1",
                Email = "user1@example.com",
                PasswordHash = passwordHasher.Generate("1234", new CancellationToken()),
                FirstName = "John",
                LastName = "Doe",
                // CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                // UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                IsDeleted = false,
                UserRoles = new List<UserRoles>()
            },

            new UserEntity
            {
                //Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Id = Guid.NewGuid(),
                Username = "user2",
                Email = "user2@example.com",
                PasswordHash = passwordHasher.Generate("1234", new CancellationToken()),
                FirstName = "Jane",
                LastName = "Doe",
                // CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                // UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt =  DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                IsDeleted = false,
                UserRoles = new List<UserRoles>()
            }
        ];
    }

    public static List<RoleEntity> GenerateRoles()
    {
        return
        [
            new RoleEntity
            {
                //Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Id = Guid.NewGuid(),
                RoleName = Role.Guest
            },

            new RoleEntity
            {
                // Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Id = Guid.NewGuid(),
                RoleName = Role.User
            },

            new RoleEntity
            {
                //Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Id = Guid.NewGuid(),
                RoleName = Role.OrganizationAdmin
            },

            new RoleEntity
            {
                //Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Id = Guid.NewGuid(),
                RoleName = Role.Admin
            }
        ];
    }

    public static List<UserRoles> GenerateUserRoles(List<UserEntity> users, List<RoleEntity> roles)
    {
        return
        [
            //Admin
            new UserRoles
            {
                UserId = users[0].Id, 
                RoleId = roles[1].Id
            },

            new UserRoles
            {
                UserId = users[0].Id, 
                RoleId = roles[2].Id 
            },
            
            new UserRoles
            {
                UserId = users[0].Id, 
                RoleId = roles[3].Id 
            },
            
            //OrganizationAdmin
            new UserRoles
            {
                UserId = users[1].Id, 
                RoleId = roles[1].Id 
            },
            
            new UserRoles
            {
                UserId = users[1].Id, 
                RoleId = roles[2].Id 
            },
            
            //User
            new UserRoles
            {
                UserId = users[2].Id, 
                RoleId = roles[1].Id 
            }
        ];
    }
}