using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    public Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<UserEntity?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}