using Microsoft.AspNetCore.Authorization;

namespace UserService.Api.Requirements;

public record RolePermissionRequirement(
    string Role
    ) : IAuthorizationRequirement
{
    public string Role { get; } = Role;
}