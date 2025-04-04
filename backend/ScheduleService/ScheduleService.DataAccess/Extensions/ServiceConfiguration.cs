using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleService.DataAccess.DatabaseContext;

namespace ScheduleService.DataAccess.Extensions;

public static class ServiceConfiguration
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ScheduleServiceDbContext>(options =>
        {
            options.UseMongoDB(configuration["MongoConnectionString"]!, configuration["DatabaseName"]!);
        });
    }
}