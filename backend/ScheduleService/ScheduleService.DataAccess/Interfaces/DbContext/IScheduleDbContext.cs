using MongoDB.Driver;

namespace ScheduleService.DataAccess.Interfaces.DbContext;

public interface IScheduleDbContext : IDisposable
{
    public IClientSessionHandle Session { get; set; }
    Task<IClientSessionHandle> StartSessionAsync(CancellationToken cancellationToken);
    void AddCommand(Func<Task> func);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    IMongoCollection<T> GetCollection<T>(string name);
}