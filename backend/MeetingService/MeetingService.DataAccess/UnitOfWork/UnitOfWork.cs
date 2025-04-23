using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.DataAccess.UnitOfWork;

public class UnitOfWork(
    MeetingServiceDbContext dbContext
    ) : IUnitOfWork
{
    private bool _disposed;
    
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            dbContext.Dispose();
        }

        _disposed = true;
    }
}