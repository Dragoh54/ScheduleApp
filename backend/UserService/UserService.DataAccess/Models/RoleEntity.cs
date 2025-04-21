using UserService.DataAccess.Enums;

namespace UserService.DataAccess.Models;

public class RoleEntity : IdEntity
{
    public RoleEntity(){}

    public RoleEntity(Roles rolesName)
    {
        RoleName = rolesName;
    }
    
    public Roles RoleName { get; set; }
    
    public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
}