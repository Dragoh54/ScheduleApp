using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DataAccess.DbContext;
using ScheduleService.DataAccess.Indexes;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Repositories;
using ScheduleService.DataAccess.Settings;

namespace ScheduleService.DataAccess.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddScoped<IScheduleDbContext, ScheduleDbContext>();
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAvailabilityTemplateRepository>(options => 
        {
            var dbContext = options.GetRequiredService<IScheduleDbContext>();
            return new AvailabilityTemplateRepository(dbContext);
        });
        
        services.AddScoped<IMeetingRepository>(options => 
        {
            var dbContext = options.GetRequiredService<IScheduleDbContext>();
            return new MeetingRepository(dbContext);
        });
    }
}