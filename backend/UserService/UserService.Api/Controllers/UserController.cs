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
public class UserController : Controller
{
    private readonly JwtOptions _jwtOptions;
    private readonly IUserService _service;

    public UserController(IUserService service, IOptions<JwtOptions> jwtOptions)
    {
        _service = service;
        _jwtOptions = jwtOptions.Value;
    }
    
    [HttpPost("register")]
    public async Task<IResult> Register(RegisterDto user, CancellationToken cancellationToken)
    {
        var resultUser = await _service.RegisterUser(user, cancellationToken);
        return Results.Ok(resultUser);
    }
    
    [HttpPost("login")]
    [ServiceFilter(typeof(AllowAnonymousOnlyFilter))]
    public async Task<IResult> Login(LoginUserDto user, CancellationToken cancellationToken)
    {
        var (token, refreshToken) = await _service.Login(user, cancellationToken);
        
        HttpContext.Response.Cookies.Append("tasty-cookies", token, new CookieOptions()
        {
            Domain = "localhost",
            Secure = true,
            HttpOnly = true,
            MaxAge = TimeSpan.FromMinutes(_jwtOptions.ExpiresMinutes)
        });

        HttpContext.Response.Cookies.Append("not-a-refresh-token-cookies", refreshToken, new CookieOptions()
        {
            Domain = "localhost",
            Secure = true,
            HttpOnly = true,
            MaxAge = TimeSpan.FromDays(_jwtOptions.ExpiresDays)
        });

        return Results.Ok(new { Token = token, RefreshToken = refreshToken });
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IResult> Logout(CancellationToken cancellationToken)
    {
        string? refreshToken = HttpContext.Request.Cookies["not-a-refresh-token-cookies"];
        var result = await _service.Logout(refreshToken, cancellationToken);

        HttpContext.Response.Cookies.Delete("tasty-cookies");
        HttpContext.Response.Cookies.Delete("not-a-refresh-token-cookies");
        
        return Results.Ok(result);
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> GetUsers(CancellationToken cancellationToken)
    {
        var resultUsers = await _service.GetUsers(cancellationToken);
        return Results.Ok(resultUsers);
    }
    
    [HttpGet("get")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> GetUser([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var resultUsers = await _service.GetUserById(id, cancellationToken);
        return Results.Ok(resultUsers);
    }
    
    [HttpGet("find")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> GetUserByEmail([FromQuery] string email,CancellationToken cancellationToken)
    {
        var resultUsers = await _service.GetUserByEmailWithRoles(email, cancellationToken);
        return Results.Ok(resultUsers);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IResult> UpdateUser(UpdateUserDto user, CancellationToken cancellationToken)
    {
        var resultUser = await _service.UpdateUser(user, cancellationToken);
        return Results.Ok(resultUser);
    }

    
    [HttpDelete("delete")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteUser([FromQuery] DeleteUserDto user, CancellationToken cancellationToken)
    {
        var deletedUser = await _service.DeleteUser(user, cancellationToken);
        return Results.Ok(deletedUser);
    }
    
    [HttpPost("soft-delete")]
    [Authorize]
    public async Task<IResult> SoftDeleteUser([FromQuery] DeleteUserDto user, CancellationToken cancellationToken)
    {
        var deletedUser = await _service.SoftDelete(user, cancellationToken);
        return Results.Ok(deletedUser);
    }

    [HttpGet("health")]
    public async Task<IResult> GetHealth()
    {
        return Results.Ok("Healthy");
    }
}