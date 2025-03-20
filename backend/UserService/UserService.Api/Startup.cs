using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
        services.AddEndpointsApiExplorer();
        
        services.AddDbContext<UserServiceDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("UserServiceDbContext")));
        services.AddRepositories();
        services.AddJwt();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, Application.Services.UserService>();
        
        services.AddControllers();
        
        services.AddValidation();
        
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, string[] args)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}