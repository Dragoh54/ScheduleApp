using UserService.Application.Dto.RoleDto;
using UserService.DataAccess.Enums;

namespace UserService.Api.Interfaces;

public interface IRoleService
{
    Task<RoleDto>  GetRoleById(Guid id, CancellationToken cancellationToken);
    Task<RoleDto> GetRoleByRoleName(Role role, CancellationToken cancellationToken);
    Task<IEnumerable<RoleDto>> GetRoles(CancellationToken cancellationToken);
    
    // Task<RoleDto> CreateRole(RoleDto role, CancellationToken cancellationToken);
    // Task<RoleDto> UpdateRole(RoleDto role, CancellationToken cancellationToken);
    // Task<RoleDto> DeleteRole(RoleDto role, CancellationToken cancellationToken);
}