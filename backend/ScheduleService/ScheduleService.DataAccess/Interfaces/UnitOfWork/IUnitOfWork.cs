using ScheduleService.DataAccess.Interfaces.Repositories;

namespace ScheduleService.DataAccess.Interfaces.UnitOfWork;


public interface IUnitOfWork : IDisposable
{
    IAvailabilityTemplateRepository AvailabilityTemplates { get; }
    ICalendarDayRepository CalendarDays { get; }
    Task<bool> Commit();
}