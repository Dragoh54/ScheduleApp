using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.DataAccess.Database;
using UserService.DataAccess.Database.Repositories;
using UserService.DataAccess.Interfaces.Repositories;

namespace UserService.DataAccess.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenModelRepository, TokenModelModelRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }

    public static void AddUserDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserServiceDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("UserServiceDbContext")));
    }

    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "UserService_";
        });
    }

    public static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(options =>
        {
            options.UsePostgreSqlStorage(configuration.GetConnectionString("Hangfire"));
        });
        
        services.AddHangfireServer();
    }
}