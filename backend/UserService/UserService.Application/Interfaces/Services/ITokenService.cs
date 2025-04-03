using UserService.Application.Dto;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.Api.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(UserEntity user, CancellationToken cancellationToken);
    Task<string> GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken);
    public Task<string> GenerateEmailToken(UserEntity user, TokenTypes tokenTypesType, CancellationToken cancellationToken);
    
    Task<(string, string)> RefreshAccessToken(string? refreshToken, CancellationToken cancellationToken);
    
    Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken);
    Task<string> GetIdFromToken(string token, CancellationToken cancellationToken);
}