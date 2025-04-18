using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UserService.Api.Requirements;
using UserService.Application.Dto.EmailDtos;
using UserService.Application.Dto.RoleDtos;
using UserService.Application.Dto.UserDtos;
using UserService.Application.Validator;
using UserService.Application.Validator.EmailValidators;
using UserService.Application.Validator.RoleValidators;
using UserService.Application.Validator.UserValidators;
using UserService.DataAccess.Enums;

namespace UserService.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddSwaggerGenAuthenticationExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme()
            {
                BearerFormat = "Jwt",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Reference = new OpenApiReference()
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    jwtSecurityScheme, 
                    Array.Empty<string>()
                }
            });
        });
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
    
    public static void AddApiAuthenfication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration["JWTSecretKey"] ?? throw new NullReferenceException("JWTSecretKey is null");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions))
                };
            });

        services.AddScoped<IAuthorizationHandler, RolePermissionHandler>();

        services.AddAuthorization(options =>
        {
            var admin = Roles.Admin.ToString();
            var organizationAdmin = Roles.OrganizationAdmin.ToString();

            options.AddPolicy(admin, policy =>
            {
                policy.AddRequirements(new RolePermissionRequirement(admin));
            });
            
            options.AddPolicy(organizationAdmin, policy =>
            {
                policy.AddRequirements(new RolePermissionRequirement(organizationAdmin));
            });
        });
    }
}