using Hangfire;
using Hangfire.PostgreSql;
using MeetingService.Application.Interfaces.Repositories;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingService.DataAccess.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();
        services.AddScoped<IScheduledJobRepository, ScheduledJobRepository>();
    }
    
    public static void AddMeetingDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MeetingServiceDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("MeetingServiceDbContext")));
    }
    
    public static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(options =>
        {
            options.UsePostgreSqlStorage(configuration.GetConnectionString("Hangfire"));
        });
        
        services.AddHangfireServer();
    }
    
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "MeetingService_";
        });
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}