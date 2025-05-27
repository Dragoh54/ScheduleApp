using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;
using ScheduleService.Application.Dto.Meetings.Requests.Commands;
using ScheduleService.Application.Dto.Meetings.Requests.Queries;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.IsUserFreeQuery;
using ScheduleService.Application.UseCases.Meeting.Command.CreateMeetingCommand;
using ScheduleService.Application.UseCases.Meeting.Command.DeleteMeetingCommand;
using ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingCommand;
using ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingStatusCommand;
using ScheduleService.Application.UseCases.Meeting.Query.GetMeetingByIdQuery;
using ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserInRangeQuery;
using ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserOnDateQuery;
using ScheduleService.Application.UseCases.Meeting.Query.GetUpcomingMeetingsQuery;
using ScheduleService.Application.UseCases.Meeting.Query.GetUserMeetings;
using ScheduleService.Application.UseCases.Meeting.Query.IsUserHasMeetingQuery;

namespace ScheduleService.Api.Controllers;

[ApiController]
[Authorize]
[Route("meetings")]
public class MeetingController : Controller
{
    private readonly IMediator _mediator;

    public MeetingController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateMeetingCommand(dto);
        
        var meeting = await _mediator.Send(command, cancellationToken);
        return Ok(meeting);
    }
    
    [HttpPut("{meetingId:guid}")]
    public async Task<IActionResult> UpdateMeeting([FromRoute] Guid meetingId,[FromBody] UpdateMeetingRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateMeetingCommand(meetingId, dto);
        
        var meeting = await _mediator.Send(command, cancellationToken);
        return Ok(meeting);
    }
    
    [HttpPatch("{meetingId:guid}")]
    public async Task<IActionResult> UpdateMeetingStatus([FromRoute] Guid meetingId, [FromQuery] UpdateMeetingStatusRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateMeetingStatusCommand(meetingId, dto);
        
        var meeting = await _mediator.Send(command, cancellationToken);
        return Ok(meeting);
    }
    
    [HttpDelete("{MeetingId:guid}")]
    public async Task<IActionResult> DeleteMeeting([FromQuery] DeleteMeetingRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new DeleteMeetingCommand(dto);
        
        var meeting = await _mediator.Send(command, cancellationToken);
        return Ok(meeting);
    }
    
    [HttpGet("{MeetingId:guid}")]
    public async Task<IActionResult> GetMeeting([FromRoute] GetMeetingByIdRequestDto dto, CancellationToken cancellationToken)
    {
        var query = new GetMeetingByIdQuery(dto);
        
        var meeting = await _mediator.Send(query, cancellationToken);
        return Ok(meeting);
    }
    
    [HttpGet("user/{userId:guid}/in-range")]
    public async Task<IActionResult> GetMeetingsInRange([FromRoute] Guid userId, [FromQuery] GetMeetingsInRangeRequestDto dto, CancellationToken cancellationToken)
    {
        var query = new GetMeetingsForUserInRangeQuery(userId, dto);
        
        var meetings = await _mediator.Send(query, cancellationToken);
        return Ok(meetings);
    }
    
    [HttpGet("user/{userId:guid}/on-date")]
    public async Task<IActionResult> GetMeetingsOnDate([FromRoute] Guid userId, [FromQuery] GetMeetingsOnDateRequestDto dto, CancellationToken cancellationToken)
    {
        var query = new GetMeetingsForUserOnDateQuery(userId, dto);
        
        var meetings = await _mediator.Send(query, cancellationToken);
        return Ok(meetings);
    }
    
    [HttpGet("user/{UserId:guid}/upcoming")]
    public async Task<IActionResult> GetUpcomingMeetings([FromRoute] GetUpcomingMeetingsRequestDto dto, CancellationToken cancellationToken)
    {
        var query = new GetUpcomingMeetingsQuery(dto);
        
        var meetings = await _mediator.Send(query, cancellationToken);
        return Ok(meetings);
    }
    
    [HttpGet("user/{UserId:guid}")]
    public async Task<IActionResult> GetUserMeetings([FromRoute] GetUserMeetingsRequestDto dto, CancellationToken cancellationToken)
    {
        var query = new GetUserMeetingsQuery(dto);
        
        var meetings = await _mediator.Send(query, cancellationToken);
        return Ok(meetings);
    }
    
    [HttpGet("user/{userId:guid}/free")]
    public async Task<IActionResult> IsUserHasMeetings([FromRoute] Guid userId, [FromQuery] IsUserHasMeetingsRequestDto dto, CancellationToken cancellationToken)
    {
        var query = new IsUserHasMeetingQuery(userId, dto);
        
        var isUserHasMeetings = await _mediator.Send(query, cancellationToken);
        return Ok(isUserHasMeetings);
    }
}