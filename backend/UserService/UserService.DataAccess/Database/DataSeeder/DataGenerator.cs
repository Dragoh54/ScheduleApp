using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.DataSeeder;

public static class DataGenerator
{
    public static List<UserEntity> GenerateUsers()
    {
        return
        [
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS",
                FirstName = "Admin",
                LastName = "User",
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                IsDeleted = false,
                IsConfirmed = true,
                UserRoles = new List<UserRoles>()
            },

            new UserEntity
            {
                Id = Guid.NewGuid(),
                Username = "user1",
                Email = "user1@example.com",
                PasswordHash = "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK",
                FirstName = "John",
                LastName = "Doe",
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                IsDeleted = false,
                IsConfirmed = true,
                UserRoles = new List<UserRoles>()
            },

            new UserEntity
            {
                Id = Guid.NewGuid(),
                Username = "user2",
                Email = "user2@example.com",
                PasswordHash = "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La",
                FirstName = "Jane",
                LastName = "Doe",
                CreatedAt =  DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                IsDeleted = false,
                IsConfirmed = false,
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
                Id = Guid.NewGuid(),
                RoleName = Roles.Guest
            },

            new RoleEntity
            {
                Id = Guid.NewGuid(),
                RoleName = Roles.User
            },

            new RoleEntity
            {
                Id = Guid.NewGuid(),
                RoleName = Roles.OrganizationAdmin
            },

            new RoleEntity
            {
                Id = Guid.NewGuid(),
                RoleName = Roles.Admin
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