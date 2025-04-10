using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Extensions;
using UserService.Application.Dto.EmailDtos;
using UserService.Application.Dto.RoleDtos;
using UserService.Application.Dto.UserDtos;
using UserService.Application.Interfaces.Services;
using UserService.DataAccess.Enums;

namespace UserService.Api.Controllers;

[ApiController]
[Route("users")]
[Authorize]
public class UserController(
    IUserService service,
    IAuthenticationService authService
    ) : Controller
{
    /// <summary>
    /// Get all users with pagination
    /// </summary>
    [HttpGet]
    public async Task<IResult> GetUsers([FromQuery] PaginatedPageUsers query, CancellationToken cancellationToken)
    {
        var resultUsers = await service.GetUsers(query, cancellationToken);
        
        return Results.Ok(resultUsers);
    }
    
    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id:Guid}")]
    public async Task<IResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var resultUsers = await service.GetUserById(id, cancellationToken);
        
        return Results.Ok(resultUsers);
    }
    
    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost]
    public async Task<IResult> Register(RegisterDto user, CancellationToken cancellationToken)
    {
        var resultUser = await authService.Register(user, cancellationToken);
        
        return Results.Ok(resultUser);
    }
    
    /// <summary>
    /// Update user information
    /// </summary>
    [HttpPut("{id:Guid}")]
    [Authorize]
    public async Task<IResult> UpdateUser([FromRoute] Guid id, UpdateUserDto user, CancellationToken cancellationToken)
    {
        var resultUser = await service.UpdateUser(id, user, cancellationToken);
        
        return Results.Ok(resultUser);
    }

    /// <summary>
    /// Soft delete user account
    /// </summary>
    [HttpDelete("me")]
    [Authorize]
    public async Task<IResult> SoftDeleteUser(CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.GetBearerToken();
        var deletedUser = await service.SoftDelete(accessToken, cancellationToken);
        
        return Results.Ok(deletedUser);
    }
    
    /// <summary>
    /// Initiate account restoration process
    /// </summary>
    [HttpPost("{email}/restore")]
    public async Task<IResult> RestoreAccount([FromRoute] string email, CancellationToken cancellationToken)
    {
        var callbackUrl = Url.RouteUrl(
            "RestoreAccount",
            values: null,
            protocol: Request.Scheme);
    
        var token = await authService.RecoverAccountAsync(email, callbackUrl!, cancellationToken);
    
        return Results.Ok(token);
    }
    
    /// <summary>
    /// Complete account restoration
    /// </summary>
    [HttpGet("restore", Name = "RestoreAccount")]
    public async Task<IResult> OnRestoreAccount([FromQuery] EmailTokenDto restoreAccountRequest, CancellationToken cancellationToken)
    {
        var result = await authService.RestoreAccountAsync(restoreAccountRequest, cancellationToken);
    
        return Results.Ok(result);
    }
    
    /// <summary>
    /// Add role to user (Admin only)
    /// </summary>
    [HttpPut("{id:Guid}/roles")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> AddAdminRoleToUser([FromRoute]Guid id, [FromQuery] Roles role, CancellationToken cancellationToken)
    {
        var dto = new AddRoleDto{Role = role, UserId = id};
        var resultUser = await service.AddRole(dto, cancellationToken);
        
        return Results.Ok(resultUser);
    }
}