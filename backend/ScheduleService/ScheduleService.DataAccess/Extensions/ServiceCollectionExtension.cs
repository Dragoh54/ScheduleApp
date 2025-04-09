using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScheduleService.DataAccess.DbContext;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DataAccess.Repositories;
using ScheduleService.DataAccess.Settings;
using MongoCollectionSettings = ScheduleService.DataAccess.Settings.MongoCollectionSettings;

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
            var settings = options.GetRequiredService<IOptions<MongoCollectionSettings>>().Value;
            return new AvailabilityTemplateRepository(dbContext, settings.AvailabilityTemplates);
        });
        
        services.AddScoped<ICalendarDayRepository>(options => 
        {
            var dbContext = options.GetRequiredService<IScheduleDbContext>();
            var settings = options.GetRequiredService<IOptions<MongoCollectionSettings>>().Value;
            return new CalendarDayRepository(dbContext, settings.CalendarDays);
        });
    }
    
    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(options => 
        {
            var settings = options.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.MongoConnectionString);
        });

        // services.AddSingleton<IMongoClient>(options => 
        // {
        //     var settings = options.GetRequiredService<IOptions<MongoDbSettings>>().Value;
        //     return new MongoClient(settings.MongoReplicaConnectionString);
        // });
        
        services.AddScoped<IMongoDatabase>(options => 
        {
            var settings = options.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = options.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.MongoDatabaseName);
        });
    }
}