using MediatR;
using MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;
using MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;
using MeetingService.Application.UseCases.Participants.Command.UpdateParticipantStatusCommand;
using MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;
using MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;
using MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace MeetingService.Api.Controllers;

[ApiController]
[Route("participants")]
public class ParticipantController(
    IMediator mediator
    ) : Controller
{
    [HttpPost("meetings/{meetingId:guid}")]
    public async Task<IResult> AddParticipantToMeeting([FromRoute]Guid meetingId, [FromForm] AddParticipantToMeetingCommand command, CancellationToken cancellationToken)
    {
        command.MeetingId = meetingId;
        
        var participant = await mediator.Send(command, cancellationToken);
        return Results.Ok(participant);
    }

    [HttpDelete]
    public async Task<IResult> RemoveParticipantFromMeeting([FromQuery] RemoveParticipantFromMeetingCommand command,
        CancellationToken cancellationToken)
    {
        var participant = await mediator.Send(command, cancellationToken);
        return Results.Ok(participant);
    }

    [HttpPatch("meetings/{meetingId:guid}/status")]
    public async Task<IResult> UpdateParticipantStatus([FromRoute] Guid meetingId, 
        [FromForm] UpdateParticipantStatusCommand command, CancellationToken cancellationToken)
    {
        command.MeetingId = meetingId;
        
        var participant = await mediator.Send(command, cancellationToken);
        return Results.Ok(participant);
    }

    [HttpGet("meetings/{meetingId:guid}/email")]
    public async Task<IResult> GetParticipantsByEmail([FromRoute] Guid meetingId, [FromForm] GetParticipantByEmailQuery query,
        CancellationToken cancellationToken)
    {
        query.MeetingId = meetingId;
        
        var participant = await mediator.Send(query, cancellationToken);
        return Results.Ok(participant);
    }

    [HttpGet]
    public async Task<IResult> GetParticipant([FromQuery] GetParticipantQuery query,
        CancellationToken cancellationToken)
    {
        var participant = await mediator.Send(query, cancellationToken);
        return Results.Ok(participant);
    }

    [HttpGet("meetings/{meetingId:guid}")]
    public async Task<IResult> GetParticipantsForMeeting([FromRoute] GetParticipantsByMeetingIdQuery query,
        CancellationToken cancellationToken)
    {
        var participants = await mediator.Send(query, cancellationToken);
        return Results.Ok(participants);
    }
    
    [HttpGet("health")]
    public async Task<IResult> Health()
    {
        return Results.Ok("Healthy");
    }
}