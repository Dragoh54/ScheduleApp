using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserService.Api.Interfaces;
using UserService.Application.Dto.EmailDtos;
using UserService.Application.Handlers.Email;

namespace UserService.Api.Controllers;

[ApiController]
public class EmailController(IEmailService emailService, IUserService userService, IConfiguration configuration) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("confirmation/send")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmailSend([FromQuery]ConfirmEmailDto confirmEmailDto,  CancellationToken cancellationToken)
    {
        var success = await emailService.SendEmailAsync(confirmEmailDto, cancellationToken);

        return Ok(success);
    }
}