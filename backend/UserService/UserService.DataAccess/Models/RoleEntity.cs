using UserService.DataAccess.Enums;

namespace UserService.DataAccess.Models;

public class RoleEntity : IdEntity
{
    public Role RoleName { get; set; }
    
    public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    
    public RoleEntity(){}

    public RoleEntity(Role roleName)
    {
        RoleName = roleName;
    }
}