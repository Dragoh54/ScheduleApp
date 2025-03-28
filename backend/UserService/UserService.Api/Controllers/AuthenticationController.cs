using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.Application.Dto.EmailDtos;
using UserService.DataAccess.Handlers.Jwt;

namespace UserService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IAuthenticationService authService, IOptions<JwtOptions> jwtOptions)
    : Controller
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    [HttpPost("register")]
    public async Task<IResult> Register(RegisterDto user, CancellationToken cancellationToken)
    {
        var resultUser = await authService.Register(user, cancellationToken);
        
        return Results.Ok(resultUser);
    }
    
    //TODO: ADD HANGFIRE TO DELETE USED TOKENS
    [HttpPost("login")]
    public async Task<IResult> Login(LoginUserDto user, CancellationToken cancellationToken)
    {
        var (token, refreshToken) = await authService.Login(user, cancellationToken);
        
        HttpContext.Response.Cookies.Append("not-a-refresh-token-cookies", refreshToken, new CookieOptions()
        {
            Domain = "localhost",
            Secure = true,
            HttpOnly = true,
            MaxAge = TimeSpan.FromDays(_jwtOptions.RefreshExpiresDays)
        });

        return Results.Ok(new { Token = token, RefreshToken = refreshToken });
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IResult> Logout(CancellationToken cancellationToken)
    {
        var refreshToken = HttpContext.Request.Cookies["not-a-refresh-token-cookies"];
        var result = await authService.Logout(refreshToken, cancellationToken);

        HttpContext.Response.Cookies.Delete("not-a-refresh-token-cookies");
        
        return Results.Ok(result);
    }
    
    //TODO: ADD HANGFIRE FOR DELETING TOKENS IF THEY ARE EXPIRES
    [HttpPost("forgot-password")]
    public async Task<IResult> ForgotPassword([FromQuery] string email, CancellationToken cancellationToken)
    {
        var callbackUrl = Url.RouteUrl(
            "ResetPassword",
            values: null,
            protocol: Request.Scheme);
    
        var token = await authService.ForgotPasswordAsync(email, callbackUrl!, cancellationToken);
    
        return Results.Ok(token);
    }
    
    [HttpGet("reset-password", Name = "ResetPassword")]
    public async Task<IResult> OnResetPassword([FromQuery] EmailTokenDto resetPasswordRequest, CancellationToken cancellationToken)
    {
        var success = await authService.ValidateResetPasswordAsync(resetPasswordRequest, cancellationToken);
        return Results.Ok(success);
    }
    
    [HttpPost("reset-password")]
    public async Task<IResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
    {
        var result = await authService.ResetPasswordAsync(resetPasswordDto, cancellationToken);
    
        return Results.Ok(result);
    }
}