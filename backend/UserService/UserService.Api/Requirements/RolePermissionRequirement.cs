using Microsoft.AspNetCore.Authorization;

namespace UserService.Api.Requirements;

public record RolePermissionRequirement(string role) : IAuthorizationRequirement
{
    public string Role { get; } = role;
}