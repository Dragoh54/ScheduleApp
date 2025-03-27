using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using UserService.Api.Interfaces;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;

namespace UserService.Application.Services;

public class TokenService(IJwtProvider jwtProvider, IUnitOfWork unitOfWork) : ITokenService
{
    public async Task<string> GenerateAccessToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = jwtProvider.GenerateToken(user, Token.Access, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        return token;
    }

    public async Task<string> GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = jwtProvider.GenerateTokenModel(user, Token.Refresh, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        await unitOfWork.TokenModelRepository.Add(token, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return token.Token;
    }

    public async Task<string> GenerateEmailToken(UserEntity user, Token tokenType, CancellationToken cancellationToken)
    {
        var confirmToken = jwtProvider.GenerateToken(user, tokenType, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        confirmToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));
        
        var token = jwtProvider.GenerateTokenModel(user, confirmToken, tokenType, cancellationToken)
            ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        await unitOfWork.TokenModelRepository.Add(token, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return confirmToken;
    }
    
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