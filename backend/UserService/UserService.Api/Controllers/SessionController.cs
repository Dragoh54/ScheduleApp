using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserService.Application.Dto;
using UserService.DataAccess.Handlers.Jwt;
using IAuthenticationService = UserService.Api.Interfaces.IAuthenticationService;

namespace UserService.Api.Controllers;

[ApiController]
[Route("sessions")]
public class SessionController(
    IAuthenticationService authService, 
    IOptions<JwtOptions> jwtOptions
    ) : Controller
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    /// <summary>
    /// Login user
    /// </summary>
    [HttpPost]
    public async Task<IResult> Login(LoginUserDto user, CancellationToken cancellationToken)
    {
        var (token, refreshToken) = await authService.Login(user, cancellationToken);
        
        HttpContext.Response.Cookies.Append("not-a-refresh-token-cookies", refreshToken, new CookieOptions()
        {
            SameSite = SameSiteMode.Lax,
            Domain = "localhost",
            Secure = true,
            HttpOnly = true,
            MaxAge = TimeSpan.FromDays(_jwtOptions.RefreshExpiresDays)
        });

        return Results.Ok(new { Token = token, RefreshToken = refreshToken });
    }
    
    /// <summary>
    /// Logout user
    /// </summary>
    [HttpDelete]
    [Authorize]
    public async Task<IResult> Logout(CancellationToken cancellationToken)
    {
        var refreshToken = HttpContext.Request.Cookies["not-a-refresh-token-cookies"];
        var result = await authService.Logout(refreshToken, cancellationToken);

        HttpContext.Response.Cookies.Delete("not-a-refresh-token-cookies");
        
        return Results.Ok(result);
    }
}