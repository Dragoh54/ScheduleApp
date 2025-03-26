using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.Application.Dto.TokenDtos;
using UserService.DataAccess.Enums;
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
        var token = _jwtProvider.GenerateToken(user, Token.Access, cancellationToken);
        
        if (token is null)
        {
            throw new UnauthorizedAccessException("Failed to generate token.");
        }
        
        return token;
    }

    public async Task<string> GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = _jwtProvider.GenerateTokenModel(user, Token.Refresh, cancellationToken);
        
        if (token is null)
        {
            throw new UnauthorizedAccessException("Failed to generate token.");
        }
        
        await _unitOfWork.TokenModelRepository.Add(token, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return token.Token;
    }

    public async Task<string> GenerateEmailToken(UserEntity user, Token tokenType, CancellationToken cancellationToken)
    {
        var confirmToken = _jwtProvider.GenerateToken(user, tokenType, cancellationToken);
        if (confirmToken is null)
        {
            throw new UnauthorizedAccessException("Failed to generate token.");
        }
        
        confirmToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));
        
        var token = _jwtProvider.GenerateTokenModel(user, confirmToken, tokenType, cancellationToken);
        if (token is null)
        {
            throw new UnauthorizedAccessException("Failed to generate token.");
        }
        
        await _unitOfWork.TokenModelRepository.Add(token, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();
        
        return confirmToken;
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

    public async Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken)
    {
        var principal = _jwtProvider.ValidateToken(token);
        if (principal == null)
        {
            throw new BadRequestException("Invalid or expired token");
        }
        cancellationToken.ThrowIfCancellationRequested();
        
        var emailClaim = principal.FindFirst(ClaimTypes.Email);
        if (emailClaim == null || string.IsNullOrEmpty(emailClaim.Value))
        {
            throw new BadRequestException("Email claim not found in token");
        }
        
        cancellationToken.ThrowIfCancellationRequested();

        return emailClaim.Value;
    }

    public async Task<bool> DeleteToken(string token, CancellationToken cancellationToken)
    {
        var candidate = await _unitOfWork.TokenModelRepository.GetByToken(token, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        if (candidate is null)
        {
            throw new BadRequestException("Invalid token");
        }
        
        var success = await _unitOfWork.TokenModelRepository.Delete(candidate, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        return success;
    }
}