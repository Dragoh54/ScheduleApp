using MediatR;
using MeetingService.Api.Extensions;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;
using MeetingService.Application.UseCases.Participants.Command.ConfirmParticipationCommand;
using MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;
using MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;
using MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingService.Api.Controllers;

[ApiController]
[Authorize]
[Route("participants")]
public class ParticipantController(
    IMediator mediator
    ) : Controller
{                            
    //todo: add [OrganizatorOnly]
    [HttpPost("meetings/{meetingId:guid}")]
    public async Task<IResult> AddParticipantToMeeting([FromRoute]Guid meetingId, 
        [FromForm] AddParticipantToMeetingDto dto, CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.GetBearerToken();
        
        var callbackUrl = Url.RouteUrl(
            "ConfirmParticipation",
            values: new { meetingId },
            protocol: Request.Scheme);
        
        var command = new AddParticipantToMeetingCommand(meetingId, dto, callbackUrl!, accessToken);
        
        var participant = await mediator.Send(command, cancellationToken);
        
        return Results.Ok(participant);
    }
    
    [HttpGet("meetings/{meetingId:guid}/confirm", Name = "ConfirmParticipation")]
    public async Task<IResult> ConfirmParticipantStatus([FromRoute] Guid meetingId, 
        [FromQuery] ConfirmParticipantStatusDto dto, CancellationToken cancellationToken)
    {
        var command = new ConfirmParticipationCommand(meetingId, dto);
        
        var participant = await mediator.Send(command, cancellationToken);
        
        return Results.Ok(participant);
    }
    
    //todo: add [OrganizatorOnly]
    [HttpDelete("meetings/{meetingId:guid}")]
    public async Task<IResult> RemoveParticipantFromMeeting([FromRoute]Guid meetingId, 
        [FromQuery] RemoveParticipantFromMeetingDto dto, CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.GetBearerToken();
        
        var command = new RemoveParticipantFromMeetingCommand(meetingId, dto, accessToken);
        
        var participant = await mediator.Send(command, cancellationToken);
        
        return Results.Ok(participant);
    }

    [HttpGet]
    public async Task<IResult> GetParticipant([FromQuery] GetParticipantQuery query,
        CancellationToken cancellationToken)
    {
        var participant = await mediator.Send(query, cancellationToken);
        return Results.Ok(participant);
    }

    [HttpGet("meetings/{MeetingId:guid}")]
    public async Task<IResult> GetParticipantsForMeeting([FromRoute] GetParticipantsByMeetingIdQuery query,
        CancellationToken cancellationToken)
    {
        var participants = await mediator.Send(query, cancellationToken);
        return Results.Ok(participants);
    }
}