using System.Text;
using System.Web;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.Application.Dto.EmailDtos;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;

namespace UserService.Application.Services;

public class AuthenticationService(
    IPasswordHasher passwordHasher, 
    IUnitOfWork unitOfWork, 
    ITokenService tokenService, 
    IEmailService emailService,
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
        var userByEmail = await unitOfWork.UserRepository.GetByEmailAsync(loginUserDto.Email, cancellationToken);

        if (userByEmail is null)
        {
            throw new NotFoundException("Cannot found user with this email");
        }

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
        
        var confirmToken = await tokenService.GenerateEmailConfirmationToken(user, cancellationToken);
        var confirmationLink = GenerateConfirmationLink(callbackUrl, email, confirmToken);
        
        SendEmailAsync(
            email,
            "Подтверждение email",
            confirmationLink,
            cancellationToken);

        return confirmToken;
    }

    public async Task<string> ConfirmEmailReceiveAsync(ConfirmEmailDto dto, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByEmailAsync(dto.Email, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        await tokenService.DeleteToken(dto.Token, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        user.IsConfirmed = true;
    
        await unitOfWork.UserRepository.Update(user, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        cancellationToken.ThrowIfCancellationRequested();

        return "Email successfully confirmed!";
    }

    public Task<string> ForgotPasswordAsync(string? accessToken, string callbackUrl, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordRequest, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private string GenerateConfirmationLink(string baseUrl, string email, string token)
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
    
    private void SendEmailAsync(string email, string title, string callbackUrl, CancellationToken cancellationToken)
    {
        emailService.SendEmailAsync(email, title,
            $"To confirm email visit this link -> {callbackUrl}.", cancellationToken);
    }
}