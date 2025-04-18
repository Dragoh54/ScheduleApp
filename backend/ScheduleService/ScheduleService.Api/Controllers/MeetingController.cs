using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.Application.UseCases.Meeting.Command.CreateMeetingCommand;

namespace ScheduleService.Api.Controllers;

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
}