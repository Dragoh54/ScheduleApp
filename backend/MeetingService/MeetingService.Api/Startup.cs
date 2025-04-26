using Hangfire;
using MeetingService.Application.Extensions;
using MeetingService.Application.Mappings;
using MeetingService.Application.Settings;
using MeetingService.DataAccess.Extensions;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DataAccess.UnitOfWork;
using ExceptionHandlerMiddleware = MeetingService.Api.Middlewares.ExceptionHandlerMiddleware;

namespace MeetingService.Api;

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
        services.Configure<EmailSettings>(Configuration.GetSection(nameof(EmailSettings)));
        
        services.AddControllersWithViews();
        
        services.AddMeetingDbContext(Configuration);
        services.AddHangfire(Configuration);
        services.AddRedis(Configuration);
        
        services.AddRepositories();
        services.AddUnitOfWork();

        GeneralMappingConfig.RegisterMappers();
        
        services.AddServices();
        
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddMediatRServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, string[] args)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseStaticFiles();
        app.UseRouting();
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.UseHangfireServer();
        app.UseHangfireDashboard();

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