using MongoDB.Driver;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.DataAccess.UnitOfWork;

public class UnitOfWork(
        IMongoDatabase database,
        IAvailabilityTemplateRepository availabilityTemplateRepository,
        ICalendarDayRepository calendarDayRepository
    ) : IUnitOfWork
{
    public IAvailabilityTemplateRepository AvailabilityTemplateRepository { get; } = availabilityTemplateRepository;
    public ICalendarDayRepository CalendarDayRepository { get; } = calendarDayRepository;
}