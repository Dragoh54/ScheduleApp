using UserService.Application.Dto;

namespace UserService.Api.Interfaces;

public interface IAuthenticationService
{
    Task<UserDto> Register(RegisterDto registerDto, CancellationToken cancellationToken);
    Task<(string, string)> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken);
    Task<bool> Logout(string? token, CancellationToken cancellationToken);
}