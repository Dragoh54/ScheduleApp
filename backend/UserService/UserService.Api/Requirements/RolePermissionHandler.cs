using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UserService.Api.Requirements;

public class RolePermissionHandler : AuthorizationHandler<RolePermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolePermissionRequirement requirement)
    {
        var roleClaims = context.User
            .FindAll(claim => claim.Type == ClaimTypes.Role).ToList();

        if (!roleClaims.Any())
        {
            context.Fail(new AuthorizationFailureReason(this, "This token has no role"));
            return Task.CompletedTask;
        }
        
        var roles = roleClaims.Select(c => c.Value);
        
        string[] requireRoles = context.Requirements
            .OfType<RolePermissionRequirement>()
            .Select(r => r.Role)
            .ToArray();
        
        bool hasRequiredRoles = roles.Any(role => requireRoles.Contains(role));

        if (!hasRequiredRoles)
        {
            context.Fail(new AuthorizationFailureReason(this, "This token has no role"));
            return Task.CompletedTask;
        }
        
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}