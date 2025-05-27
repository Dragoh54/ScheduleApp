using MediatR;
using MeetingService.Api.Extensions;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.MeetingDto.Requests;
using MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;
using MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;
using MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;
using MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;
using MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;
using MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;
using MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingService.Api.Controllers;

[ApiController]
[Authorize]
[Route("meetings")]
public class MeetingController : Controller
{
    public MeetingController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    private readonly IMediator _mediator;
    
    [HttpPost]
    public async Task<IResult> CreateMeeting([FromForm] CreateMeetingRequestDto requestDto, CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.GetBearerToken();
        
        var command = new CreateMeetingCommand(requestDto, accessToken);
        
        var meeting = await _mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }
    
    [HttpDelete]
    public async Task<IResult> DeleteMeeting([FromQuery] DeleteMeetingRequestDto requestDto, CancellationToken cancellationToken)
    {
        var command = new DeleteMeetingCommand(requestDto, HttpContext.GetBearerToken());
        
        var meeting = await _mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }
    
    [HttpPatch("{meetingId:guid}/reschedule")]
    public async Task<IResult> RescheduleMeeting([FromRoute] Guid meetingId, 
        [FromForm] RescheduleMeetingRequestDto requestDto, CancellationToken cancellationToken)
    {
        var command = new RescheduleMeetingCommand(meetingId, requestDto);
        
        var meeting = await _mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }
    
    [HttpPatch("{meetingId:guid}/information")]
    public async Task<IResult> UpdateInformation([FromRoute] Guid meetingId, 
        [FromForm] UpdateMeetingInformationRequestDto requestDto, CancellationToken cancellationToken)
    {
        var command = new UpdateMeetingInformationCommand(meetingId, requestDto);
        
        var meeting = await _mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }
    
    [HttpPatch("{meetingId:guid}/status")]
    public async Task<IResult> UpdateMeetingStatus([FromRoute] Guid meetingId,
        [FromForm] UpdateMeetingStatusRequestDto requestDto, CancellationToken cancellationToken)
    {
        var command = new UpdateMeetingStatusCommand(meetingId, requestDto);
        
        var meeting = await _mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }
    
    [HttpGet("user/{OrganizerId:guid}")]
    public async Task<IResult> GetMeetingsOrganizedByUser([FromRoute] GetMeetingsOrganizedByUserQuery query, CancellationToken cancellationToken)
    {
        var meetings = await _mediator.Send(query, cancellationToken);
        
        return Results.Ok(meetings);
    }
    
    [HttpGet("{MeetingId:guid}")]
    public async Task<IResult> GetMeetings([FromRoute] GetMeetingWithParticipantsQuery query, CancellationToken cancellationToken)
    {
        var meeting = await _mediator.Send(query, cancellationToken);
        
        return Results.Ok(meeting);
    }
}