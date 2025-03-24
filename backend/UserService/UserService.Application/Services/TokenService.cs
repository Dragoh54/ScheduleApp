using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.Application.Dto.TokenDtos;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;

namespace UserService.Application.Services;

public class TokenService : ITokenService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public TokenService(IJwtProvider jwtProvider, IUnitOfWork unitOfWork)
    {
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> GenerateAccessToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = _jwtProvider.GenerateAccessToken(user, cancellationToken);
        
        if (token is null)
        {
            throw new UnauthorizedAccessException("Failed to generate token.");
        }
        
        return token;
    }

    public async Task<string> GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = _jwtProvider.GenerateRefreshToken(user, cancellationToken);
        
        if (token is null)
        {
            throw new UnauthorizedAccessException("Failed to generate token.");
        }
        
        await _unitOfWork.TokenModelRepository.Add(token, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return token.Token;
    }

    public async Task<string> RefreshAccessToken(string? refreshToken, CancellationToken cancellationToken)
    {
        if (refreshToken is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        var token = await _unitOfWork.TokenModelRepository.GetByToken(refreshToken, cancellationToken);
        if (token is null || token.IsUsed || token.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException();
        }
        
        var user = await _unitOfWork.UserRepository.Get(token.UserId, cancellationToken);
        if (user is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        var accessToken = await GenerateAccessToken(user, cancellationToken);
        if (accessToken is null)
        {
            throw new BadRequestException("Failed to refresh token.");
        }

        return accessToken;
    }

    public async Task<bool> DeleteRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}