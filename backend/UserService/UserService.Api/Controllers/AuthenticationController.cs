using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.DataAccess.Handlers.Jwt;

namespace UserService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : Controller
{
    private readonly JwtOptions _jwtOptions;
    private readonly IAuthenticationService _authService;
    private readonly ITokenService _tokenService;
    
    public AuthenticationController(IAuthenticationService authService, ITokenService tokenService, IOptions<JwtOptions> jwtOptions)
    {
        _authService = authService;
        _tokenService = tokenService;
        _jwtOptions = jwtOptions.Value;
    }
    
    [HttpPost("register")]
    public async Task<IResult> Register(RegisterDto user, CancellationToken cancellationToken)
    {
        var resultUser = await _authService.Register(user, cancellationToken);
        return Results.Ok(resultUser);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IResult> Login(LoginUserDto user, CancellationToken cancellationToken)
    {
        var (token, refreshToken) = await _authService.Login(user, cancellationToken);
        
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
        var result = await _authService.Logout(refreshToken, cancellationToken);

        HttpContext.Response.Cookies.Delete("not-a-refresh-token-cookies");
        
        return Results.Ok(result);
    }
}