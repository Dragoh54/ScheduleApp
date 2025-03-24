using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserService.Api.Extensions;
using UserService.Api.Filters;
using UserService.Api.Interfaces;
using UserService.Api.Middlewares;
using UserService.Application.Dto;
using UserService.Application.Extensions;
using UserService.Application.Mapper;
using UserService.Application.Validator;
using UserService.Application.Validator.UserValidators;
using UserService.DataAccess.Database;
using UserService.DataAccess.Database.UnitOfWork;
using UserService.DataAccess.Extensions;
using UserService.DataAccess.Handlers.Jwt;
using UserService.DataAccess.Interfaces.UnitOfWork;

namespace UserService.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureBuilder(WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<JwtOptions>(Configuration.GetSection(nameof(JwtOptions)));

        services.AddControllersWithViews();

        services.AddApiAuthenfication(Configuration);
        
        services.AddDbContext<UserServiceDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("UserServiceDbContext")));
        services.AddRepositories();
        services.AddJwt();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddApplicationServices();
        
        services.AddScoped<AllowAnonymousOnlyFilter>();
        
        services.AddValidation();
        
        services.AddControllers();
        
        services.AddSwaggerGen(options =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme()
            {
                BearerFormat = "Jwt",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Reference = new OpenApiReference()
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            
            options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    jwtSecurityScheme, 
                    Array.Empty<string>()
                }
            });
        });
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