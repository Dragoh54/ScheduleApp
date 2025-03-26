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
public class EmailController(IEmailService emailService, IAuthenticationService authService, IConfiguration configuration) : Controller
{
    [HttpPost("send")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmailSend(CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);;
        var callbackUrl = Url.RouteUrl(
            "EmailConfirmation",
            values: null,
            protocol: Request.Scheme);
    
        var token = await authService.ConfirmEmailSendAsync(accessToken, callbackUrl!, cancellationToken);
    
        return Ok(token);
    }
    
    [HttpGet("receive", Name = "EmailConfirmation")]
    public async Task<IActionResult> ConfirmEmailReceive([FromQuery] ConfirmEmailDto confirmEmailRequest, CancellationToken cancellationToken)
    {
        var token = await authService.ConfirmEmailReceiveAsync(confirmEmailRequest, cancellationToken);
    
        return Ok($"Email confirmed!\nToken: {token}");
    }
}