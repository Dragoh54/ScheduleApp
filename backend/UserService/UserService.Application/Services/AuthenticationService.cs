using System.Text;
using System.Web;
using Hangfire;
using Mapster;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using UserService.Application.Dto.EmailDtos;
using UserService.Application.Dto.UserDtos;
using UserService.Application.Features.Email;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Interfaces.Services;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.DataAccess.Models;

namespace UserService.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistributedCache _cache;

    public AuthenticationService(IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, ITokenService tokenService,
        IEmailService emailService, IDistributedCache cache
    )
    {
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }
    
    public async Task<UserDto> Register(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        var candidate = await _unitOfWork.UserRepository.GetByEmailAsync(registerDto.Email, cancellationToken);
        if (candidate is not null)
        {
            throw new AlreadyExistsException("User with this email already exists!");
        }
        
        var hashedPassword = _passwordHasher.Generate(registerDto.Password, cancellationToken);
        var user = new UserEntity(registerDto.Username, registerDto.Email, hashedPassword, registerDto.FirstName, registerDto.LastName);
        
        await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        BackgroundJob.Enqueue(() => 
            _emailService.SendEmailAsync(
                user.Email, 
                "Welcome!",
                "<h1>You are successfully registered to SCHEDULE APP</h1>",
                cancellationToken));

        return user.Adapt<UserDto>();
    }
    
    public async Task<(string, string)> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken)
    {
        var userByEmail = await _unitOfWork.UserRepository.GetByEmailAsync(loginUserDto.Email, cancellationToken);

        var isVerified = _passwordHasher.Verify(loginUserDto.Password, userByEmail.PasswordHash, cancellationToken);

        if (!isVerified)
        {
            throw new BadRequestException("Incorrect email or password!");
        }

        var token = await _tokenService.GenerateAccessToken(userByEmail, cancellationToken);
        var refreshToken = await _tokenService.GenerateRefreshToken(userByEmail, cancellationToken);
        
        userByEmail.LastLoginAt = DateTime.UtcNow;
        
        await _unitOfWork.UserRepository.UpdateAsync(userByEmail, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        return (token, refreshToken);
    }
    
    public async Task<bool> Logout(string? token, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }
        
        var refreshToken = _unitOfWork.TokenModelRepository.GetByToken(token, cancellationToken).Result
                           ?? throw new NotFoundException("Refresh token not found");
        
        cancellationToken.ThrowIfCancellationRequested();
        
        refreshToken.IsUsed = true;

        await _unitOfWork.TokenModelRepository.UpdateAsync(refreshToken, cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();
        
        return true;
    }
    
    public async Task<string> ConfirmEmailSendAsync(string? accessToken, string callbackUrl, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new BadRequestException("Invalid access token");
        }
        
        var email = await _tokenService.GetEmailFromToken(accessToken, cancellationToken);
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken)
            ?? throw new NotFoundException("User not found");
        
        var confirmToken = await _tokenService.GenerateEmailToken(user, TokenTypes.EmailConfirmation, cancellationToken);
        
        var link = GenerateEmailTokenLink(callbackUrl, email, confirmToken);
        
        BackgroundJob.Enqueue(() => 
            _emailService.SendEmailAsync(
                email, 
                "No-reply",
                EmailTemplates.ConfirmEmailBody(link),
                cancellationToken));

        return confirmToken;
    }

    public async Task<string> ConfirmEmailReceiveAsync(EmailTokenDto tokenDto, CancellationToken cancellationToken)
    {
        var token = await _cache.GetStringAsync(tokenDto.Email, cancellationToken)
            ?? throw new NotFoundException("Token is not found or expired");
        
        CheckTokens(token, tokenDto.Token);

        await _cache.RemoveAsync(tokenDto.Email, cancellationToken);
        
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(tokenDto.Email, cancellationToken)
             ?? throw new NotFoundException("User not found");
        
        user.IsConfirmed = true;
    
        await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        cancellationToken.ThrowIfCancellationRequested();

        return "Email successfully confirmed!";
    }

    public async Task<string> ForgotPasswordAsync(string? email, string callbackUrl, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new BadRequestException("invalid email address");
        }
        
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken)
            ?? throw new NotFoundException("User not found");
        
        var emailToken = await _tokenService.GenerateEmailToken(user, TokenTypes.ResetPassword, cancellationToken);
        var link = GenerateEmailTokenLink(callbackUrl, email, emailToken);

        BackgroundJob.Enqueue(() =>
            _emailService.SendEmailAsync(
                email,
                "Reset password",
                EmailTemplates.ResetPasswordEmailBody(link),
                cancellationToken)
        );

        return emailToken;
    }
    
    public async Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
    {
        var token = await _cache.GetStringAsync(resetPasswordDto.Email, cancellationToken)
                    ?? throw new NotFoundException("Token is not found or expired");
        
        CheckTokens(token, resetPasswordDto.Token);

        await _cache.RemoveAsync(resetPasswordDto.Email, cancellationToken);
        
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(resetPasswordDto.Email, cancellationToken)
             ?? throw new NotFoundException("User not found");
        
        user.PasswordHash = _passwordHasher.Generate(resetPasswordDto.Password, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
    
        await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        return $"Password successfully reset!\n Password: {resetPasswordDto.Password}";
    }
    
    public async Task<bool> ValidateResetPasswordAsync(EmailTokenDto resetPasswordRequestTokenDto, CancellationToken cancellationToken)
    {
        var token = await _unitOfWork.TokenModelRepository.GetByToken(resetPasswordRequestTokenDto.Token, cancellationToken)
            ?? throw new NotFoundException("Token not found");

        if (token.ExpiresAt < DateTime.UtcNow || token.IsUsed)
        {
            throw new BadRequestException("Token expired");
        }
        
        return true;
    }

    public async Task<string> RecoverAccountAsync(string? email, string callbackUrl, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new BadRequestException("invalid email address");
        }
        
        var user = await _unitOfWork.UserRepository.GetDeletedUserByEmailAsync(email, cancellationToken)
            ?? throw new NotFoundException("User not found");

        if (!user.IsConfirmed)
        {
            throw new BadRequestException("To recover account you must activated it in past time.");
        }
        
        var emailToken = await _tokenService.GenerateEmailToken(user, TokenTypes.RecoverAccount, cancellationToken);
        var link = GenerateEmailTokenLink(callbackUrl, email, emailToken);
        
        await _emailService.SendEmailAsync(
            email, 
            "Recover account",
            EmailTemplates.RecoverEmailBody(link), 
            cancellationToken);

        return emailToken;
    }

    public async Task<string> RestoreAccountAsync(EmailTokenDto emailTokenDto, CancellationToken cancellationToken)
    {
        var token = await _cache.GetStringAsync(emailTokenDto.Email, cancellationToken)
                    ?? throw new NotFoundException("Token is not found or expired");
        
        CheckTokens(token, emailTokenDto.Token);

        await _cache.RemoveAsync(emailTokenDto.Email, cancellationToken);
        
        var user = await _unitOfWork.UserRepository.GetDeletedUserByEmailAsync(emailTokenDto.Email, cancellationToken)
                   ?? throw new NotFoundException("User not found");
        
        user.IsDeleted = false;
    
        await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        return "Account successfully recovered!";
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

    private void CheckTokens(string token, string encodedToken)
    {
        var decodedTokenFromDto = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedToken));
        if (String.CompareOrdinal(decodedTokenFromDto, token) != 0)
        {
            throw new BadRequestException("Invalid token");
        }
    }
}