namespace ScheduleService.DataAccess.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    //TODO: ADD REPOS
    Task SaveChangesAsync();
}