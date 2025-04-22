using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserService.Application.Features.Jwt;
using UserService.Application.Interfaces.Services;

namespace UserService.Api.Controllers;

[ApiController]
[Route("tokens")]
public class TokenController(
    ITokenService tokenService, 
    IOptions<JwtOptions> jwtOptions
    ) : Controller
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    /// <summary>
    /// Refresh access and refresh token
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IResult> Refresh(CancellationToken cancellationToken)
    {
        var refreshToken = HttpContext.Request.Cookies["not-a-refresh-token-cookies"];
        var (newAccessToken, newRefreshToken) = await tokenService.RefreshAccessToken(refreshToken, cancellationToken);
        
        HttpContext.Response.Cookies.Append("not-a-refresh-token-cookies", newRefreshToken, new CookieOptions()
        {
            SameSite = SameSiteMode.Lax,
            Domain = "localhost",
            Secure = true,
            HttpOnly = true,
            MaxAge = TimeSpan.FromDays(_jwtOptions.RefreshExpiresDays)
        });
        
        return Results.Ok(new { Token = newAccessToken, RefreshToken = newRefreshToken });
    }
}