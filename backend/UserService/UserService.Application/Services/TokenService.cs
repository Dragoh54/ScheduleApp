using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Interfaces.Providers;
using UserService.Application.Interfaces.Services;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;

namespace UserService.Application.Services;

public class TokenService(
    IJwtProvider jwtProvider, 
    IEmailTokenProvider emailTokenProvider,
    IDistributedCache cache, 
    IEmailCacheService emailCacheService,
    IUnitOfWork unitOfWork
    ) : ITokenService
{
    public async Task<string> GenerateAccessToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = jwtProvider.GenerateAccessToken(user, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        return token;
    }
    
    public async Task<string> GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = jwtProvider.GenerateRefreshToken(user, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        await unitOfWork.TokenModelRepository.Add(token, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return token.Token;
    }
    
    public async Task<string> GenerateEmailToken(UserEntity user, TokenTypes tokenType, CancellationToken cancellationToken)
    {
        var token = await cache.GetStringAsync(user.Email, cancellationToken);
        if (!string.IsNullOrEmpty(token))
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }
        
        var confirmToken = emailTokenProvider.GenerateEmailToken(user, tokenType, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        await emailCacheService.AddEmailTokenToCacheAsync(user.Email, confirmToken, tokenType, cancellationToken);
        
        confirmToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));
        return confirmToken;
    }

    public async Task<(string, string)> RefreshAccessToken(string? refreshToken, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedAccessException();
        }
        
        var token = await unitOfWork.TokenModelRepository.GetByToken(refreshToken, cancellationToken);
        
        var isTokenInvalid = token is null || token.IsUsed || token.ExpiresAt < DateTime.UtcNow;
        
        if (isTokenInvalid)
        {
            throw new UnauthorizedAccessException();
        }
        
        var user = await unitOfWork.UserRepository.GetById(token.UserId, cancellationToken)
                   ?? throw new UnauthorizedAccessException();

        token.Token = jwtProvider.GenerateRefreshTokenString();
        token.UpdatedAt = DateTime.UtcNow;
        
        await unitOfWork.TokenModelRepository.Update(token, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        
        var accessToken = await GenerateAccessToken(user, cancellationToken)
            ?? throw new BadRequestException("Failed to refresh token.");

        return (accessToken, token.Token);
    }

    public async Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken) =>
        await jwtProvider.GetClaimFromToken(token, ClaimTypes.Email);

    public async Task<string> GetIdFromToken(string token, CancellationToken cancellationToken) =>
        await jwtProvider.GetClaimFromToken(token, "Id");
}