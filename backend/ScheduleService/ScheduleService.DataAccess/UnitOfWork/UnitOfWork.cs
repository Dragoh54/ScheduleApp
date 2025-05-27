using MongoDB.Driver;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DataAccess.DbContext;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Repositories;

namespace ScheduleService.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IScheduleDbContext _context;

    public IAvailabilityTemplateRepository AvailabilityTemplates { get; }
    public IMeetingRepository Meetings { get; }

    public UnitOfWork(IScheduleDbContext context, IAvailabilityTemplateRepository availabilityTemplateRepository,
        IMeetingRepository meetingRepository)
    {
        _context = context;
        AvailabilityTemplates = availabilityTemplateRepository;
        Meetings = meetingRepository;
    }

    public async Task<bool> Commit(CancellationToken cancellationToken)
    {
        var changeAmount = await _context.SaveChangesAsync(cancellationToken);

        return changeAmount > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}