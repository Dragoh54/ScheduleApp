using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.DataAccess.Handlers.Jwt;

namespace UserService.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController
{
    private readonly JwtOptions _jwtOptions;
    private readonly IUserService _service;

    public UserController(IUserService service, IOptions<JwtOptions> jwtOptions)
    {
        _service = service;
        _jwtOptions = jwtOptions.Value;
    }
    
    [HttpPost("/register")]
    public async Task<IResult> Register(RegisterDto user, CancellationToken cancellationToken)
    {
        var resultUser = await _service.RegisterUser(user, cancellationToken);
        return Results.Ok(resultUser);
    }

    [HttpGet]
    public async Task<IResult> GetUsers(CancellationToken cancellationToken)
    {
        var resultUsers = await _service.GetUsers(cancellationToken);
        return Results.Ok(resultUsers);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var resultUsers = await _service.GetUserById(id, cancellationToken);
        return Results.Ok(resultUsers);
    }
    
    [HttpGet("find")]
    public async Task<IResult> GetUserByEmail([FromQuery] string email,CancellationToken cancellationToken)
    {
        var resultUsers = await _service.GetUserByEmail(email, cancellationToken);
        return Results.Ok(resultUsers);
    }

    [HttpPut("/update")]
    public async Task<IResult> UpdateUser(UpdateUserDto user, CancellationToken cancellationToken)
    {
        var resultUser = await _service.UpdateUser(user, cancellationToken);
        return Results.Ok(resultUser);
    }

    
    [HttpDelete("/delete")]
    public async Task<IResult> Register([FromQuery] DeleteUserDto user, CancellationToken cancellationToken)
    {
        var deletedUser = await _service.DeleteUser(user, cancellationToken);
        return Results.Ok(deletedUser);
    }

    [HttpGet("/health")]
    public async Task<IResult> GetHealth()
    {
        return Results.Ok("Healthy");
    }
}