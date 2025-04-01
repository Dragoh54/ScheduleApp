using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Interfaces.UnitOfWork;

namespace UserService.Application.Services;

public class UserCleanupService(IUnitOfWork unitOfWork) : IUserCleanupService
{
    public async Task FindAndCleanupUsers(CancellationToken cancellationToken)
    {
        var oneYearAgo = DateTime.UtcNow.AddYears(-1);
        
        var inactiveUsers = await unitOfWork.UserRepository.GetOldUsersAsync(oneYearAgo, cancellationToken);
        
        
        
        throw new NotImplementedException();
    }
}