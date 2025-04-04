using UserService.DataAccess.Enums;

namespace UserService.Application.Dto.RoleDtos;

public class AddRoleDto
{
    public Guid UserId { get; init; }
    public Roles Role { get; init; }
}