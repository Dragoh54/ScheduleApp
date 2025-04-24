using MeetingService.DataAccess.Interfaces.Repositories;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.DataAccess.UnitOfWork;

public class UnitOfWork(
    IMeetingRepository meetingRepository,
    IParticipantRepository participantRepository,
    MeetingServiceDbContext dbContext
    ) : IUnitOfWork
{
    private bool _disposed;

    public IMeetingRepository MeetingRepository { get; } = meetingRepository;
    public IParticipantRepository ParticipantRepository { get; } = participantRepository;

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