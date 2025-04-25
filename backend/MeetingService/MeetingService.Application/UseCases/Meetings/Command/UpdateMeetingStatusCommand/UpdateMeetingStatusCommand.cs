using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public record UpdateMeetingStatusCommand : IRequest<MeetingDto>
{
    
}