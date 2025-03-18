namespace UserService.DataAccess.Models;

public class UserRoles
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid RoleId { get; set; }
    public RoleEntity Role { get; set; }

    public UserRoles()
    {
        
    }

    public UserRoles(Guid userId, Guid roleId, UserEntity user, RoleEntity role)
    {
        UserId = userId;
        RoleId = roleId;
        User = user;
        Role = role;
    }
}