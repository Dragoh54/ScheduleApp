using ScheduleService.DataAccess.DatabaseContext;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.DataAccess.UnitOfWork;

public class UnitOfWork(
    ScheduleServiceDbContext dbContext
    ) : IUnitOfWork
{
    private readonly ScheduleServiceDbContext _dbContext = dbContext;
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}