using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dto.EmailDtos;
using UserService.Application.Interfaces.Services;

namespace UserService.Api.Controllers;

[ApiController]
[Route("confirmations")]
public class ConfirmationController(
    IAuthenticationService authService
) : Controller
{
    /// <summary>
    /// create link to confirm account and send to email
    /// </summary>
    [HttpPost]
    public async Task<IResult> ConfirmEmailSend(CancellationToken cancellationToken)
    {
        //TODO: move it to extension method
        var accessToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
        var callbackUrl = Url.RouteUrl(
            "EmailConfirmation",
            values: null,
            protocol: Request.Scheme);
    
        var token = await authService.ConfirmEmailSendAsync(accessToken, callbackUrl!, cancellationToken);
    
        return Results.Ok(token);
    }
    
    /// <summary>
    /// accept link from email and confirm account
    /// </summary>
    [HttpGet(Name = "EmailConfirmation")]
    public async Task<IResult> ConfirmEmailReceive([FromQuery] EmailTokenDto emailTokenDto, CancellationToken cancellationToken)
    {
        var result = await authService.ConfirmEmailReceiveAsync(emailTokenDto, cancellationToken);
    
        return Results.Ok(result);
    }
}