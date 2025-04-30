using Hangfire;
using MeetingService.Api.Extensions;
using MeetingService.Application.Extensions;
using MeetingService.Application.Mappings;
using MeetingService.Application.Settings;
using MeetingService.DataAccess.Extensions;
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
        services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));
        
        services.AddControllersWithViews();
        
        services.AddApiAuthentication(Configuration);
        
        services.AddMeetingDbContext(Configuration);
        services.AddHangfire(Configuration);
        services.AddRedis(Configuration);
        
        services.AddRepositories();
        services.AddUnitOfWork();

        GeneralMappingConfig.RegisterMappers();
        
        services.AddServices();
        services.AddProviders();
        
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddMediatRServices();
        
        
        services.AddSwaggerGenAuthenticationExtension();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, string[] args)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        
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