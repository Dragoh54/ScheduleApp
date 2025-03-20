using UserService.DataAccess.Enums;

namespace UserService.Application.Dto.RoleDto;

public class RoleDto
{
    public Role RoleName { get; set; }

    public RoleDto()
    {
        
    }

    public RoleDto(Role role)
    {
        RoleName = role;
    }
}