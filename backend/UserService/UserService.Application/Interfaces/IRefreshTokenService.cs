using UserService.Application.Dto;
using UserService.Application.Dto.TokenDtos;

namespace UserService.Api.Interfaces;

public interface IRefreshTokenService
{
    Task<string> GenerateAccessToken(LoginUserDto user, CancellationToken cancellationToken);
    Task<string> GenerateRefreshToken(LoginUserDto user, CancellationToken cancellationToken);
    Task<TokenDto> UpdateToken(TokenDto token, CancellationToken cancellationToken);
    // Task<Guid> AddToken(RefreshTokenDto token, CancellationToken cancellationToken);
    // Task<bool> DeleteToken(Guid tokenId, CancellationToken cancellationToken);
}