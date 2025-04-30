using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MeetingService.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSecret = configuration["JWTSecretKey"]
                        ?? throw new NullReferenceException("JWTSecretKey is null");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                };
            });

        services.AddAuthorization(); 
    }
    
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
                Reference = new OpenApiReference
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
}