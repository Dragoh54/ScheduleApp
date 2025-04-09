using MongoDB.Driver;

namespace ScheduleService.DataAccess.Interfaces.DbContext;

public interface IScheduleDbContext : IDisposable
{
    public IClientSessionHandle Session { get; set; }
    void AddCommand(Func<Task> func);
    Task<int> SaveChanges(CancellationToken cancellationToken);
    IMongoCollection<T> GetCollection<T>(string name);
}