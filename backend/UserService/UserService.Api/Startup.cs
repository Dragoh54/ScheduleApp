using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using UserService.Api.Extensions;
using UserService.Api.Filters;
using UserService.Api.Middlewares;
using UserService.Application.Extensions;
using UserService.Application.Handlers.Email;
using UserService.Application.Mapper;
using UserService.DataAccess.Database;
using UserService.DataAccess.Database.UnitOfWork;
using UserService.DataAccess.Extensions;
using UserService.DataAccess.Handlers.Jwt;
using UserService.DataAccess.Interfaces.UnitOfWork;

namespace UserService.Api;

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
        services.Configure<JwtOptions>(Configuration.GetSection(nameof(JwtOptions)));
        services.Configure<EmailSettings>(Configuration.GetSection(nameof(EmailSettings)));

        services.AddControllersWithViews();

        services.AddApiAuthenfication(configuration);
        
        services.AddUserDbContext(configuration);
        services.AddRedis(configuration);
        services.AddHangfire(configuration);
        
        GeneralConfig.RegisterMappers();
        
        services.AddRepositories();
        services.AddJwt();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddApplicationServices();
        
        services.AddScoped<AllowAnonymousOnlyFilter>();
        
        services.AddValidation();
        
        services.AddControllers();
        
        services.AddSwaggerGenAuthenticationExtension();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, string[] args)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHangfireDashboard();
        }
        
        app.UseHangfireServer();
        
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