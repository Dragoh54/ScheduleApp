using UserService.Application.Dto;

namespace UserService.Api.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken);
    Task<UserDto> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken);
    Task<PaginatedListDto<UserDto>> GetUsers(PaginatedPageUsers request, CancellationToken cancellationToken);
    Task<UserDto> UpdateUser(UpdateUserDto userDto, CancellationToken cancellationToken);
    Task<UserDto> DeleteUser(DeleteUserDto userDto, CancellationToken cancellationToken);
    Task<UserDto> SoftDelete(DeleteUserDto userDto, CancellationToken cancellationToken);
    Task<UserDto> AddAdminRole(Guid userId, CancellationToken cancellationToken);
}