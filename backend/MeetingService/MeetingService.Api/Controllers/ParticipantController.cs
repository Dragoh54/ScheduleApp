using MediatR;
using MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;
using Microsoft.AspNetCore.Mvc;

namespace MeetingService.Api.Controllers;

[ApiController]
[Route("participants")]
public class ParticipantController(
    IMediator mediator
    ) : Controller
{
    [HttpPost("/meetings/{meetingId:guid}")]
    public async Task<IResult> AddParticipantToMeeting([FromRoute]Guid meetingId, [FromBody] AddParticipantToMeetingCommand command, CancellationToken cancellationToken)
    {
        command.MeetingId = meetingId;
        var participant = await mediator.Send(command, cancellationToken);
        return Results.Ok(participant);
    }
    
    [HttpGet]
    public async Task<IResult> Health()
    {
        return Results.Ok("Healthy");
    }
}