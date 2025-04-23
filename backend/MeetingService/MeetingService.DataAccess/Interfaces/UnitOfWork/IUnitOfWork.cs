namespace MeetingService.DataAccess.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    //TODO: ADD REPOSITORIES
    
    /// <summary>
    /// Save all changes in UnitOfWork context
    /// </summary>
    /// <returns></returns>
    public Task SaveChangesAsync();
}