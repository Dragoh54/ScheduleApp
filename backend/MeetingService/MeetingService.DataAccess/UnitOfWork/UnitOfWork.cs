using MeetingService.Application.Interfaces.Repositories;
using MeetingService.Application.Interfaces.UnitOfWork;

namespace MeetingService.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IMeetingRepository meetingRepository, IParticipantRepository participantRepository,
        IScheduledJobRepository scheduledJobRepository, MeetingServiceDbContext dbContext)
    {
        MeetingRepository = meetingRepository;
        ParticipantRepository = participantRepository;
        ScheduledJobRepository = scheduledJobRepository;
        _dbContext = dbContext;
    }
    
    private readonly MeetingServiceDbContext _dbContext;
    private bool _disposed;

    public IMeetingRepository MeetingRepository { get; }
    public IParticipantRepository ParticipantRepository { get; }
    public IScheduledJobRepository ScheduledJobRepository { get; }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
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
            _dbContext.Dispose();
        }

        _disposed = true;
    }
}