using Microsoft.AspNetCore.Mvc;
using UserService.Api.Interfaces;

namespace UserService.Api.Controllers;

[ApiController]
public class EmailController : Controller
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public EmailController(IUserService userService, IConfiguration configuration, ILogger<EmailController> logger)
    {
        _userService = userService;
        _configuration = configuration;
    }
}