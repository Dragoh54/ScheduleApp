using MediatR;
using MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;
using Microsoft.AspNetCore.Mvc;

namespace MeetingService.Api.Controllers;

[ApiController]
[Route("meetings")]
public class MeetingController(
    IMediator mediator
    ) : Controller
{
    [HttpPost]
    public async Task<IResult> CreateMeeting([FromBody] CreateMeetingCommand command, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(command, cancellationToken);
        return Results.Ok(meeting);
    }
    
    [HttpGet]
    public async Task<IResult> Health()
    {
        return Results.Ok("Healthy");
    }
}