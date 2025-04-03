using Microsoft.AspNetCore.Diagnostics;
using ScheduleService.Application.Mapping;

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
        
        GeneralConfig.RegisterMappers();
        
        services.AddControllers();
        
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