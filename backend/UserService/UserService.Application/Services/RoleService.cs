using UserService.Api.Interfaces;
using UserService.Application.Dto.RoleDto;
using UserService.DataAccess.Enums;

namespace UserService.Application.Services;

public class RoleService : IRoleService
{
    public Task<RoleDto> GetRoleById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoleDto> GetRoleByRoleName(Role role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RoleDto>> GetRoles(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoleDto> CreateRole(RoleDto role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoleDto> UpdateRole(RoleDto role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoleDto> DeleteRole(RoleDto role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}