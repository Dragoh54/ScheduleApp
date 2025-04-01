using Microsoft.Extensions.DependencyInjection;
using UserService.DataAccess.Database.Repositories;
using UserService.DataAccess.Interfaces;

namespace UserService.DataAccess.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenModelRepository, TokenModelModelRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }
}