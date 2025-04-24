using MeetingService.DataAccess.Interfaces.Repositories;

namespace MeetingService.DataAccess.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Repository for working with Meeting entities
    /// </summary>
    public IMeetingRepository MeetingRepository { get; }
    
    /// <summary>
    /// Repository for working with Participant entities
    /// </summary>
    public IParticipantRepository ParticipantRepository { get; }
    
    /// <summary>
    /// Save all changes in UnitOfWork context
    /// </summary>
    /// <returns></returns>
    public Task SaveChangesAsync();
}