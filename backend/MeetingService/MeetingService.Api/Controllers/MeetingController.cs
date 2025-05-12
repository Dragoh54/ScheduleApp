using MediatR;
using MeetingService.Api.Extensions;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.MeetingDtos;
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

//TODO: ADD NOTIFICATION TIME
[ApiController]
[Authorize]
[Route("meetings")]
public class MeetingController(
    IMediator mediator
    ) : Controller
{
    [HttpPost]
    public async Task<IResult> CreateMeeting([FromForm] CreateMeetingDto dto, CancellationToken cancellationToken)
    {
        var accessToken = HttpContext.GetBearerToken();
        
        var command = new CreateMeetingCommand(dto, accessToken);
        
        var meeting = await mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }

    [HttpDelete]
    public async Task<IResult> DeleteMeeting([FromQuery] DeleteMeetingDto dto, CancellationToken cancellationToken)
    {
        var command = new DeleteMeetingCommand(dto, HttpContext.GetBearerToken());
        
        var meeting = await mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }
    
    [HttpPatch("{meetingId:guid}/reschedule")]
    public async Task<IResult> RescheduleMeeting([FromRoute] Guid meetingId, 
        [FromForm] RescheduleMeetingDto dto, CancellationToken cancellationToken)
    {
        var command = new RescheduleMeetingCommand(meetingId, dto);
        
        var meeting = await mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }
    
    [HttpPatch("{meetingId:guid}/information")]
    public async Task<IResult> UpdateInformation([FromRoute] Guid meetingId, 
        [FromForm] UpdateMeetingInformationDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateMeetingInformationCommand(meetingId, dto);
        
        var meeting = await mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }
    
    [HttpPatch("{meetingId:guid}/status")]
    public async Task<IResult> UpdateMeetingStatus([FromRoute] Guid meetingId,
        [FromForm] UpdateMeetingStatusDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateMeetingStatusCommand(meetingId, dto);
        
        var meeting = await mediator.Send(command, cancellationToken);
        
        return Results.Ok(meeting);
    }

    [HttpGet("user/{OrganizerId:guid}")]
    public async Task<IResult> GetMeetingsOrganizedByUser([FromRoute] GetMeetingsOrganizedByUserQuery query, CancellationToken cancellationToken)
    {
        var meetings = await mediator.Send(query, cancellationToken);
        
        return Results.Ok(meetings);
    }
    
    [HttpGet("{MeetingId:guid}")]
    public async Task<IResult> GetMeetings([FromRoute] GetMeetingWithParticipantsQuery query, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(query, cancellationToken);
        
        return Results.Ok(meeting);
    }
}