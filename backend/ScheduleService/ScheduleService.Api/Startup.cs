using ScheduleService.Api.Filtres;
using ScheduleService.Application.Extensions;
using ScheduleService.Application.Mapping;
using ScheduleService.DataAccess.Extensions;
using ScheduleService.DataAccess.Persistence;
using ScheduleService.DataAccess.Settings;
using ExceptionHandlerMiddleware = ScheduleService.Api.Middlewares.ExceptionHandlerMiddleware;

namespace ScheduleService.Api;

public class Startup(
    IConfiguration configuration
    )
{
    private IConfiguration Configuration { get; } = configuration;

    public static void ConfigureBuilder(WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        
        services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));
            
        MongoDbPersistence.Configure();
        
        services.AddDatabase(Configuration);
        services.AddDbContext();
        services.AddUnitOfWork();
        services.AddRepositories(Configuration);

        services.AddMediatRServices();
        
        GeneralConfig.RegisterMappers();
        
        services.AddControllers();
        services.AddSwaggerGen(options =>
        {
            options.SchemaFilter<DayOfWeekDictionaryFilter>();
        });
        
        //services.AddSwaggerGenAuthenticationExtension();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, string[] args)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always,
        });
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}