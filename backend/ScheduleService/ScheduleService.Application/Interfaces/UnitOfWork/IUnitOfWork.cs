using ScheduleService.Application.Interfaces.Repositories;

namespace ScheduleService.Application.Interfaces.UnitOfWork;


public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Availability templates collection
    /// </summary>
    public IAvailabilityTemplateRepository AvailabilityTemplates { get; }
    
    /// <summary>
    /// Meetings collection
    /// </summary>
    public IMeetingRepository Meetings { get; }
    
    /// <summary>
    /// Commit changes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> Commit(CancellationToken cancellationToken);
}