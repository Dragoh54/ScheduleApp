using UserService.DataAccess.Enums;

namespace UserService.Application.Dto.RoleDto;

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