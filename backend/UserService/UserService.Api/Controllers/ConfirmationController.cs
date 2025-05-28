using Microsoft.AspNetCore.Mvc;
using UserService.Api.Extensions;
using UserService.Application.Dto.EmailDtos;
using UserService.Application.Interfaces.Services;

namespace UserService.Api.Controllers;

[ApiController]
[Route("confirmations")]
public class ConfirmationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public ConfirmationController(IAuthenticationService authService)
    {
        _authenticationService = authService;
    }
    
    /// <summary>
    /// create link to confirm account and send to email
    /// </summary>
    [HttpPost]
    public async Task<IResult> ConfirmEmailSend(CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.GetBearerToken();
        var callbackUrl = Url.RouteUrl(
            "EmailConfirmation",
            values: null,
            protocol: Request.Scheme);
    
        var token = await _authenticationService.ConfirmEmailSendAsync(accessToken, callbackUrl!, cancellationToken);
    
        return Results.Ok(token);
    }
    
    /// <summary>
    /// accept link from email and confirm account
    /// </summary>
    [HttpGet(Name = "EmailConfirmation")]
    public async Task<IResult> ConfirmEmailReceive([FromQuery] EmailTokenDto emailTokenDto, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.ConfirmEmailReceiveAsync(emailTokenDto, cancellationToken);
    
        return Results.Ok(result);
    }
}