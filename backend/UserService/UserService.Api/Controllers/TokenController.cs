using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Interfaces;

namespace UserService.Api.Controllers;

[ApiController]
public class TokenController(ITokenService tokenService) : Controller
{
    //TODO: ADD AND TAKE FROM CACHE
    [HttpPost("/refresh")]
    [AllowAnonymous]
    public async Task<IResult> Refresh(CancellationToken cancellationToken)
    {
        var refreshToken = HttpContext.Request.Cookies["not-a-refresh-token-cookies"];
        var token = await tokenService.RefreshAccessToken(refreshToken, cancellationToken);
        
        return Results.Ok(token);
    }
}