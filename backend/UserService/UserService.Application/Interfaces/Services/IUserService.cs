using UserService.Application.Dto;
using UserService.DataAccess.Enums;

namespace UserService.Api.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken);
    Task<PaginatedListDto<UserDto>> GetUsers(PaginatedPageUsers request, CancellationToken cancellationToken);
    
    Task<UserDto> UpdateUser(Guid id, UpdateUserDto userDto, CancellationToken cancellationToken);
    Task<UserDto> AddRole(AddRoleDto roleDto, CancellationToken cancellationToken);

    Task<UserDto> SoftDelete(string token, CancellationToken cancellationToken);
}