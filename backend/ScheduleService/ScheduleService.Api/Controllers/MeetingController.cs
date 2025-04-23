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
    
    [HttpPut]
    public async Task<IResult> UpdateMeeting([FromBody] UpdateMeetingCommand command, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(command, cancellationToken);
        return Results.Ok(meeting);
    }
    
    [HttpPatch("status")]
    public async Task<IResult> UpdateMeetingStatus([FromQuery] UpdateMeetingStatusCommand command, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(command, cancellationToken);
        return Results.Ok(meeting);
    }

    [HttpDelete]
    public async Task<IResult> DeleteMeeting([FromQuery] DeleteMeetingCommand command, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(command, cancellationToken);
        return Results.Ok(meeting);
    }
    
    [HttpGet]
    public async Task<IResult> GetMeeting([FromQuery] GetMeetingByIdQuery query, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(query, cancellationToken);
        return Results.Ok(meeting);
    }
    
    [HttpGet("user/in-range")]
    public async Task<IResult> GetMeetingsInRange([FromQuery] GetMeetingsForUserInRangeQuery query, CancellationToken cancellationToken)
    {
        var meetings = await mediator.Send(query, cancellationToken);
        return Results.Ok(meetings);
    }
    
    [HttpGet("user/on-date")]
    public async Task<IResult> GetMeetingsOnDate([FromQuery] GetMeetingsForUserOnDateQuery query, CancellationToken cancellationToken)
    {
        var meetings = await mediator.Send(query, cancellationToken);
        return Results.Ok(meetings);
    }

    [HttpGet("user/upcoming")]
    public async Task<IResult> GetUpcomingMeetings([FromQuery] GetUpcomingMeetingsQuery query, CancellationToken cancellationToken)
    {
        var meetings = await mediator.Send(query, cancellationToken);
        return Results.Ok(meetings);
    }

    [HttpGet("user")]
    public async Task<IResult> GetUserMeetings([FromQuery] GetUserMeetingsQuery query, CancellationToken cancellationToken)
    {
        var meetings = await mediator.Send(query, cancellationToken);
        return Results.Ok(meetings);
    }

    [HttpGet("check")]
    public async Task<IResult> IsUserHasMeetings([FromQuery] IsUserHasMeetingQuery query, CancellationToken cancellationToken)
    {
        var isUserHasMeetings = await mediator.Send(query, cancellationToken);
        return Results.Ok(isUserHasMeetings);
    }
}