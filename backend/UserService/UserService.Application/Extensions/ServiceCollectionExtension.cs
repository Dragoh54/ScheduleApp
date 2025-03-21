using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.Application.Services;
using UserService.Application.Validator;
using UserService.Application.Validator.UserValidators;
using UserService.DataAccess.Handlers.Jwt;
using UserService.DataAccess.Handlers.JwtUtilities;
using UserService.DataAccess.Interfaces.Auth;

namespace UserService.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, Services.UserService>();
        services.AddScoped<IRefreshTokenService, TokenService>();
        services.AddScoped<IRoleService, RoleService>();
    }
    
    public static void AddValidation(this IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        
        services.AddScoped<IValidator<DeleteUserDto>, DeleteUserValidator>();
        services.AddScoped<IValidator<LoginUserDto>, LoginUserValidator>();
        services.AddScoped<IValidator<RegisterDto>, RegisterUserValidator>();
        services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>();
        services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
    }
    
    public static void AddJwt(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}