using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Database;
using UserService.DataAccess.Extensions;

namespace UserService.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        
        services.AddDbContext<UserServiceDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("UserServiceDbContext")));
        services.AddRepositories();

        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, string[] args)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}