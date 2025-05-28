using UserService.Grpc;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
Startup.ConfigureBuilder(builder);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment, args);

await app.RunAsync();