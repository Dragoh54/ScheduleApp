using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Dto.EmailDtos;
using UserService.Application.Dto.RoleDtos;
using UserService.Application.Dto.UserDtos;
using UserService.Application.Handlers.Jwt;
using UserService.Application.Handlers.JwtUtilities;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Interfaces.Services;
using UserService.Application.Services;
using UserService.Application.Validator;
using UserService.Application.Validator.EmailValidators;
using UserService.Application.Validator.RoleValidators;
using UserService.Application.Validator.UserValidators;

namespace UserService.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, Services.UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IEmailService, EmailService>(); 
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailCacheService, EmailCacheService>();
    }
    
    public static void AddJwt(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}