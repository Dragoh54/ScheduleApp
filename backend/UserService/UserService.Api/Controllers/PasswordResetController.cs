using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dto.EmailDtos;
using UserService.Application.Interfaces.Services;

namespace UserService.Api.Controllers;

[ApiController]
[Route("password-resets")]
public class PasswordResetController(
    IAuthenticationService authService
    ) : Controller
{
    /// <summary>
    /// Create link to reset password
    /// </summary>
    [HttpPost]
    public async Task<IResult> ForgotPassword([FromQuery] string email, CancellationToken cancellationToken)
    {
        var callbackUrl = Url.RouteUrl(
            "ResetPassword",
            values: null,
            protocol: Request.Scheme);
    
        var token = await authService.ForgotPasswordAsync(email, callbackUrl!, cancellationToken);
    
        return Results.Ok(token);
    }
    
    /// <summary>
    /// Validate link to confirm resetting
    /// </summary>
    [HttpGet(Name = "ResetPassword")]
    public async Task<IResult> OnResetPassword([FromQuery] EmailTokenDto resetPasswordRequest, CancellationToken cancellationToken)
    {
        var success = await authService.ValidateResetPasswordAsync(resetPasswordRequest, cancellationToken);
        
        return Results.Ok(success);
    }
    
    /// <summary>
    /// set new user password
    /// </summary>
    [HttpPost("new-password")]
    public async Task<IResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
    {
        var result = await authService.ResetPasswordAsync(resetPasswordDto, cancellationToken);
    
        return Results.Ok(result);
    }
}