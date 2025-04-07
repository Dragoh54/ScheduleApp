using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Interfaces.Repositories;

public interface ICalendarDayRepository : IBaseRepository<CalendarDay>
{
    /// <summary>
    /// Get entity filtered by certain user and date
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="date"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<CalendarDay?> GetByDateAsync(Guid userId, DateOnly date, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get list of certain days for user between start and end
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<CalendarDay>> GetForPeriodAsync(Guid userId, DateOnly start, DateOnly end, CancellationToken cancellationToken);
}