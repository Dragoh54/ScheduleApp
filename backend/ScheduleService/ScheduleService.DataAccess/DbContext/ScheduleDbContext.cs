using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ScheduleService.DataAccess.Indexes;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Settings;
using ScheduleService.DomainModel.Models;
using MongoCollectionSettings = ScheduleService.DataAccess.Settings.MongoCollectionSettings;

namespace ScheduleService.DataAccess.DbContext;

public class ScheduleDbContext : IScheduleDbContext
{
    private IMongoDatabase Database { get; set; }
    private MongoClient MongoClient { get; set; }
    private MongoDbSettings MongoDbSettings { get; set; }
    private MongoCollectionSettings MongoCollectionSettings { get; set; }
    
    public IClientSessionHandle Session { get; set; }
    
    private readonly List<Func<Task>> _commands = new List<Func<Task>>();

    public ScheduleDbContext(IServiceProvider services)
    {
        MongoDbSettings = services.GetRequiredService<MongoDbSettings>();
        MongoCollectionSettings = services.GetRequiredService<MongoCollectionSettings>();
        
        Database =  services.GetService<IMongoDatabase>()
                    ?? throw new NullReferenceException("Database");
        
        MongoClient = services.GetService<MongoClient>()
            ?? throw new NullReferenceException("MongoClient");
        
        ConfigureIndexes();
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

    private void ConfigureIndexes()
    {
        AvailabilityTemplateConfiguration.ConfigureIndexes(
            Database.GetCollection<AvailabilityTemplate>(MongoCollectionSettings.AvailabilityTemplates));
        
        CalendarDayConfiguration.ConfigureIndexes(
            Database.GetCollection<CalendarDay>(MongoCollectionSettings.CalendarDays));
    }
}