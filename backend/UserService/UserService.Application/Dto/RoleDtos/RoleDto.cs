using UserService.DataAccess.Enums;

namespace UserService.Application.Dto.RoleDtos;

public class RoleDto
{
    public Roles RoleName { get; set; }

    public RoleDto()
    {
        
    }

    public RoleDto(Roles roles)
    {
        RoleName = roles;
    }
}