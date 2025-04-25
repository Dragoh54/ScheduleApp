using MediatR;
using MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;
using MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;
using MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;
using MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;
using MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;
using MeetingService.Application.UseCases.Meetings.Query.GetMeetingByIdQuery;
using MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;
using MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;
using Microsoft.AspNetCore.Mvc;

namespace MeetingService.Api.Controllers;

[ApiController]
[Route("meetings")]
public class MeetingController(
    IMediator mediator
    ) : Controller
{
    [HttpPost]
    public async Task<IResult> CreateMeeting([FromForm] CreateMeetingCommand command, CancellationToken cancellationToken)
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

    [HttpPatch("{meetingId:guid}/reschedule")]
    public async Task<IResult> RescheduleMeeting([FromRoute] Guid meetingId, 
        [FromForm] RescheduleMeetingCommand command, CancellationToken cancellationToken)
    {
        command.Id = meetingId;
        
        var meeting = await mediator.Send(command, cancellationToken);
        return Results.Ok(meeting);
    }

    [HttpPatch("{meetingId:guid}/information")]
    public async Task<IResult> UpdateInformation([FromRoute] Guid meetingId, 
        [FromForm] UpdateMeetingInformationCommand command, CancellationToken cancellationToken)
    {
        command.Id = meetingId;
        
        var meeting = await mediator.Send(command, cancellationToken);
        return Results.Ok(meeting);
    }

    [HttpPatch("{meetingId:guid}/status")]
    public async Task<IResult> UpdateMeetingStatus([FromRoute] Guid meetingId,
        [FromForm] UpdateMeetingStatusCommand command, CancellationToken cancellationToken)
    {
        command.Id = meetingId;
        
        var meeting = await mediator.Send(command, cancellationToken);
        return Results.Ok(meeting);
    }

    // [HttpGet("{meetingId:guid}")]
    // public async Task<IResult> GetMeeting([FromRoute] Guid meetingId, CancellationToken cancellationToken)
    // {
    //     var query = new GetMeetingByIdQuery {Id = meetingId};
    //     
    //     var meeting = await mediator.Send(query, cancellationToken);
    //     return Results.Ok(meeting);
    // }

    [HttpGet("user/{userId:guid}")]
    public async Task<IResult> GetMeetingsOrganizedByUser([FromRoute] GetMeetingsOrganizedByUserQuery query, CancellationToken cancellationToken)
    {
        var meetings = await mediator.Send(query, cancellationToken);
        return Results.Ok(meetings);
    }
    
    [HttpGet("{meetingId:guid}")]
    public async Task<IResult> GetMeetings([FromRoute] GetMeetingWithParticipantsQuery query, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(query, cancellationToken);
        return Results.Ok(meeting);
    }
    
    [HttpGet("health")]
    public async Task<IResult> Health()
    {
        return Results.Ok("Healthy");
    }
}