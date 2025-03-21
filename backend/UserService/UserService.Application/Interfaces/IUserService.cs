using UserService.Application.Dto;

namespace UserService.Api.Interfaces;

public interface IUserService
{
    Task<UserDto>  GetUserById(Guid id, CancellationToken cancellationToken);
    Task<UserDto>  GetUserByIdWithRoles(Guid id, CancellationToken cancellationToken);
    Task<UserDto> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<UserDto> GetUserByEmailWithRoles(string email, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken);
    
    Task<UserDto> RegisterUser(RegisterDto registerDto, CancellationToken cancellationToken);
    Task<(string, string)> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken);
    Task<bool> Logout(string token, CancellationToken cancellationToken);
    
    Task<UserDto> UpdateUser(UpdateUserDto userDto, CancellationToken cancellationToken);
    Task<UserDto> DeleteUser(DeleteUserDto userDto, CancellationToken cancellationToken);
    Task<UserDto> SoftDelete(DeleteUserDto userDto, CancellationToken cancellationToken);
}