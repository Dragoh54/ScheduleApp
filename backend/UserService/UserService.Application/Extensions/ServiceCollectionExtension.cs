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
        services.AddScoped<ICacheService, CacheService>();
    }
    
    public static void AddValidation(this IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        
        services.AddScoped<IValidator<LoginUserDto>, LoginUserValidator>();
        services.AddScoped<IValidator<RegisterDto>, RegisterUserValidator>();
        services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>();
        services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
        
        services.AddScoped<IValidator<EmailTokenDto>, EmailTokenValidator>();
        services.AddScoped<IValidator<ResetPasswordDto>, ResetPasswordValidator>();
        
        services.AddScoped<IValidator<AddRoleDto>, AddRoleValidator>();
        services.AddScoped<IValidator<RoleDto>, RoleDtoValidator>();
    }
    
    public static void AddJwt(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}