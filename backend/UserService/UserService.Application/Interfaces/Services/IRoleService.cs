using UserService.Application.Dto.RoleDto;
using UserService.DataAccess.Enums;

namespace UserService.Api.Interfaces;

public interface IRoleService
{
    Task<RoleDto>  GetRoleById(Guid id, CancellationToken cancellationToken);
    Task<RoleDto> GetRoleByRoleName(Roles roles, CancellationToken cancellationToken);
    Task<IEnumerable<RoleDto>> GetRoles(CancellationToken cancellationToken);
}