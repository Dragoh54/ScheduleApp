using Microsoft.Extensions.DependencyInjection;
using UserService.DataAccess.Handlers.Jwt;
using UserService.DataAccess.Handlers.JwtUtilities;
using UserService.DataAccess.Interfaces.Auth;

namespace UserService.DataAccess.Extensions;

public static class JwtServices
{
    public static void AddJwt(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}