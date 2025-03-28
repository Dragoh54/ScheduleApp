using UserService.DataAccess.Enums;

namespace UserService.Api.Interfaces;

public interface ICacheService
{
    public Task AddEmailTokenToCacheAsync(string email, string token, TokenTypes type, CancellationToken cancellationToken);
}