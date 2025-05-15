using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    //[HttpPost("{userID:guid}")]
    public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingCommand command, CancellationToken cancellationToken)
    {
        var meeting = await _mediator.Send(command, cancellationToken);
        return Ok(meeting);
    }
    
    [HttpPut]
    //[HttpPut("{meetingId:guid}")] //todo: from query get userID?
    public async Task<IActionResult> UpdateMeeting([FromBody] UpdateMeetingCommand command, CancellationToken cancellationToken)
    {
        var meeting = await _mediator.Send(command, cancellationToken);
        return Ok(meeting);
    }
    
    [HttpPatch("status")]
    public async Task<IActionResult> UpdateMeetingStatus([FromQuery] UpdateMeetingStatusCommand command, CancellationToken cancellationToken)
    {
        var meeting = await _mediator.Send(command, cancellationToken);
        return Ok(meeting);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMeeting([FromQuery] DeleteMeetingCommand command, CancellationToken cancellationToken)
    {
        var meeting = await _mediator.Send(command, cancellationToken);
        return Ok(meeting);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMeeting([FromQuery] GetMeetingByIdQuery query, CancellationToken cancellationToken)
    {
        var meeting = await _mediator.Send(query, cancellationToken);
        return Ok(meeting);
    }
    
    [HttpGet("user/in-range")]
    public async Task<IActionResult> GetMeetingsInRange([FromQuery] GetMeetingsForUserInRangeQuery query, CancellationToken cancellationToken)
    {
        var meetings = await _mediator.Send(query, cancellationToken);
        return Ok(meetings);
    }
    
    [HttpGet("user/on-date")]
    public async Task<IActionResult> GetMeetingsOnDate([FromQuery] GetMeetingsForUserOnDateQuery query, CancellationToken cancellationToken)
    {
        var meetings = await _mediator.Send(query, cancellationToken);
        return Ok(meetings);
    }

    [HttpGet("user/upcoming")]
    public async Task<IActionResult> GetUpcomingMeetings([FromQuery] GetUpcomingMeetingsQuery query, CancellationToken cancellationToken)
    {
        var meetings = await _mediator.Send(query, cancellationToken);
        return Ok(meetings);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserMeetings([FromQuery] GetUserMeetingsQuery query, CancellationToken cancellationToken)
    {
        var meetings = await _mediator.Send(query, cancellationToken);
        return Ok(meetings);
    }

    [HttpGet("check")]
    public async Task<IActionResult> IsUserHasMeetings([FromQuery] IsUserHasMeetingQuery query, CancellationToken cancellationToken)
    {
        var isUserHasMeetings = await _mediator.Send(query, cancellationToken);
        return Ok(isUserHasMeetings);
    }
}