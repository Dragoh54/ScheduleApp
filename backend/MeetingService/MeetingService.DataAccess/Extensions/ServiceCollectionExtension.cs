using Hangfire;
using Hangfire.PostgreSql;
using MeetingService.DataAccess.Interfaces.Repositories;
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
    
    
}