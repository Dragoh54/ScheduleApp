using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserService.Api.Interfaces;
using UserService.Application.Dto;
using UserService.DataAccess.Handlers.Jwt;

namespace UserService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenficationController
{
    private readonly JwtOptions _jwtOptions;
    private readonly IUserService _service;

    public AuthenficationController(IUserService service, IOptions<JwtOptions> jwtOptions)
    {
        _service = service;
        _jwtOptions = jwtOptions.Value;
    }
    
    [HttpPost("/register")]
    public async Task<IResult> Register(RegisterDto user, CancellationToken cancellationToken)
    {
        var resultUser = await _service.RegisterUser(user, cancellationToken);
        return Results.Ok(resultUser);
    }
}