using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    public Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<IEnumerable<UserEntity>?> GetDeletedUsersAsync(CancellationToken cancellationToken);
    //TODO: ADD GET USER AND USERS WITH ROLES
}