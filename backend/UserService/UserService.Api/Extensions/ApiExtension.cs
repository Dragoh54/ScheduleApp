using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using UserService.Api.Requirements;
using UserService.DataAccess.Enums;

namespace UserService.Api.Extensions;

public static class ApiExtension
{
    public static void AddApiAuthenfication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration["JWTSecretKey"];

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

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["tasty-cookies"];

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddScoped<IAuthorizationHandler, RolePermissionHandler>();

        services.AddAuthorization(options =>
        {
            string admin = Role.Admin.ToString();
            string organizationAdmin = Role.OrganizationAdmin.ToString();

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