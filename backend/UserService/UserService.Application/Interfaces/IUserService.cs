using UserService.Application.Dto;

namespace UserService.Api.Interfaces;

public interface IUserService
{
    Task<UserDto>  GetUserById(Guid id, CancellationToken cancellationToken);
    Task<UserDto> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken);
    
    Task<UserDto> RegisterUser(RegisterDto registerDto, CancellationToken cancellationToken);
    Task<string> Login(LoginDto loginDto, CancellationToken cancellationToken);
    
    Task<UserDto> UpdateUser(UserDto userDto, CancellationToken cancellationToken);
    Task<UserDto> DeleteUser(UserDto userDto, CancellationToken cancellationToken);
}