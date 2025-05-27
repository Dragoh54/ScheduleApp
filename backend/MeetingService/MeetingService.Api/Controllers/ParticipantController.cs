using MediatR;
using MeetingService.Api.Extensions;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.ParticipantDto.Requests;
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
public class ParticipantController : Controller
{
    public ParticipantController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    private readonly IMediator _mediator;
    
    [HttpPost("meetings/{meetingId:guid}")]
    public async Task<IResult> AddParticipantToMeeting([FromRoute]Guid meetingId, 
        [FromForm] AddParticipantToMeetingRequestDto requestDto, CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.GetBearerToken();
        
        var callbackUrl = Url.RouteUrl(
            "ConfirmParticipation",
            values: new { meetingId },
            protocol: Request.Scheme);
        
        var command = new AddParticipantToMeetingCommand(meetingId, requestDto, callbackUrl!, accessToken);
        
        var participant = await _mediator.Send(command, cancellationToken);
        
        return Results.Ok(participant);
    }
    
    [HttpGet("meetings/{meetingId:guid}/confirm", Name = "ConfirmParticipation")]
    public async Task<IResult> ConfirmParticipantStatus([FromRoute] Guid meetingId, 
        [FromQuery] ConfirmParticipantStatusRequestDto requestDto, CancellationToken cancellationToken)
    {
        var command = new ConfirmParticipationCommand(meetingId, requestDto);
        
        var participant = await _mediator.Send(command, cancellationToken);
        
        return Results.Ok(participant);
    }
    
    [HttpDelete("meetings/{meetingId:guid}")]
    public async Task<IResult> RemoveParticipantFromMeeting([FromRoute]Guid meetingId, 
        [FromQuery] RemoveParticipantFromMeetingRequestDto requestDto, CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.GetBearerToken();
        
        var command = new RemoveParticipantFromMeetingCommand(meetingId, requestDto, accessToken);
        
        var participant = await _mediator.Send(command, cancellationToken);
        
        return Results.Ok(participant);
    }

    [HttpGet]
    public async Task<IResult> GetParticipant([FromQuery] GetParticipantQuery query,
        CancellationToken cancellationToken)
    {
        var participant = await _mediator.Send(query, cancellationToken);
        return Results.Ok(participant);
    }

    [HttpGet("meetings/{MeetingId:guid}")]
    public async Task<IResult> GetParticipantsForMeeting([FromRoute] GetParticipantsByMeetingIdQuery query,
        CancellationToken cancellationToken)
    {
        var participants = await _mediator.Send(query, cancellationToken);
        return Results.Ok(participants);
    }
}