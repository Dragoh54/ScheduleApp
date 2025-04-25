using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingCommand;

public record UpdateMeetingCommand : IRequest<MeetingDto>
{
    
}