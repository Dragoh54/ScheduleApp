using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserService.Api.Interfaces;
using UserService.Application.Dto.EmailDtos;
using UserService.Application.Handlers.Email;

namespace UserService.Api.Controllers;

[ApiController]
[Route("confirmation")]
public class EmailController(
    IAuthenticationService authService
    ) : Controller
{
    [HttpPost("sending")]
    [AllowAnonymous]
    public async Task<IResult> ConfirmEmailSend(CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
        var callbackUrl = Url.RouteUrl(
            "EmailConfirmation",
            values: null,
            protocol: Request.Scheme);
    
        var token = await authService.ConfirmEmailSendAsync(accessToken, callbackUrl!, cancellationToken);
    
        return Results.Ok(token);
    }
    
    [HttpGet("receiving", Name = "EmailConfirmation")]
    public async Task<IResult> ConfirmEmailReceive([FromQuery] EmailTokenDto emailTokenDto, CancellationToken cancellationToken)
    {
        var result = await authService.ConfirmEmailReceiveAsync(emailTokenDto, cancellationToken);
    
        return Results.Ok(result);
    }
}