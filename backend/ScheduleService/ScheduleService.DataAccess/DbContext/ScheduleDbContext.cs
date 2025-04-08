using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using ScheduleService.DataAccess.Interfaces.DbContext;

namespace ScheduleService.DataAccess.DbContext;

public class ScheduleDbContext : IScheduleDbContext
{
    private IMongoDatabase Database { get; set; }
    public IClientSessionHandle Session { get; set; }
    private MongoClient MongoClient { get; set; }
    private readonly List<Func<Task>> _commands = new List<Func<Task>>();
    private readonly IConfiguration _configuration;
    
    public ScheduleDbContext(IServiceProvider services, IConfiguration configuration)
    {
        _configuration = configuration;

        MongoClient = services.GetService<MongoClient>()
            ?? throw new NullReferenceException("MongoClient");

        Database = services.GetService<IMongoDatabase>()
            ?? throw new NullReferenceException("Database");
    }
    

    public async Task<int> SaveChanges()
    {
        using (Session = await MongoClient.StartSessionAsync())
        {
            Session.StartTransaction();

            var commandTasks = _commands.Select(c => c());

            await Task.WhenAll(commandTasks);

            await Session.CommitTransactionAsync();
        }

        return _commands.Count;
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return Database.GetCollection<T>(name);
    }

    public void Dispose()
    {
        Session?.Dispose();
        GC.SuppressFinalize(this);
    }

    public void AddCommand(Func<Task> func)
    {
        _commands.Add(func);
    }
}