using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleService.Api.Controllers;

[ApiController]
[Route("meetings")]
public class MeetingController(
        IMediator mediator
    ) : Controller
{
    
}