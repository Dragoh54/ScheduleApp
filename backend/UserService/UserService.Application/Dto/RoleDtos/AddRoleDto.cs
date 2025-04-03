using UserService.DataAccess.Enums;

namespace UserService.Application.Dto;

public class AddRoleDto
{
    public Guid UserId { get; set; }
    public Roles Role { get; set; }

    public AddRoleDto()
    {
        
    }
}