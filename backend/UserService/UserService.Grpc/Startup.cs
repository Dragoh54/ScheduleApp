using Mapster;
using UserService.DataAccess.Database.UnitOfWork;
using UserService.DataAccess.Extensions;
using UserService.DataAccess.Interfaces.UnitOfWork;
using UserService.Grpc.Mappings;

namespace UserService.Grpc;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    private IConfiguration Configuration { get; }

    public static void ConfigureBuilder(WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        services.AddUserDbContext(Configuration);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddRepositories();
        
        TypeAdapterConfig.GlobalSettings.Scan(typeof(UserMappingConfig).Assembly);
        
        services.AddGrpc(options => options.EnableDetailedErrors = true);
    }

    public void Configure(WebApplication app, IWebHostEnvironment env, string[] args)
    {
        app.UseCors();
        
        app.MapGrpcService<Services.UserService>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    }
}