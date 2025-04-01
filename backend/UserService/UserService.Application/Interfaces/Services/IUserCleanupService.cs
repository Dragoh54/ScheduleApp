namespace UserService.Application.Services;

public interface IUserCleanupService
{
    public Task FindAndCleanupUsers(CancellationToken cancellationToken);
}