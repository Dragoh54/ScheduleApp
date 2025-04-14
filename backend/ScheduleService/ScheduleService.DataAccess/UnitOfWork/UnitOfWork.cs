using MongoDB.Driver;
using ScheduleService.DataAccess.DbContext;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DataAccess.Repositories;

namespace ScheduleService.DataAccess.UnitOfWork;

public class UnitOfWork(
    IScheduleDbContext context,
    IAvailabilityTemplateRepository availabilityTemplateRepository
    ) : IUnitOfWork
{
    public IAvailabilityTemplateRepository AvailabilityTemplates { get; } = availabilityTemplateRepository;

    public async Task<bool> Commit(CancellationToken cancellationToken)
    {
        var changeAmount = await context.SaveChanges(cancellationToken);

        return changeAmount > 0;
    }

    public void Dispose()
    {
        context.Dispose();
    }
}