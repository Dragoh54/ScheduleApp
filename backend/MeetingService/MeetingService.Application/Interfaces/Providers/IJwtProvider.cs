using System.Security.Claims;

namespace MeetingService.Application.Interfaces.Providers;

public interface IJwtProvider
{
    public ClaimsPrincipal ValidateToken(string token);
    public Task<string> GetClaimFromToken(string token, string claimType);
    public Task<Guid> GetUserIdFromToken(string token);
}