using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using UserService.Api.Interfaces;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;

namespace UserService.Application.Services;

public class TokenService(
    IJwtProvider jwtProvider, 
    IDistributedCache cache, 
    ICacheService cacheService,
    IUnitOfWork unitOfWork
    ) : ITokenService
{
    public async Task<string> GenerateAccessToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = jwtProvider.GenerateToken(user, TokenTypes.Access, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        return token;
    }
    
    public async Task<string> GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = jwtProvider.GenerateTokenModel(user, TokenTypes.Refresh, cancellationToken)
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
        
        var confirmToken = jwtProvider.GenerateToken(user, tokenType, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        await cacheService.AddEmailTokenToCacheAsync(user.Email, confirmToken, tokenType, cancellationToken);
        
        confirmToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));
        return confirmToken;
    }

    public async Task<(string, string)> RefreshAccessToken(string? refreshToken, CancellationToken cancellationToken)
    {
        if (refreshToken is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        var token = await unitOfWork.TokenModelRepository.GetByToken(refreshToken, cancellationToken);
        if (token is null || token.IsUsed || token.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException();
        }
        
        var user = await unitOfWork.UserRepository.Get(token.UserId, cancellationToken)
                   ?? throw new UnauthorizedAccessException();

        token.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        token.UpdatedAt = DateTime.UtcNow;
        
        await unitOfWork.TokenModelRepository.Update(token, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        
        var accessToken = await GenerateAccessToken(user, cancellationToken)
            ?? throw new BadRequestException("Failed to refresh token.");

        return (accessToken, token.Token);
    }

    public async Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken)
    {
        var principal = jwtProvider.ValidateToken(token)
            ?? throw new BadRequestException("Invalid or expired token");
        
        var emailClaim = principal.FindFirst(ClaimTypes.Email);
        if (emailClaim is null || string.IsNullOrEmpty(emailClaim.Value))
        {
            throw new BadRequestException("Email claim not found in token");
        }
        cancellationToken.ThrowIfCancellationRequested();

        return emailClaim.Value;
    }

    public async Task<string> GetIdFromToken(string token, CancellationToken cancellationToken)
    {
        var principal = jwtProvider.ValidateToken(token)
                        ?? throw new BadRequestException("Invalid or expired token");
        
        var idClaim = principal.FindFirst("Id")
                ?? throw new BadRequestException("Id claim not found in token");
        
        return idClaim.Value;
    }
}