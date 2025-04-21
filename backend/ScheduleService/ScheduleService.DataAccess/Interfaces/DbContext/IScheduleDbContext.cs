using MongoDB.Driver;

namespace ScheduleService.DataAccess.Interfaces.DbContext;

public interface IScheduleDbContext : IDisposable
{
    /// <summary>
    /// current session
    /// </summary>
    public IClientSessionHandle Session { get; set; }
    
    /// <summary>
    /// add command for future transaction
    /// </summary>
    /// <param name="func"></param>
    void AddCommand(Func<Task> func);
    
    /// <summary>
    /// Save changes to database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>number of commands in transaction</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Return collection of T entities
    /// </summary>
    /// <param name="name"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IMongoCollection<T> GetCollection<T>(string name);
}