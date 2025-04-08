using MongoDB.Driver;
using ScheduleService.DataAccess.DbContext;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DataAccess.Repositories;

namespace ScheduleService.DataAccess.UnitOfWork;

public class UnitOfWork(
    IScheduleDbContext context,
    IAvailabilityTemplateRepository availabilityTemplateRepository,
    ICalendarDayRepository calendarDayRepository
    ) : IUnitOfWork
{
    public IAvailabilityTemplateRepository AvailabilityTemplates { get; } = availabilityTemplateRepository;
    public ICalendarDayRepository CalendarDays { get; } = calendarDayRepository;

    public async Task<bool> Commit()
    {
        var changeAmount = await context.SaveChanges();

        return changeAmount > 0;
    }

    public void Dispose()
    {
        context.Dispose();
    }
}