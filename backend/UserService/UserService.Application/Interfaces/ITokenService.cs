using UserService.Application.Dto;
using UserService.Application.Dto.TokenDtos;
using UserService.DataAccess.Models;

namespace UserService.Api.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(UserEntity user, CancellationToken cancellationToken);
    Task<string> GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken);
    Task<string> GenerateEmailConfirmationToken(UserEntity user, CancellationToken cancellationToken);
    
    Task<string> RefreshAccessToken(string? refreshToken, CancellationToken cancellationToken);
    
    Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken);
    Task<bool> DeleteToken(string token, CancellationToken cancellationToken);
}