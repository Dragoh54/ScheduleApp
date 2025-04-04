using UserService.DataAccess.Enums;

namespace UserService.Application.Interfaces.Services;

public interface ICacheService
{
    public Task AddEmailTokenToCacheAsync(string email, string token, TokenTypes type, CancellationToken cancellationToken);
}