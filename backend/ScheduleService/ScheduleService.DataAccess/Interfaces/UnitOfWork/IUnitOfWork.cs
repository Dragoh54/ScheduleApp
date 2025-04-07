using ScheduleService.DataAccess.Interfaces.Repositories;

namespace ScheduleService.DataAccess.Interfaces.UnitOfWork;


//TODO: ADD UNIT OF WORK
public interface IUnitOfWork
{
    public IAvailabilityTemplateRepository AvailabilityTemplateRepository { get; }
    public ICalendarDayRepository CalendarDayRepository { get; }
}