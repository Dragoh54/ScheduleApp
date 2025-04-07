using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DataAccess.Repositories;
using ScheduleService.DataAccess.Settings;

namespace ScheduleService.DataAccess.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(options =>
        {
            var settings = options.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.MongoConnectionString);
        });
        
        services.AddScoped<IMongoDatabase>(options => 
        {
            var settings = options.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = options.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.MongoDatabaseName);
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAvailabilityTemplateRepository, AvailabilityTemplateRepository>();
        services.AddScoped<ICalendarDayRepository, CalendarDayRepository>();
    }
}