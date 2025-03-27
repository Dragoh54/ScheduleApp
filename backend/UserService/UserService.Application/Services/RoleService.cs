using Mapster;
using UserService.Api.Interfaces;
using UserService.Application.Dto.RoleDto;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.UnitOfWork;

namespace UserService.Application.Services;

public class RoleService(IUnitOfWork unitOfWork) : IRoleService
{
    public async Task<RoleDto> GetRoleById(Guid id, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.RoleRepository.Get(id, cancellationToken)
                     ?? throw new NotFoundException("Role not found");

        return result.Adapt<RoleDto>();
    }

    public async Task<RoleDto> GetRoleByRoleName(Role role, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.RoleRepository.GetByRole(role, cancellationToken)
                   ?? throw new NotFoundException("Role not found");

        return result.Adapt<RoleDto>();
    }

    public async Task<IEnumerable<RoleDto>> GetRoles(CancellationToken cancellationToken)
    {
        var result = await unitOfWork.RoleRepository.GetAll(cancellationToken)
                     ?? throw new NotFoundException("Role not found");

        return result.Adapt<IEnumerable<RoleDto>>();
    }
}