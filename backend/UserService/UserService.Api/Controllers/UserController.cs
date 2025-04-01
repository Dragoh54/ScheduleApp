using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using UserService.Api.Filters;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.DataAccess.Handlers.Jwt;

namespace UserService.Api.Controllers;

[ApiController]
[Route("users")]
[Authorize]
public class UserController(IUserService service) : Controller
{
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> GetUsers([FromQuery] PaginatedPageUsers query, CancellationToken cancellationToken)
    {
        var resultUsers = await service.GetUsers(query, cancellationToken);
        return Results.Ok(resultUsers);
    }
    
    [HttpGet("get")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> GetUser([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var resultUsers = await service.GetUserById(id, cancellationToken);
        return Results.Ok(resultUsers);
    }
    
    [HttpGet("find")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> GetUserByEmail([FromQuery] string email,CancellationToken cancellationToken)
    {
        var resultUsers = await service.GetUserByEmail(email, cancellationToken);
        return Results.Ok(resultUsers);
    }

    [HttpPut("add-admin-role")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> AddAdminRoleToUser([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var resultUser = await service.AddAdminRole(userId, cancellationToken);
        return Results.Ok(resultUser);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IResult> UpdateUser(UpdateUserDto user, CancellationToken cancellationToken)
    {
        var resultUser = await service.UpdateUser(user, cancellationToken);
        return Results.Ok(resultUser);
    }
    
    [HttpDelete("delete")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteUser([FromQuery] DeleteUserDto user, CancellationToken cancellationToken)
    {
        var deletedUser = await service.DeleteUser(user, cancellationToken);
        return Results.Ok(deletedUser);
    }
    
    [HttpPost("soft-delete")]
    [Authorize]
    public async Task<IResult> SoftDeleteUser([FromQuery] DeleteUserDto user, CancellationToken cancellationToken)
    {
        var deletedUser = await service.SoftDelete(user, cancellationToken);
        return Results.Ok(deletedUser);
    }
}