using System.Security.Claims;
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
using UserService.DataAccess.RedisModels;

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

    //TODO: ADD REFRESH TOKEN TO CACHE
    public async Task<string> GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = jwtProvider.GenerateTokenModel(user, TokenTypes.Refresh, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        await unitOfWork.TokenModelRepository.Add(token, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return token.Token;
    }
    
    //TODO: ADD CHECK IF TOKEN ALREADY EXIST IN CACHE
    public async Task<string> GenerateEmailToken(UserEntity user, TokenTypes tokenType, CancellationToken cancellationToken)
    {
        var confirmToken = jwtProvider.GenerateToken(user, tokenType, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        await cacheService.AddEmailTokenToCacheAsync(user.Email, confirmToken, tokenType, cancellationToken);
        
        confirmToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));
        return confirmToken;
    }
    
    //TODO: ADD CHECK FROM CACHE VALUE IF IT EXIST
    public async Task<string> RefreshAccessToken(string? refreshToken, CancellationToken cancellationToken)
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
        
        var accessToken = await GenerateAccessToken(user, cancellationToken)
            ?? throw new BadRequestException("Failed to refresh token.");

        return accessToken;
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

    public async Task<bool> DeleteToken(string token, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.TokenModelRepository.GetByToken(token, cancellationToken)
            ?? throw new BadRequestException("Invalid token");

        var success = await unitOfWork.TokenModelRepository.Delete(candidate, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        return success;
    }
}