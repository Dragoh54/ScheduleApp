using UserService.Application.Dto;
using UserService.Application.Dto.EmailDtos;

namespace UserService.Api.Interfaces;

public interface IAuthenticationService
{
    Task<UserDto> Register(RegisterDto registerDto, CancellationToken cancellationToken);
    Task<(string, string)> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken);
    Task<bool> Logout(string? token, CancellationToken cancellationToken);
    
    Task<string> ConfirmEmailSendAsync(string? accessToken, string callbackUrl, CancellationToken cancellationToken);
    Task<string> ConfirmEmailReceiveAsync(EmailTokenDto emailTokenRequest, CancellationToken cancellationToken);
    
    Task<string> ForgotPasswordAsync(string? email, string callbackUrl, CancellationToken cancellationToken);
    Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken);
    Task<bool> ValidateResetPasswordAsync(EmailTokenDto resetPasswordRequestTokenDto, CancellationToken cancellationToken);
    
    //TODO: ADD RESTORE ACCOUNT
    Task<string> RecoverAccountAsync(string? email, string callbackUrl, CancellationToken cancellationToken);
    Task<string> RestoreAccountAsync(EmailTokenDto emailTokenDto, CancellationToken cancellationToken); 
}