using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Web;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.Application.Dto.EmailDtos;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;
using UserService.DataAccess.RedisModels;

namespace UserService.Application.Services;

public class AuthenticationService(
    IPasswordHasher passwordHasher, 
    IUnitOfWork unitOfWork, 
    ITokenService tokenService, 
    IEmailService emailService,
    IDistributedCache cache,
    IUserService userService,
    IJwtProvider jwtProvider
    ) : IAuthenticationService
{
    public async Task<UserDto> Register(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        var candidate = await unitOfWork.UserRepository.GetByEmailAsync(registerDto.Email, cancellationToken);
        if (candidate is not null)
        {
            throw new AlreadyExistsException("User with this email already exists!");
        }
        
        var hashedPassword = passwordHasher.Generate(registerDto.Password, cancellationToken);
        var user = new UserEntity(registerDto.Username, registerDto.Email, hashedPassword, registerDto.FirstName, registerDto.LastName);
        
        await unitOfWork.UserRepository.Add(user, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();

        return user.Adapt<UserDto>();
    }
    
    public async Task<(string, string)> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken)
    {
        var userByEmail = await unitOfWork.UserRepository.GetByEmailAsync(loginUserDto.Email, cancellationToken)
            ?? throw new NotFoundException("Cannot found user with this email");

        var result = passwordHasher.Verify(loginUserDto.Password, userByEmail.PasswordHash, cancellationToken);

        if (!result)
        {
            throw new BadRequestException("Failed to login");
        }

        var token = await tokenService.GenerateAccessToken(userByEmail, cancellationToken);
        var refreshToken = await tokenService.GenerateRefreshToken(userByEmail, cancellationToken);
        
        return (token, refreshToken);
    }
    
    public async Task<bool> Logout(string? token, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }
        
        var refreshToken = unitOfWork.TokenModelRepository.GetByToken(token, cancellationToken).Result;
        cancellationToken.ThrowIfCancellationRequested();

        if (refreshToken is null)
        {
            throw new NotFoundException("Refresh token not found");
        }

        refreshToken.IsUsed = true;

        await unitOfWork.TokenModelRepository.Update(refreshToken, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
    
    public async Task<string> ConfirmEmailSendAsync(string? accessToken, string callbackUrl, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new BadRequestException("Invalid access token");
        }
        
        var email = await tokenService.GetEmailFromToken(accessToken, cancellationToken);
        var user = await unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken);
    
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        var confirmToken = await tokenService.GenerateEmailToken(user, TokenTypes.EmailConfirmation, cancellationToken);
        
        var link = GenerateEmailTokenLink(callbackUrl, email, confirmToken);
        
        await emailService.SendEmailAsync(
            email, 
            "No-reply",
            $"""
             <h1>Email confirmation</h1>
             <p>Go thought this link to confirm:</p>
             <a href="{link}">Confirm!</a>
             <p>This link active only 24 hours.</p>
             """,
            cancellationToken);

        return confirmToken;
    }

    public async Task<string> ConfirmEmailReceiveAsync(EmailTokenDto tokenDto, CancellationToken cancellationToken)
    {
        var token = await cache.GetStringAsync(tokenDto.Email, cancellationToken)
            ?? throw new NotFoundException("Token is not found or expired");
        
        CheckTokens(token, tokenDto.Token);

        await cache.RemoveAsync(tokenDto.Email, cancellationToken);
        
        var user = await unitOfWork.UserRepository.GetByEmailAsync(tokenDto.Email, cancellationToken)
             ?? throw new NotFoundException("User not found");
        
        user.IsConfirmed = true;
    
        await unitOfWork.UserRepository.Update(user, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();

        return "Email successfully confirmed!";
    }

    public async Task<string> ForgotPasswordAsync(string? email, string callbackUrl, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new BadRequestException("invalid email address");
        }
        
        var user = await unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken);
    
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        var resetPass = await tokenService.GenerateEmailToken(user, TokenTypes.ResetPassword, cancellationToken);
        var link = GenerateEmailTokenLink(callbackUrl, email, resetPass);
        
        await emailService.SendEmailAsync(
            email, 
            "Reset password",
            $"""
               <h1>Reset password</h1>
               <p>Go thought this link to reset:</p>
               <a href="{link}">Reset!</a>
               <p>This link active only 24 hours.</p>
               """, 
            cancellationToken);

        return resetPass;
    }
    
    public async Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
    {
        var token = await cache.GetStringAsync(resetPasswordDto.Email, cancellationToken)
                    ?? throw new NotFoundException("Token is not found or expired");
        
        CheckTokens(token, resetPasswordDto.Token);

        await cache.RemoveAsync(resetPasswordDto.Email, cancellationToken);
        
        var user = await unitOfWork.UserRepository.GetByEmailAsync(resetPasswordDto.Email, cancellationToken)
             ?? throw new NotFoundException("User not found");
        
        user.PasswordHash = passwordHasher.Generate(resetPasswordDto.Password, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
    
        await unitOfWork.UserRepository.Update(user, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();

        return $"Password successfully reset!\n Password: {resetPasswordDto.Password}";
    }
    
    public async Task<bool> ValidateResetPasswordAsync(EmailTokenDto resetPasswordRequestTokenDto, CancellationToken cancellationToken)
    {
        var token = await unitOfWork.TokenModelRepository.GetByToken(resetPasswordRequestTokenDto.Token, cancellationToken)
            ?? throw new NotFoundException("Token not found");

        if (token.ExpiresAt < DateTime.UtcNow || token.IsUsed)
        {
            throw new BadRequestException("Token expired");
        }
        
        return true;
    }


    private static string GenerateEmailTokenLink(string baseUrl, string email, string token)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["email"] = email;
        query["token"] = HttpUtility.UrlEncode(token);
    
        var uriBuilder = new UriBuilder(baseUrl)
        {
            Query = query.ToString()
        };
    
        return uriBuilder.ToString();
    }

    private void CheckTokens(string token, string encodedToke)
    {
        var decodedTokenFromDto = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedToke));
        if (token != decodedTokenFromDto)
        {
            throw new BadRequestException("Invalid token");
        }
    }
}