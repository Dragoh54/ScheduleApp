using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Interfaces.Repositories;

public interface IAvailabilityTemplateRepository : IBaseRepository<AvailabilityTemplate>
{
    /// <summary>
    /// Get default(active) template for user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<AvailabilityTemplate?> GetDefaultTemplateAsync(Guid userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// get all templates for user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<AvailabilityTemplate>> GetUserTemplatesAsync(Guid userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// set template by id to default
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="templateId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task SetDefaultTemplateAsync(Guid userId, Guid templateId, CancellationToken cancellationToken);
    //public Task<AvailabilityTemplate?> SetDefaultTemplateAsync(Guid userId, Guid templateId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Checks if user free according his template
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> IsUserFreeAsync(Guid userId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken);
}