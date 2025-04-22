using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScheduleService.DataAccess.Indexes;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Repositories;
using ScheduleService.DataAccess.Settings;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.DbContext;

public class ScheduleDbContext : IScheduleDbContext
{
    private IMongoDatabase Database { get; set; }
    private IMongoClient MongoClient { get; set; }
    
    private readonly MongoDbSettings _mongoDbSettings;
    
    public IClientSessionHandle Session { get; set; }
    
    private readonly List<Func<Task>> _commands = new List<Func<Task>>();

    public ScheduleDbContext(IServiceProvider services, IOptions<MongoDbSettings> settings)
    { 
        _mongoDbSettings = settings.Value;
        
        MongoClient = new MongoClient(_mongoDbSettings.MongoConnectionString);
        Database = MongoClient.GetDatabase(_mongoDbSettings.MongoDatabaseName);
        
        // Database =  services.GetService<IMongoDatabase>()
        //             ?? throw new NullReferenceException("Database");
        //
        // MongoClient = services.GetService<IMongoClient>()
        //     ?? throw new NullReferenceException("MongoClient");
        
        ConfigureIndexes();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        using (Session = await MongoClient.StartSessionAsync(cancellationToken: cancellationToken))
        {
            Session.StartTransaction();

            var commandTasks = _commands.Select(c => c());

            await Task.WhenAll(commandTasks);

            await Session.CommitTransactionAsync(cancellationToken);
        }

        return _commands.Count;
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return Database.GetCollection<T>(name);
    }
    
    private void DisposeSession()
    {
        if (Session is not { } session) return;
        session.Dispose();
        Session = null;
    }

    public void Dispose()
    {
        DisposeSession();
        GC.SuppressFinalize(this);
    }

    public void AddCommand(Func<Task> func)
    {
        _commands.Add(func);
    }

    private void ConfigureIndexes()
    {
        AvailabilityTemplateConfiguration.ConfigureIndexes(
            Database.GetCollection<AvailabilityTemplate>("availability_templates"));
        
        MeetingConfiguration.ConfigureIndexes(
            Database.GetCollection<Meeting>("meetings"));
    }
}